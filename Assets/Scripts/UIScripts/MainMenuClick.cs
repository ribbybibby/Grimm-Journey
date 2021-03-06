﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuClick : MonoBehaviour {

	bool yesToStart;
	public int LevelToLoad;

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
			SceneManager.LoadScene (LevelToLoad);
		}
	}
}
