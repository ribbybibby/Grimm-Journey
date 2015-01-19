using UnityEngine;
using System.Collections;

public class WitchControllerNew : MonoBehaviour {
	
	// Set in Unity
	public int speed; // Speed of the character
	public float sightDownDistance; // LOS downwards
	public float sightCloseDistance; // Line of sight for turning around
	public float catTimer;

	//CATS! D:
	public GameObject cat; 
	
	// Private
	private int startTimeOut; //
	private bool headingright;
	private bool headingup;
	private float catTimerUpdate;
	//Used to load in the textures for the swap (left text for moving left / right text for moving right)
	Texture leftTexture;
	Texture rightTexture;
	
	// Make sure chars in the 'Enemy' layer ignore each other, set 'dropped' to false
	void Start() {
		headingup = true;
		// dropped = false;
		Physics2D.IgnoreLayerCollision (11, 11);

		//Loading in the textures :D
		leftTexture = Resources.Load ("Textures/witch", typeof(Texture)) as Texture;
		rightTexture = Resources.Load ("Textures/witch", typeof(Texture)) as Texture;

		catTimerUpdate = UpdateTimer (catTimer);
	}
	
	/* Main method:
	 * - Fix orientation every update
	 * - Turn Around if LRRH is within sightCloseDistance
	 * - Translate left or right based on current heading
	 * - Drop down if conditions are satisfied
	 */
	void FixedUpdate () 
	{
		transform.eulerAngles = new Vector3(0,0,0);
		TurnAround ();
		KeepOnMoving ();
		rndCatFling();
	}
	
	// Tag the left and right boundary walls as LeftBound and RightBound
	// T will turn around when he hits one
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "LeftBound") 
		{
			headingright = true;
		}
		if (col.gameObject.tag == "RightBound")
		{
			headingright = false;
		}
		if (col.gameObject.tag == "Roof")
		{
			headingup = false;
		}
		if (col.gameObject.tag == "Floor")
		{
			headingup = true;
		}
	}

	// Throw a cat
	void rndCatFling(){
		if(Time.time >= catTimerUpdate)
		{
			GameObject newCat = (GameObject)Instantiate(cat, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(new Vector3(0, 0, 0)));
			newCat.name = "Cat";
			catTimerUpdate = UpdateTimer (catTimer);
		}
	}

	// Update Timer:
	// Returns a value to set timerUpdate to
	// This is the current time + (timer value + the timer value divided by a random number between 1 and 5)
	private float UpdateTimer (float thetime) 
	{
		float rndNo = Random.Range(1,5);
		float newTime = Time.time + (thetime + (thetime / rndNo));
		return newTime;
	}

	// T moves in one direction along the X axis until 'headright' changes
	void KeepOnMoving ()
	{
		//Added by R2DJ - Just RNG for now till I work something out
		//rndMoveDown();
		//transform.Translate (Vector2.up * speed * Time.deltaTime);
		if (headingright == true)
		{
			transform.Translate (Vector2.right * speed * Time.deltaTime); 
			gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", rightTexture);
		}
		else if (headingright == false)
		{
			transform.Translate (-Vector2.right * speed * Time.deltaTime);
			gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", leftTexture);
		}

		//This witch can go where she pleases (kinda) so i'm adding in Y-axis movement
		//Texture doesn't matter really since we only use one texture
		if (headingup == true)
		{
			transform.Translate (Vector2.up * speed * Time.deltaTime);
			//gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", rightTexture);
		}
		else if (headingup == false)
		{
			transform.Translate (-Vector2.up * speed * Time.deltaTime);
			//gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", leftTexture);
		}
	}
	
	
	// If LLRH comes within sightCloseDistance of T, T will turn around to face LRRH
	void TurnAround ()
	{
		RaycastHit2D[] hitleft = Physics2D.RaycastAll (transform.position, -Vector2.right, sightCloseDistance);
		RaycastHit2D[] hitright = Physics2D.RaycastAll (transform.position, Vector2.right, sightCloseDistance);

		RaycastHit2D[] hitup = Physics2D.RaycastAll (transform.position, Vector2.up, sightCloseDistance);
		RaycastHit2D[] hitdown = Physics2D.RaycastAll (transform.position, -Vector2.up, sightCloseDistance);
		
		for (int i = 0; i < hitright.Length; i++)
		{
			if (hitright[i].collider.tag == "LRRH")
			{
				headingright = true;
				gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", rightTexture);
			}
		}
		
		for (int i = 0; i < hitleft.Length; i++)
		{
			if (hitleft[i].collider.tag == "LRRH")
			{
				headingright = false;
				gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", leftTexture);
			}
		}

		//Mercilessly butcher Rob1's code again
		for (int i = 0; i < hitdown.Length; i++)
		{
			if (hitdown[i].collider.tag == "LRRH")
			{
				headingup = false;
				gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", leftTexture);
			}
		}

		//Mercilessly butcher Rob1's code again
		for (int i = 0; i < hitup.Length; i++)
		{
			if (hitup[i].collider.tag == "LRRH")
			{
				headingup = true;
				gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", leftTexture);
			}
		}
				
	}
}
