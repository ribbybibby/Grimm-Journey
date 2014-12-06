using UnityEngine;
using System.Collections;

public class PickUpBehaviour : MonoBehaviour {
	public int timeout;
	private int startTimeOut;

	// Save the initial timeout value set in Unity
	void Start()
	{
		startTimeOut = timeout;
	}

	// Keep knocking timeout down
	void Update()
	{
		timeout--;
	}
	
	// If a player character collides with the pickup, and the timer has reset, switch controls between BBW and LRRH,
	// then switch positions and destroy old objects.
	void OnCollisionEnter2D(Collision2D col)
	{
		if (timeout <= 0 & (col.gameObject.tag == "BBW" || col.gameObject.tag == "LRRH"))
		{
					GameObject currentlrrh = GameObject.FindGameObjectWithTag ("LRRH");
					GameObject currentbbw = GameObject.FindGameObjectWithTag ("BBW");
		
					KeyCode bbwOldUp = currentbbw.GetComponent<BBWController> ().moveJump;
					KeyCode bbwOldLeft = currentbbw.GetComponent<BBWController> ().moveLeft;
					KeyCode bbwOldRight = currentbbw.GetComponent<BBWController> ().moveRight;
					KeyCode bbwOldDown = currentbbw.GetComponent<BBWController> ().moveDown;

					KeyCode lrrhOldUp = currentlrrh.GetComponent<LRRHController> ().moveJump;
					KeyCode lrrhOldLeft = currentlrrh.GetComponent<LRRHController> ().moveLeft;
					KeyCode lrrhOldRight = currentlrrh.GetComponent<LRRHController> ().moveRight;
					KeyCode lrrhOldDown = currentlrrh.GetComponent<LRRHController> ().moveDown;

				
					currentlrrh.gameObject.GetComponent<LRRHController> ().moveRight = bbwOldRight;
					currentlrrh.gameObject.GetComponent<LRRHController> ().moveLeft = bbwOldLeft;
					currentlrrh.gameObject.GetComponent<LRRHController> ().moveJump = bbwOldUp;
					currentlrrh.gameObject.GetComponent<LRRHController> ().moveDown = bbwOldDown;


					currentbbw.gameObject.GetComponent<BBWController> ().moveRight = lrrhOldRight;
					currentbbw.gameObject.GetComponent<BBWController> ().moveLeft = lrrhOldLeft;
					currentbbw.gameObject.GetComponent<BBWController> ().moveJump = lrrhOldUp;
					currentbbw.gameObject.GetComponent<BBWController> ().moveJump = lrrhOldDown;


					Instantiate (currentbbw, currentlrrh.gameObject.transform.position, currentlrrh.gameObject.transform.rotation);
					Instantiate (currentlrrh, currentbbw.transform.position, currentbbw.transform.rotation);
					Destroy (currentlrrh);
					Destroy (currentbbw);
					timeout = startTimeOut;
		}
	}
}
