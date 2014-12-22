using UnityEngine;
using System.Collections;

public class ClickHelp : MonoBehaviour {

	GameObject storeCtrls;

	// Use this for initialization
	void Start () {
		storeCtrls = GameObject.Find("PnlHelp");
		storeCtrls.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void toggleHelp(){
		if(storeCtrls.activeSelf){
			storeCtrls.SetActive(false);
			//Time.timeScale = 1F;
		}else{
			storeCtrls.SetActive(true);
			//Time.timeScale = 0F;
		}
	}
}
