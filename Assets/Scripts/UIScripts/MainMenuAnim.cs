using UnityEngine;
using System.Collections;

public class MainMenuAnim : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKey){
			gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
		}
	}
}
