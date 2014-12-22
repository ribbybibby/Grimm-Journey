using UnityEngine;
using System.Collections;

public class PickUpBehaviour : MonoBehaviour {
	public int timeout;
	public GameObject bbw1p;
	public GameObject bbw2p;
	public GameObject lrrh1p;
	public GameObject lrrh2p;

	private int startTimeOut;
	private bool hidden;
	private Vector2 startPos;

	// Save the initial timeout value set in Unity and set hidden to false
	void Start()
	{
		hidden = false;
		startTimeOut = timeout;
		startPos = transform.position;
		timeout = 0;
	}

	// Bring back pick up if the time has ran down
	void Update()
	{
		if (hidden == true) // 
		{
			timeout--;
			if (timeout <= 0)
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
		if (timeout <= 0 & (col.gameObject.tag == "BBW" || col.gameObject.tag == "LRRH"))
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
			timeout = startTimeOut;
			transform.position = new Vector2 (-100f, -100f);
			hidden = true;
		}
	}
}
