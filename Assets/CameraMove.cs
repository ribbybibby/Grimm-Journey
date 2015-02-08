using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {
	private GameObject bbw;
	private GameObject lrrh;
	private GameObject background;
	private float charDistance;
	private float origCamSize;
	// Use this for initialization
	void Start () {
		bbw = GameObject.Find ("BBW");
		lrrh = GameObject.Find ("LRRH");
		background = GameObject.Find ("LevelCastleBG");
		origCamSize = gameObject.camera.orthographicSize;

	}
	
	// Update is called once per frame
	void Update () {
		bbw = GameObject.Find ("BBW");
		lrrh = GameObject.Find ("LRRH");
		charDistance = Vector3.Distance (bbw.transform.position, lrrh.transform.position);
		//if (charDistance > 10 && gameObject.camera.orthographicSize < 20) 
		//{
			gameObject.camera.orthographicSize = origCamSize + (charDistance / 10);
		//}

		Debug.Log (charDistance);
		gameObject.transform.position = new Vector3 (bbw.transform.position.x, bbw.transform.position.y, gameObject.transform.position.z);
		background.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, background.transform.position.z);
	}
}
