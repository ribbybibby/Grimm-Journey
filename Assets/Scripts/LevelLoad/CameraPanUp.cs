using UnityEngine;
using System.Collections;

public class CameraPanUp : MonoBehaviour {

	float camPanDirection;
	public bool stopPan;
	public int LevelID;
	
	// Use this for initialization
	void Start () {
		stopPan = false;
		//StartCoroutine(waitThenPanUp());
		//SoundManager play = GameObject.Find("SoundManager").gameObject.GetComponent<SoundManager>();
		//play.PlayIntroNarration();
	}
	
	// Update is called once per frame
	void Update () {
		if(stopPan == false){
			gameObject.transform.position += new Vector3(0,15,0)*Time.deltaTime;
		}
	}


	public void startwaitThenPanUp(){
		StartCoroutine(waitThenPanUp());
	}

	IEnumerator waitThenPanUp(){
		stopPan = false;
		yield return new WaitForSeconds(5.0F);
		Application.LoadLevel(LevelID);
	}

	void OnTriggerEnter(Collider other) {
		if(other.name == "GroundMeet"){
			stopPan = true;
		}
	}
}
