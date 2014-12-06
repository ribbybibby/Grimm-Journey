using UnityEngine;
using System.Collections;

public class PickUpBehaviour : MonoBehaviour {
	public int timeout;
	private int startTimeOut;


	void Start()
	{
		startTimeOut = timeout;
	}

	void Update()
	{
		timeout--;
	}
	
	
	void OnCollisionEnter2D(Collision2D col)
	{
		if (timeout <= 0 & (col.gameObject.tag == "BBW" || col.gameObject.tag == "LRRH"))
		{
					GameObject currentlrrh = GameObject.FindGameObjectWithTag ("LRRH");
					GameObject currentbbw = GameObject.FindGameObjectWithTag ("BBW");
		
					KeyCode bbwOldUp = currentbbw.GetComponent<BBWController> ().moveJump;
					KeyCode bbwOldLeft = currentbbw.GetComponent<BBWController> ().moveLeft;
					KeyCode bbwOldRight = currentbbw.GetComponent<BBWController> ().moveRight;
					KeyCode lrrhOldUp = currentlrrh.GetComponent<LRRHController> ().moveJump;
					KeyCode lrrhOldLeft = currentlrrh.GetComponent<LRRHController> ().moveLeft;
					KeyCode lrrhOldRight = currentlrrh.GetComponent<LRRHController> ().moveRight;
					
					currentlrrh.gameObject.GetComponent<LRRHController> ().moveRight = bbwOldRight;
					currentlrrh.gameObject.GetComponent<LRRHController> ().moveLeft = bbwOldLeft;
					currentlrrh.gameObject.GetComponent<LRRHController> ().moveJump = bbwOldUp;
					
					currentbbw.gameObject.GetComponent<BBWController> ().moveRight = lrrhOldRight;
					currentbbw.gameObject.GetComponent<BBWController> ().moveLeft = lrrhOldLeft;
					currentbbw.gameObject.GetComponent<BBWController> ().moveJump = lrrhOldUp;

					Instantiate (currentbbw, currentlrrh.gameObject.transform.position, currentlrrh.gameObject.transform.rotation);
					Instantiate (currentlrrh, currentbbw.transform.position, currentbbw.transform.rotation);
					Destroy (currentlrrh);
					Destroy (currentbbw);
					timeout = startTimeOut;
		}
	}
}
