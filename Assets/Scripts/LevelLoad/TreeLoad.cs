using UnityEngine;
using System.Collections;

public class TreeLoad : MonoBehaviour {

	bool stopPan;
	public string direction;

	// Use this for initialization
	void Start () {
		stopPan = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(stopPan == false && direction == "left"){
			gameObject.transform.position += new Vector3(10,0,0)*Time.deltaTime;
		}

		if(stopPan == false && direction == "right"){
			gameObject.transform.position += new Vector3(-10,0,0)*Time.deltaTime;
		}
	}

	void OnTriggerEnter(Collider other) {
		if(other.name == "TreeMeet"){
			stopPan = true;
		}
	}
}
