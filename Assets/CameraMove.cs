using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {
	public int lockMidVector;
	public float camSpeed;
	public float moveDistance;

	private GameObject bbw;
	private GameObject lrrh;
	private GameObject background;
	private Vector3 midVector;
	private float charDistance;
	private float origCamSize;
	private Vector3 camVector;

	// Use this for initialization
	void Start () {
		bbw = GameObject.Find ("BBW");
		lrrh = GameObject.Find ("LRRH");
		background = GameObject.Find ("LevelCastleBG");
		midVector = new Vector3 ((bbw.transform.position.x+lrrh.transform.position.x)/2, (bbw.transform.position.y+lrrh.transform.position.y)/2, gameObject.transform.position.z);
		origCamSize = gameObject.camera.orthographicSize;
		gameObject.transform.position = midVector;
		camVector = new Vector3 (0, camSpeed, 0);

	}
	
	// Update is called once per frame
	void Update () {
		bbw = GameObject.Find ("BBW");
		lrrh = GameObject.Find ("LRRH");
		midVector = new Vector3 ((bbw.transform.position.x+lrrh.transform.position.x)/2, (bbw.transform.position.y+lrrh.transform.position.y)/2, gameObject.transform.position.z);
		charDistance = Vector3.Distance (bbw.transform.position, lrrh.transform.position);

		switch (lockMidVector) 
		{
			case 1:
				if (charDistance > 2) 
				{
					gameObject.camera.orthographicSize = origCamSize + (charDistance / 100);
				}

				if ((midVector.y - gameObject.transform.position.y) < -moveDistance) {
					gameObject.transform.position -= camVector*Time.deltaTime;
				}
				if ((midVector.y - gameObject.transform.position.y) > moveDistance) {
					gameObject.transform.position += camVector*Time.deltaTime;
				}

				gameObject.transform.position = new Vector3 (midVector.x, gameObject.transform.position.y, gameObject.transform.position.z);
				background.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, background.transform.position.z);
				break;
			case 2:
				gameObject.camera.orthographicSize = origCamSize + (charDistance / 100);
				gameObject.transform.position = new Vector3 (midVector.x, midVector.y, gameObject.transform.position.z);
				background.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, background.transform.position.z);
				break;
			case 3:
				gameObject.camera.orthographicSize = origCamSize + (charDistance / 100);
				gameObject.transform.position = new Vector3 (bbw.transform.position.x, bbw.transform.position.y, gameObject.transform.position.z);
				background.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, background.transform.position.z);
				break;
		}
	}
}
