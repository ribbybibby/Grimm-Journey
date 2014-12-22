using UnityEngine;
using System.Collections;

public class QuitOut : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void quit(){
		Application.LoadLevel(0);
	}

	public void quitAll(){
		Application.Quit();
		Debug.Log("Quit Out");
	}
}
