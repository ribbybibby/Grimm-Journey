using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {
	public int lockMidVector; // 1, 2 or 3 - decides which camera code we use
	public float camSpeed; // The speed the camera moves at
	public float moveDistance; // The distance the camera should be from the midVector before moving
	public float camSizeDivider; // The number we divide the distance between the characters by (which we then then add to the orthographic camera size)
	public GameObject background; // The background object

	private GameObject bbw; // BBW
	private GameObject lrrh; // LRRH
	private Vector3 midVector; // The Vector between LRRH and BBW
	private float charDistance; // The distance between LRRH and BBW
	private float origCamSize; // The starting size of the Camera
	private Vector3 camVector; // The Vector we use to apply camSpeed

	// Use this for initialization
	void Start () {
		// Find the char game objects
		bbw = GameObject.Find ("BBW");
		lrrh = GameObject.Find ("LRRH");

		// Find the midVector
		midVector = new Vector3 ((bbw.transform.position.x+lrrh.transform.position.x)/2, (bbw.transform.position.y+lrrh.transform.position.y)/2, gameObject.transform.position.z);

		// Store the original size of the camera
		origCamSize = gameObject.camera.orthographicSize;

		// Start the camera at the midVector
		gameObject.transform.position = midVector;

		// Create the camVector using camSpeed
		camVector = new Vector3 (0, camSpeed, 0);

	}
	
	// Update is called once per frame
	void Update () {

		// Find BBW and LRRH each update, just in case they've switched
		bbw = GameObject.Find ("BBW");
		lrrh = GameObject.Find ("LRRH");

		// Find the mid vector between the two
		midVector = new Vector3 ((bbw.transform.position.x+lrrh.transform.position.x)/2, (bbw.transform.position.y+lrrh.transform.position.y)/2, gameObject.transform.position.z);

		// Find the distance between the two
		charDistance = Vector3.Distance (bbw.transform.position, lrrh.transform.position);


		switch (lockMidVector) 
		{
			case 1:
				
				// Adjust cam size based on charDistance
				gameObject.camera.orthographicSize = origCamSize + (charDistance / camSizeDivider);
				
				// If the distance between the Camera and the mid vector on the y-axis is below -moveDistance, move the camera down
				if ((midVector.y - gameObject.transform.position.y) < -moveDistance) {
					gameObject.transform.position -= camVector*Time.deltaTime;
				}
				// If the distance between the Camera and the mid vector on the y-axis is above moveDistance, move the camera up
				if ((midVector.y - gameObject.transform.position.y) > moveDistance) {
					gameObject.transform.position += camVector*Time.deltaTime;
				}
				
				// Move the camera along with the mid vector on the x-axis
				gameObject.transform.position = new Vector3 (midVector.x, gameObject.transform.position.y, gameObject.transform.position.z);
				
				// Move the background in sync with the camera
				background.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, background.transform.position.z);
				break;
			case 2:
				// Adjust cam size based on charDistance
				gameObject.camera.orthographicSize = origCamSize + (charDistance / camSizeDivider);
				
				// Move Camera in sync with the mid vector
				gameObject.transform.position = new Vector3 (midVector.x, midVector.y, gameObject.transform.position.z);

				// Move the background in sync with the camera
				background.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, background.transform.position.z);
				break;
			case 3:
				// Adjust cam size based on charDistance
				gameObject.camera.orthographicSize = origCamSize + (charDistance / camSizeDivider);
				
				// Move camera in sync with BBW
				gameObject.transform.position = new Vector3 (bbw.transform.position.x, bbw.transform.position.y, gameObject.transform.position.z);
				
				// Move background in sync with the camera
				background.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, background.transform.position.z);
				break;
		}
	}
}
