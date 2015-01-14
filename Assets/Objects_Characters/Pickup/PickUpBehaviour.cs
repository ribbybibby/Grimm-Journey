using UnityEngine;
using System.Collections;

public class PickUpBehaviour : MonoBehaviour {
	public float timeout;
	public GameObject bbw1p;
	public GameObject bbw2p;
	public GameObject lrrh1p;
	public GameObject lrrh2p;
	public float distanceFromPickup;

	private float startTimeOut;
	private bool hidden;
	private Vector2 startPos;
	private GameObject bbw;
	private GameObject lrrh;
	private float bbwDistance;
	private float lrrhDistance;

	// Save the initial timeout value set in Unity and set hidden to false
	void Start()
	{
		hidden = false;
		startTimeOut = UpdateTimer (timeout);
		startPos = transform.position;
	}

	// Bring back pick up if the time has ran down
	void Update()
	{
		if (hidden == true) // 
		{
			bbw = GameObject.FindGameObjectWithTag ("BBW");
			lrrh = GameObject.FindGameObjectWithTag ("LRRH");
			bbwDistance = Vector3.Distance(startPos, bbw.gameObject.transform.position);
			lrrhDistance = Vector3.Distance(startPos, lrrh.gameObject.transform.position);
			if (Time.time >= startTimeOut && bbwDistance > distanceFromPickup && lrrhDistance > distanceFromPickup)
			{
				transform.position = startPos;
				hidden = false;
			}
		}
	}
	
	// If a player character collides with the pickup, and the timer has reset, switch controls between BBW and LRRH,
	// then switch positions, change the clone's name and destroy old objects.
	void OnCollisionEnter2D(Collision2D col)
	{
		if /*(timeout >= startTimeOut &*/ (col.gameObject.tag == "BBW" || col.gameObject.tag == "LRRH")
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
			startTimeOut = UpdateTimer (timeout);
			transform.position = new Vector2 (-100f, -100f);
			hidden = true;
		}
	}

		// Update Timer:
	// Returns a value to set timerUpdate to
	// This is the current time + (timer value + the timer value divided by a random number between 1 and 5)
	public float UpdateTimer (float thetime) 
	{
		float newTime = Time.time + thetime;
		return newTime;
	}
}
