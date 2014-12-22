using UnityEngine;
using System.Collections;

public class BackGroundDrop : MonoBehaviour {

	bool stopPan;

	// Use this for initialization
	void Start () {
		stopPan = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(stopPan == false){
			gameObject.transform.position += new Vector3(0,-10,0)*Time.deltaTime;
		}
	}

	void OnTriggerEnter(Collider other) {
		if(other.name == "GroundMeet"){
			stopPan = true;
		}
	}
}
