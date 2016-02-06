using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CameraPanUp : MonoBehaviour {

	float camPanDirection;
	public bool stopPan;
	public int LevelID;
	private bool loadIn;
	GameObject bg;
	
	// Use this for initialization
	void Start () {
		stopPan = false;
		bg = gameObject.GetComponent<CameraMove>().background;
		loadIn = true;
		//StartCoroutine(waitThenPanUp());
		//SoundManager play = GameObject.Find("SoundManager").gameObject.GetComponent<SoundManager>();
		//play.PlayIntroNarration();
	}
	
	// Update is called once per frame
	void Update () {


		// On the load in, if we are above the Y co-ord of the background then we know to stop
		if (loadIn == true && stopPan == false) 
		{

			if (gameObject.transform.position.y >= bg.transform.position.y) 
			{
				stopPan = true;
				loadIn = false;
			}

		}

		// Camera movement
		if(stopPan == false){
			// Make sure the background is always aligned with the camera on the X axis
			bg.transform.position = new Vector3 (gameObject.transform.position.x, bg.transform.position.y, bg.transform.position.z);
			gameObject.transform.position += new Vector3(0,15,0)*Time.deltaTime;
		}
	}


	public void startwaitThenPanUp(){
		StartCoroutine(waitThenPanUp());
	}

	IEnumerator waitThenPanUp(){
		stopPan = false;
		yield return new WaitForSeconds(5.0F);
		SceneManager.LoadScene (LevelID);
	}

	//void OnTriggerEnter(Collider other) {
	//	if(other.name == "GroundMeet"){
	//		stopPan = true;
	//	}
	//}
}
