using UnityEngine;
using System.Collections;

public class LoadNextLevel : MonoBehaviour {

	//Running out of time so I'm not using GetComponent camera I'm just going to plug it in here
	public Camera mainCam;


	private bool bbw;
	private bool lrrh;


	// Use this for initialization
	void Start () {
		bbw = false;
		lrrh = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (lrrh == true && bbw == true)
		{
			mainCam.transform.SendMessage("startwaitThenPanUp", SendMessageOptions.DontRequireReceiver);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.name == "BBW" || other.name == "LRRH")
		{
			other.GetComponent<EnemyReceiver>().invuln = true;
		}

		if (other.name == "BBW")
		{
			bbw = true;
		}

		if (other.name == "LRRH")
		{
			lrrh = true;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.name == "BBW" || other.name == "LRRH")
		{
			other.GetComponent<EnemyReceiver>().invuln = false;
		}

		if (other.name == "BBW")
		{
			bbw = false;
		}
		
		if (other.name == "LRRH")
		{
			lrrh = false;
		}
	}
}
