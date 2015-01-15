using UnityEngine;
using System.Collections;

public class MainMenuClick : MonoBehaviour {

	bool yesToStart;

	// Use this for initialization
	void Start () {
		yesToStart = false;
		//StartCoroutine(waitForDestruct());
		//InvokeRepeating("waitForDestruct", 2, 5.0F);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKey){
			yesToStart = true;
			StartCoroutine(waitForDestruct());
		}
	}

	IEnumerator waitForDestruct(){
		//if (Input.anyKey){
		yield return new WaitForSeconds(3);
		if (yesToStart == true){
			Application.LoadLevel(1);
		}
	}
}
