using UnityEngine;
using System.Collections;

public class CameraPan : MonoBehaviour {

	float camPanDirection;
	bool stopPan;

	// Use this for initialization
	void Start () {
		stopPan = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(stopPan == false){
			gameObject.transform.position += new Vector3(0,0,15)*Time.deltaTime;
		}
		Debug.Log (stopPan);

	}

	void OnTriggerEnter(Collider other) {
			stopPan = true;
	}
}
