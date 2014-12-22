using UnityEngine;
using System.Collections;

public class ClickPause : MonoBehaviour {

	GameObject tmpStoreObj;

	// Use this for initialization
	void Start () {
		//GameObject.Find("Panel").SetActive(true);
		tmpStoreObj = GameObject.Find("Panel");
		tmpStoreObj.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		padToggle();
		padCancel();
	}

	public void togglePause(){
		if(tmpStoreObj.activeSelf){
			tmpStoreObj.SetActive(false);
			Time.timeScale = 1F;
		}else{
			tmpStoreObj.SetActive(true);
			Time.timeScale = 0F;
		}
	}

	public void padToggle(){
		if(Input.GetButtonUp("Pad_Start")){
			if(tmpStoreObj.activeSelf){
				tmpStoreObj.SetActive(false);
				Time.timeScale = 1F;
			}else{
				tmpStoreObj.SetActive(true);
				Time.timeScale = 0F;
			}
		}
	}

	public void padCancel(){
		if(Input.GetButtonUp("Cancel")){
			if(tmpStoreObj.activeSelf){
				tmpStoreObj.SetActive(false);
				Time.timeScale = 1F;
			}
		}
	}
}
