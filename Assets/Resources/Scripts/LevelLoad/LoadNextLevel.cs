using UnityEngine;
using System.Collections;

public class LoadNextLevel : MonoBehaviour {

	//Running out of time so I'm not using GetComponent camera I'm just going to plug it in here
	public Camera mainCam;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.name == "BBW" || other.name == "LRRH"){
			mainCam.transform.SendMessage("startwaitThenPanUp", SendMessageOptions.DontRequireReceiver);
		}
	}
}
