using UnityEngine;
using System.Collections;

public class PickUpBehaviour : MonoBehaviour {
	// Set in Unity
	public float timeout; // How long before we bring the PickUp back?
	public GameObject bbw1p; // The PlayerOne BBW prefab
	public GameObject bbw2p; // The PlayerTwo BBW prefab
	public GameObject lrrh1p; // The PlayerOne LRRH prefab
	public GameObject lrrh2p; // The PlayerTwo LRRH prefab
	public LayerMask myLayerMask; // A layer mask that should include P1 and P2 layers
	public float showUIDistance; // How close should a player be before we show the UI?

	// Private
	private GameObject tracklrrh; // Save the current LRRH object
	private GameObject trackbbw; // Save the current BBW object
	private float bbwDistance; // How far away is BBW?
	private float lrrhDistance; // How far away is LRRH?
	private bool hidden; // Bool to track whether the pickup is hidden or not
	private Canvas mask; // The 'mask' UI

	// Save the initial timeout value set in Unity and set hidden to false
	void Start()
	{
		// Find the mask UI canvas, disable it to begin with
		mask = GetComponentInChildren<Canvas> ();
		mask.enabled = false;

		// Pickup is not hidden to begin with
		hidden = false;
	}

	void Update()
	{
		// Find how far away LRRH and BBW are from the pickup
		tracklrrh = GameObject.FindGameObjectWithTag ("LRRH");
		trackbbw = GameObject.FindGameObjectWithTag ("BBW");

		lrrhDistance = Vector3.Distance (gameObject.transform.position, tracklrrh.transform.position);
		bbwDistance = Vector3.Distance (gameObject.transform.position, trackbbw.transform.position);

		// Show the mask UI if BBW or LRRH are within showUIDistance of the Pickup, and the Pickup isn't hidden
		if (hidden == false && (bbwDistance < showUIDistance || lrrhDistance < showUIDistance))
		{
			mask.enabled = true;
		}
		else 
		{
			mask.enabled = false;
		}
	}
	
	// If a player character collides with the pickup, and the timer has reset, switch controls between BBW and LRRH,
	// then switch positions, change the clone's name and destroy old objects.
	void OnTriggerEnter2D(Collider2D col)
	{
		if (hidden == false && (col.gameObject.tag == "BBW" || col.gameObject.tag == "LRRH"))
		{

			// Play sound effect
			SoundManager play = GameObject.Find("SoundManager").gameObject.GetComponent<SoundManager>();
			play.PlayTransformingLight();

			// Find the current LRRH and BBW objects
			GameObject currentlrrh = GameObject.FindGameObjectWithTag ("LRRH");
			GameObject currentbbw = GameObject.FindGameObjectWithTag ("BBW");

			// Switch prefabs for BBW and LRRH based on player number
			if (currentbbw.layer == 12)
			{
				GameObject NewLRRH = (GameObject)Instantiate (lrrh1p, currentbbw.transform.position, currentbbw.transform.rotation);
				NewLRRH.name = "LRRH";
			}
			if (currentbbw.layer == 13)
			{
				GameObject NewLRRH = (GameObject)Instantiate (lrrh2p, currentbbw.transform.position, currentbbw.transform.rotation);
				NewLRRH.name = "LRRH"; 
			}
			if (currentlrrh.layer == 12)
			{
				GameObject NewBBW = (GameObject)Instantiate (bbw1p, currentlrrh.transform.position, currentlrrh.transform.rotation);
				NewBBW.name = "BBW";
			}
			if (currentlrrh.layer == 13)
			{
				GameObject NewBBW = (GameObject)Instantiate (bbw2p, currentlrrh.transform.position, currentlrrh.transform.rotation);
				NewBBW.name = "BBW";
			}

			// Destroy old objects
			Destroy (currentlrrh);
			Destroy (currentbbw);

			// Hide the pickup
			StartCoroutine(waitThenShow());
		}
	}

	// Hides the pickup by disabling the mask UI, the renderer and turning the 'hidden' bool to true
	// Waits for timeout
	// And then reverts the hiding
	IEnumerator waitThenShow(){
		gameObject.renderer.enabled = false;
		mask.enabled = false;
		hidden = true;
		yield return new WaitForSeconds(timeout);
		hidden = false;
		mask.enabled = true;
		gameObject.renderer.enabled = true;
	}
}
