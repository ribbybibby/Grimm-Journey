using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {
	public int lockMidVector; // 1, 2 or 3 - decides which camera code we use
	public float camSpeed; // The speed the camera moves at
	public float moveDistance; // The distance the camera should be from the midVector before moving
	public float camSizeDivider; // The number we divide the distance between the characters by (which we then then add to the orthographic camera size)
	public GameObject background; // The background object
	public float shakeForce;

	private GameObject bbw; // BBW
	private GameObject lrrh; // LRRH
	private Vector3 midVector; // The Vector between LRRH and BBW
	private float charDistance; // The distance between LRRH and BBW
	private float origCamSize; // The starting size of the Camera
	private Vector3 camVector; // The Vector we use to apply camSpeed
	private float screenQuart; // The three-quarter point of the screen
	private bool moveUp; // Should the camera be moving up?
	private float newPosY; // New Y position for the camera to move to
	private bool offScreen; // Are either of LRRH or BBW offscreen?
	private float topY; // Top of the camera
	private float bottomY; // Bottom of the camera
	private float someNumber; // Some number

	// Camera shake
	private bool shakeCentreFound;
	private bool shakeReturn;
	private Vector3 shakeCentre;


	// Use this for initialization
	void Start () {
		// Find the char game objects
		bbw = GameObject.Find ("BBW");
		lrrh = GameObject.Find ("LRRH");

		// Find the midVector
		midVector = new Vector3 ((bbw.transform.position.x+lrrh.transform.position.x)/2, (bbw.transform.position.y+lrrh.transform.position.y)/2, gameObject.transform.position.z);

		// Store the original size of the camera
		origCamSize = gameObject.camera.orthographicSize;

		// Create the camVector using camSpeed
		camVector = new Vector3 (0, camSpeed, 0);

		// Start the camera at the midVector
		gameObject.transform.position = midVector;

		someNumber = 0;

		// Shake things
		shakeCentreFound = false;
		shakeReturn = false;

	}
	
	// Update is called once per frame
	void Update () 
	{

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
			// ONE: Moves up or down depending on whether the characters are outside of a tolerance area
			CameraOne();
			break;
		case 2:
			// TWO: Moves 1:1 with the MidVector
			CameraTwo();
			break;
		case 3:
			// THREE: Moves 1:1 with BBW
			CameraThree();
			break;
		case 4:
			// FOUR: Readjusts to the centre of the screen after both characters reach the top quarter of the screen
			CameraFourandFive(4);
			break;
		case 5:
			// FIVE: Readjusts so that the chars are in the bottom quarter of the camera's view once they reach the top quarter
			CameraFourandFive(5);
			break;
		default:
			// DEFAULT: Use Camera 2 by default
			CameraTwo ();
			break;
		}

		// Deal with cases where characters are outside the camera's view
		OffScreen ();

	}

	void CameraOne()
	{
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
	}
	
	void CameraTwo() 
	{
		// Adjust cam size based on charDistance
		gameObject.camera.orthographicSize = origCamSize + (charDistance / camSizeDivider);
		
		// Move Camera in sync with the mid vector
		gameObject.transform.position = new Vector3 (midVector.x, midVector.y, gameObject.transform.position.z);
		
		// Move the background in sync with the camera
		background.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, background.transform.position.z);
	}
	
	void CameraThree ()
	{
		// Adjust cam size based on charDistance
		gameObject.camera.orthographicSize = origCamSize + (charDistance / camSizeDivider);
		
		// Move camera in sync with BBW
		gameObject.transform.position = new Vector3 (bbw.transform.position.x, bbw.transform.position.y, gameObject.transform.position.z);
		
		// Move background in sync with the camera
		background.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, background.transform.position.z);
	}
	
	void CameraFourandFive (int choice) 
	{
		// Find the start of the top quarter of the screen (y-axis)
		screenQuart = gameObject.transform.position.y + (gameObject.camera.orthographicSize/2);

		// If both chars are in the top quarter, and the camera isn't currently moving already, move up
		if (bbw.transform.position.y > screenQuart && lrrh.transform.position.y > screenQuart && moveUp == false)
		{		
			// Cam 4: centre on where the chars are
			if (choice == 4) 
			{
				newPosY = midVector.y;
			}
			// Cam 5: Put the chars in the bottom quarter of the screen
			if (choice == 5)
			{
				newPosY = midVector.y + (gameObject.camera.orthographicSize/2);
			}
			moveUp = true;
		}
		
		if (moveUp == true)
		{
			// If we're at the point we need to be, stop moving
			if (gameObject.transform.position.y >= newPosY)
			{
				moveUp = false;
			}
			// Move up at the speed of camVector
			else
			{
				gameObject.transform.position += camVector*Time.deltaTime;
			}
		}
		
		// Move the background in sync with the camera
		background.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, background.transform.position.z);
	}

	void OffScreen ()
	{

		// Find the top and bottom y-coords of the camera in world space
		topY = gameObject.transform.position.y + gameObject.camera.orthographicSize;
		bottomY = gameObject.transform.position.y - gameObject.camera.orthographicSize;

		// If either character is above or below, we set offScreen to true
		if (lrrh.transform.position.y >= topY || lrrh.transform.position.y <= bottomY 
		    || bbw.transform.position.y >= topY || bbw.transform.position.y <= bottomY)
		{
			offScreen = true; 
		}
		// Else false
		else
		{
			offScreen = false;
		}
		
		// If offscreen, increment someNumber
		if (offScreen == true) 
		{
			someNumber++;
			// Shake the screen
			ShakeIt(true);
		}
		// If false, bring the number back down to 0
		if (offScreen == false && someNumber > 0)
		{
			someNumber--;
			// Stop shaking the screen
			ShakeIt(true);
		}	
	}


	void ShakeIt (bool shakeon)
	{
		switch (shakeon){
		// Shaking
		case true:
			// Find the current position of the camera, which will anchor the shake
			if (shakeCentreFound == false)
			{
				shakeCentre = gameObject.transform.position;
				shakeCentreFound = true;
			}

			// Shake
			if (shakeCentreFound == true)
			{

				// We alternate between the anchor position and a random position by switching shakeReturn between true and false
				if (shakeReturn == false)
				{
					// Find a random vector within (-shakeForce, -shakeForce, z) and (shakeForce, shakeForce, z)
					float rndY = Random.Range (-(someNumber/100), (someNumber/100));
					float rndX = Random.Range (-(someNumber/100), (someNumber/100));

					// Move the camera to this random vector 
					gameObject.transform.position = new Vector3 ((gameObject.transform.position.x + rndX), (gameObject.transform.position.y + rndY), gameObject.transform.position.z);

					shakeReturn = true;
				}
				// Anchor
				else
				{
					gameObject.transform.position = shakeCentre;
					shakeReturn = false;
				}
			}
			break;
		// Stop shaking
		case false:
			// Return the camera back to the centre
			gameObject.transform.position = shakeCentre;

			// Unset the centre
			shakeCentreFound = false;
			break;
		}
	}

	
}
