using UnityEngine;
using System.Collections;

public class ClickCredits : MonoBehaviour {

	GameObject storeCtrls;
//	GameObject storeBackBtn;
	
	// Use this for initialization
	void Start () {
		storeCtrls = GameObject.Find("PnlCred");
		storeCtrls.SetActive(false);
		//storeBackBtn.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void toggleCred(){
		if(storeCtrls.activeSelf){
			storeCtrls.SetActive(false);
			//storeBackBtn.SetActive(false);
			//Time.timeScale = 1F;
		}else{
			storeCtrls.SetActive(true);
			//storeBackBtn.SetActive(true);
			//Time.timeScale = 0F;
		}
	}
}
