using UnityEngine;
using System.Collections;

public class LRRHController : MonoBehaviour {
	
	// Set these in Unity
	public float speed; //Player move speed
	public int jumpLimit; // Number of jumps allowed before having to return to the floor
	public int jumpHeight; //Jump Height
	public float airMoveIncrement; //How much to decrease the airborne x-axis movement by each frame
	public float airGravityIncrement; //How much to increment the airborne gravity by each frame
	public KeyCode moveRight; // Right
	public KeyCode moveLeft; // Left
	public KeyCode moveJump; // Jump
	public KeyCode moveDown; // Dash

	// Private
	private int jumpsMade; //Number of jumps performed since leaving the ground
	private float airMoves; //Incremented value for x-axis movement while in the air
	private float airGravity; //Incremented value for x-axis movement while in the air
	private float origGravity; //Original gravity of the character
	private bool horizPlatMove; // Should we move with horizontal platforms?

	//Used to load in the textures for the swap (left text for moving left / right text for moving right)
	Texture leftTexture;
	Texture rightTexture;

	// At start we set the number of jumps performed since leaving the ground to 0
	void Start () {
		// Ignore P1 and P2 collisions
		Physics2D.IgnoreLayerCollision(12, 13);

		// Reset jumps
		if (gameObject.tag == "LRRH") 
		{
			jumpsMade = 0;
		}

		//Loading in the textures :D
		leftTexture = Resources.Load ("Textures/LRRH_Flip", typeof(Texture)) as Texture;
		rightTexture = Resources.Load ("Textures/LRRH", typeof(Texture)) as Texture;

		// Set airMoves to 1; save the initial gravity as origGravity
		airMoves = 1;
		origGravity = gameObject.rigidbody2D.gravityScale;

	}

	// Movement and jumping; reset rotation to 0,0,0 every frame to avoid Little Red Rotation Hood
	void Update () 
	{
		transform.eulerAngles = new Vector3(0,0,0); 
//		Debug.Log(Input.GetAxis("Horizontal_PLR2"));
		if (gameObject.tag == "LRRH")
		{
			//We check the layer to make sure its player one (layer 12)
			// If it is we let the pad controls work fine if its not we dont cause it's not player one
			if (gameObject.layer == 12)
				{
				//Move Right
				if (Input.GetKey (moveRight) || Input.GetAxis("Horizontal_PLR1") > 0) 
				{
					if (airMoves == 1)
					{
						transform.Translate (Vector2.right * speed * Time.deltaTime);
					}
					else if (airMoves > 1) 
					{
						transform.Translate (Vector2.right * (speed/airMoves) * Time.deltaTime);
						airMoves = airMoves + airMoveIncrement; 
						gameObject.rigidbody2D.gravityScale = gameObject.rigidbody2D.gravityScale + airGravityIncrement;
					}
					//transform.eulerAngles = new Vector2(0,0); 
					GameObject.Find("LRRH").gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", rightTexture);
				}
				//Move Left
				if (Input.GetKey (moveLeft) || Input.GetAxis("Horizontal_PLR1") < 0) 
				{
					if (airMoves == 1)
					{
						transform.Translate (-Vector2.right * speed * Time.deltaTime);
					}
					else if (airMoves > 1) 
					{
						transform.Translate (-Vector2.right * (speed/airMoves) * Time.deltaTime);
						airMoves = airMoves + airMoveIncrement; 
						gameObject.rigidbody2D.gravityScale = gameObject.rigidbody2D.gravityScale + airGravityIncrement;
					}
					//transform.eulerAngles = new Vector2(0,0); //flip the character on its x axis
					GameObject.Find("LRRH").gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", leftTexture);
				}
				//Jump
				if (Input.GetKeyDown (moveJump) || Input.GetButtonUp("Jump"))
				{
					if (jumpsMade < jumpLimit)
					{
						SoundManager play = GameObject.Find("SoundManager").gameObject.GetComponent<SoundManager>();
						play.PlayJumpLRRH();
						rigidbody2D.isKinematic = true;
						rigidbody2D.isKinematic = false;
						rigidbody2D.AddForce (Vector2.up * jumpHeight);
						jumpsMade++;
						airMoves = airMoves + airMoveIncrement;
					}
				}
				}else{
				//Copy Paste of the above code, this will be the controls for any player that != player1 (layer12)
				//As I only have one pad, for now I am removing pad support for player 2. SHould be quick to set up
				//In the future though.
					//Move Right
					if (Input.GetKey (moveRight)) 
					{
						if (airMoves == 1)
						{
							transform.Translate (Vector2.right * speed * Time.deltaTime);
						}
						else if (airMoves > 1) 
						{
							transform.Translate (Vector2.right * (speed/airMoves) * Time.deltaTime);
							airMoves = airMoves + airMoveIncrement; 
							gameObject.rigidbody2D.gravityScale = gameObject.rigidbody2D.gravityScale + airGravityIncrement;
						}
						//transform.eulerAngles = new Vector2(0,0); 
						GameObject.Find("LRRH").gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", rightTexture);
					}
					//Move Left
					if (Input.GetKey (moveLeft)) 
					{
						if (airMoves == 1)
						{
							transform.Translate (-Vector2.right * speed * Time.deltaTime);
						}
						else if (airMoves > 1) 
						{
							transform.Translate (-Vector2.right * (speed/airMoves) * Time.deltaTime);
							airMoves = airMoves + airMoveIncrement; 
							gameObject.rigidbody2D.gravityScale = gameObject.rigidbody2D.gravityScale + airGravityIncrement;
						}
						//transform.eulerAngles = new Vector2(0,0); //flip the character on its x axis
						GameObject.Find("LRRH").gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", leftTexture);
					}
					//Jump
					if (Input.GetKeyDown (moveJump))
					{
						if (jumpsMade < jumpLimit)
						{
							SoundManager play = GameObject.Find("SoundManager").gameObject.GetComponent<SoundManager>();
							play.PlayJumpLRRH();
							rigidbody2D.isKinematic = true;
							rigidbody2D.isKinematic = false;
							rigidbody2D.AddForce (Vector2.up * jumpHeight);
							jumpsMade++;
							airMoves = airMoves + airMoveIncrement;
						}
					}
				}
		}	
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		// If she hits a piece of floor (or BBW's head) reset the available jumps
		if (col.gameObject.tag == "Ground" || col.gameObject.tag == "BBW") 
		{
			jumpsMade = 0;
			airMoves = 1;
			gameObject.rigidbody2D.gravityScale = origGravity;
		}
	}

	// If we're sat on a horizontal-moving platform, and we aren't moving anywhere, we move in sync with the platform
	void OnCollisionStay2D(Collision2D col)
	{
		if (col.gameObject.tag == "MovingPlatform")
		{
			if (Input.GetKey (moveLeft) || Input.GetKey (moveJump) || Input.GetKey (moveRight))
			{
				horizPlatMove = false;
			}
			else
			{
				horizPlatMove = true;
			}
			PlatformMovement moveScript = col.gameObject.GetComponent<PlatformMovement>();
			if (moveScript.upAndDown == false && horizPlatMove == true)
			{
				switch (moveScript.goingRight)
				{
				case true:
					transform.Translate (Vector2.right * moveScript.speed * Time.deltaTime);
					break;
				case false:
					transform.Translate (-Vector2.right * moveScript.speed * Time.deltaTime);
					break;
				}
			}
		}
	}
}





