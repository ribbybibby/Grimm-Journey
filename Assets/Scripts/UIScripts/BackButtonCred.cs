using UnityEngine;
using System.Collections;

public class BackButtonCred : MonoBehaviour {

	public GameObject storeCred;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void toggleCred(){
		if(storeCred.activeSelf){
			storeCred.SetActive(false);
		}else{
			storeCred.SetActive(true);
		}
	}
}
