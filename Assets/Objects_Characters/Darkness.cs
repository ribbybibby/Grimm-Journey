using UnityEngine;
using System.Collections;

public class Darkness : MonoBehaviour {

	//float camPanDirection;
	//bool stopPan;
	
	// Use this for initialization
	void Start () {
		//stopPan = false;
	}
	
	// Update is called once per frame
	void Update () {
		//if(stopPan == false){
			Physics2D.IgnoreLayerCollision (0, 0);
			gameObject.transform.position += new Vector3(0,1,0)*Time.deltaTime;
		//}
		//Debug.Log (stopPan);
		
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		//if(other.tag == "enemy" || other.tag == "LRRH" || other.tag == "BBW"){
			Destroy(other.gameObject);

		if(other.gameObject.name == "BBW" || other.gameObject.name == "LRRH")
		{
			Application.LoadLevel(6);
		}
		//}
	}
}
