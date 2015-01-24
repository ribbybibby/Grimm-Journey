using UnityEngine;
using System.Collections;

public class PickUpBehaviour : MonoBehaviour {
	public float timeout;
	public GameObject bbw1p;
	public GameObject bbw2p;
	public GameObject lrrh1p;
	public GameObject lrrh2p;
	public LayerMask myLayerMask;
	public float showUIDistance;

	private GameObject tracklrrh;
	private GameObject trackbbw;
	private float bbwDistance;
	private float lrrhDistance;
	//private int startTimeOut;
	private bool hidden;
	private Vector2 startPos;
	private Canvas mask;

	// Save the initial timeout value set in Unity and set hidden to false
	void Start()
	{
		mask = GetComponentInChildren<Canvas> ();
		mask.enabled = false;
		hidden = false;
		//startTimeOut = timeout;
		//startPos = transform.position;
	}

	// Bring back pick up if the time has ran down
	void Update()
	{
		tracklrrh = GameObject.FindGameObjectWithTag ("LRRH");
		trackbbw = GameObject.FindGameObjectWithTag ("BBW");

		lrrhDistance = Vector3.Distance (gameObject.transform.position, tracklrrh.transform.position);
		bbwDistance = Vector3.Distance (gameObject.transform.position, trackbbw.transform.position);

		if (hidden == false && (bbwDistance < showUIDistance || lrrhDistance < showUIDistance))
		{
			mask.enabled = true;
		}
		else 
		{
			mask.enabled = false;
		}

		/*if (hidden == true) // 
		{
			mask.enabled = false;
			timeout--;
			if (timeout <= 0)
			{
				gameObject.renderer.enabled = true;
				hidden = false;
			}
		}*/

	}
	
	// If a player character collides with the pickup, and the timer has reset, switch controls between BBW and LRRH,
	// then switch positions, change the clone's name and destroy old objects.
	void OnTriggerEnter2D(Collider2D col)
	{
		if (hidden == false && (col.gameObject.tag == "BBW" || col.gameObject.tag == "LRRH"))
		{

			SoundManager play = GameObject.Find("SoundManager").gameObject.GetComponent<SoundManager>();
			play.PlayTransformingLight();
			GameObject currentlrrh = GameObject.FindGameObjectWithTag ("LRRH");
			GameObject currentbbw = GameObject.FindGameObjectWithTag ("BBW");

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

			Destroy (currentlrrh);
			Destroy (currentbbw);
			gameObject.renderer.enabled = false;
			mask.enabled = false;
			hidden = true;
			StartCoroutine(waitThenShow());
			//timeout = startTimeOut;
			//transform.position = new Vector2 (-100f, -100f);
			//gameObject.renderer.enabled = false;
		}
	}

	IEnumerator waitThenShow(){
		yield return new WaitForSeconds(timeout);
		hidden = false;
		mask.enabled = true;
		gameObject.renderer.enabled = true;
	}
}
