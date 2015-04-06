using UnityEngine;
using System.Collections;

public class Darkness : MonoBehaviour {

	//float camPanDirection;
	//bool stopPan;
	private SoundManager play; // The sound manager

	//Not really required as we only tag floor as Ground
	//But I added this just in case we tag other things as Ground in the future
	//Will be used to make sure the Bwap sound only plays once
	//When the darkness appears on screen
	bool hasPlayed;

	// Use this for initialization
	void Start () {
		//stopPan = false;
		play = GameObject.Find("SoundManager").gameObject.GetComponent<SoundManager>();
		hasPlayed = false;
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

		if(other.gameObject.name == "BBW" || other.gameObject.name == "LRRH")
		{
			Application.LoadLevel(6);
		}
		
		if (other.gameObject.tag == "Ground" && hasPlayed == false)
		{
			play.PlayBwap();
			hasPlayed = true;
		}

		Destroy(other.gameObject);

	}
}
