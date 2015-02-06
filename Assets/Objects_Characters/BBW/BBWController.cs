using UnityEngine;
using System.Collections;

public class BBWController : MonoBehaviour {
	
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
	public bool facingRight; // Allows Melee System to tell which direction BBW is facing 
	
	//Used to load in the textures for the swap (left text for moving left / right text for moving right)
	public Texture leftTexture;
	public Texture rightTexture;


	// Private
	private int jumpsMade; //Number of jumps performed since leaving the ground
	private float airMoves; //Incremented value for x-axis movement while in the air
	private float airGravity; //Incremented value for x-axis movement while in the air
	private float origGravity; //Original gravity of the character
	private SoundManager play; // The sound manager
	private GameObject glow; // The glow object on BBW
	private bool horizPlatMove; // Should we move with horizontal platforms?

	void Start () 
	{	
		// Grab the soundmanager
		play = GameObject.Find("SoundManager").gameObject.GetComponent<SoundManager>();

		// Reset jumps to 0
		if (gameObject.tag == "BBW") 
		{
			jumpsMade = 0;
		}

		//Loading in the textures :D
		leftTexture = Resources.Load ("Textures/BBW", typeof(Texture)) as Texture;
		rightTexture = Resources.Load ("Textures/BBW_Flip", typeof(Texture)) as Texture;

		// Set airMoves to 1; save the initial gravity as origGravity
		airMoves = 1;
		origGravity = gameObject.rigidbody2D.gravityScale;

		// Find the attached Glow object
		glow = GameObject.Find ("Glow");
		glow.renderer.enabled = false;
	}

	// Movement controls
	void Update () 
	{
		// Make sure we still have the correct Glow object
		glow = GameObject.Find ("Glow");

		//Fix rotation every update
		transform.eulerAngles = new Vector3(0,0,0);

		if (gameObject.tag == "BBW") {
			//We check the layer to make sure its player one (layer 12)
			// If it is we let the pad controls work fine if its not we dont cause it's not player one
			if (gameObject.layer == 12) {
				//Move Right
				// If BBW is in the air, start to limit movement
				if (Input.GetKey (moveRight) || Input.GetAxis ("Horizontal_PLR1") > 0) {
					if (airMoves == 1) 
					{
						transform.Translate (Vector2.right * speed * Time.deltaTime);
					} 
					else if (airMoves > 1) 
					{
						transform.Translate (Vector2.right * (speed / airMoves) * Time.deltaTime);
						airMoves = airMoves + airMoveIncrement; 
						gameObject.rigidbody2D.gravityScale = gameObject.rigidbody2D.gravityScale + airGravityIncrement;
					}

					//Switch to Right Texture, position glow
					GameObject.Find ("BBW").gameObject.GetComponent<Renderer> ().material.SetTexture ("_MainTex", rightTexture);
					glow.transform.localPosition = new Vector3 (-0.1f, -0.039f, 0.2f);
					facingRight = true;
					
				}
				//Move Left
				// If BBW is in the air, start to limit movement
				if (Input.GetKey (moveLeft) || Input.GetAxis ("Horizontal_PLR1") < 0) {
					if (airMoves == 1) 
					{
						transform.Translate (-Vector2.right * speed * Time.deltaTime);
					} 
					else if (airMoves > 1) 
					{
						transform.Translate (-Vector2.right * (speed / airMoves) * Time.deltaTime);
						airMoves = airMoves + airMoveIncrement; 
						gameObject.rigidbody2D.gravityScale = gameObject.rigidbody2D.gravityScale + airGravityIncrement;
					}

					//Switch to Left Texture, position glow
					GameObject.Find ("BBW").gameObject.GetComponent<Renderer> ().material.SetTexture ("_MainTex", leftTexture);
					glow.transform.localPosition = new Vector3 (0.08f, -0.04f, 0.2f);
					facingRight = false;
				}	
			}else{
			//END OF PLAYER 1 LAYER CHECK
				if (gameObject.tag == "BBW") {
					//Move Right
					// If BBW is in the air, start to limit movement
					if (Input.GetKey (moveRight)) {
						if (airMoves == 1) 
						{
							transform.Translate (Vector2.right * speed * Time.deltaTime);
						} 
						else if (airMoves > 1) 
						{
							transform.Translate (Vector2.right * (speed / airMoves) * Time.deltaTime);
							airMoves = airMoves + airMoveIncrement; 
							gameObject.rigidbody2D.gravityScale = gameObject.rigidbody2D.gravityScale + airGravityIncrement;
						}

						//Switch to Right Texture, position glow
						GameObject.Find ("BBW").gameObject.GetComponent<Renderer> ().material.SetTexture ("_MainTex", rightTexture);
						glow.transform.localPosition = new Vector3 (-0.1f, -0.039f, 0.2f);
						facingRight = true;
						
					}
					//Move Left
					// If BBW is in the air, start to limit movement
					if (Input.GetKey (moveLeft)) {
						if (airMoves == 1) 
						{
							transform.Translate (-Vector2.right * speed * Time.deltaTime);
						} 
						else if (airMoves > 1) 
						{
							transform.Translate (-Vector2.right * (speed / airMoves) * Time.deltaTime);
							airMoves = airMoves + airMoveIncrement; 
							gameObject.rigidbody2D.gravityScale = gameObject.rigidbody2D.gravityScale + airGravityIncrement;
						}

						//Switch to Left Texture, position glow
						GameObject.Find ("BBW").gameObject.GetComponent<Renderer> ().material.SetTexture ("_MainTex", leftTexture);
						glow.transform.localPosition = new Vector3 (0.08f, -0.04f, 0.2f);
						facingRight = false;
					}
				}
			}
		}
	}
	
	// On Collision with LRRH, allow jumping
	void OnTriggerEnter2D(Collider2D other)
	{

		// Enable glow, if Player 1 accept pad and key input, if not just key
		if (other.gameObject.tag == "LRRHTrigger") {
			glow.renderer.enabled = true;		
			if (gameObject.layer == 12) {

				//Jump
				if (Input.GetKeyDown (moveJump) || Input.GetButtonUp ("Jump")) {
					if (jumpsMade < jumpLimit) {
						play.PlayJumpBBW ();
						rigidbody2D.AddForce (Vector2.up * jumpHeight);
						jumpsMade++;
					}
				}
			}
			else
			{
				//Jump
				if (Input.GetKeyDown (moveJump)) {
					if (jumpsMade < jumpLimit) {
						play.PlayJumpBBW ();
						rigidbody2D.AddForce (Vector2.up * jumpHeight);
						jumpsMade++;
					}
				}
			}
		}
		
		//If BBW hits a piece of floor, reset the available jumps and switch back to normal texture 
		if (other.tag == "Ground" || other.gameObject.tag == "LRRHTrigger") 
		{
			jumpsMade = 0;
			airMoves = 1;
			gameObject.rigidbody2D.gravityScale = origGravity;
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{

		// Enable glow, if Player 1 accept pad and key input, if not just key
		if (other.gameObject.tag == "LRRHTrigger") 
		{
			glow.renderer.enabled = true;		
			if (gameObject.layer == 12) 
			{
				//Jump
				if (Input.GetKeyDown (moveJump) || Input.GetButtonUp("Jump"))
				{
					if (jumpsMade < jumpLimit)
					{
						play.PlayJumpBBW();
						rigidbody2D.AddForce (Vector2.up * jumpHeight);
						jumpsMade++;
						airMoves = airMoves + airMoveIncrement;
					}
				}
			}
			else
			{
				//Jump
				if (Input.GetKeyDown (moveJump))
				{
					if (jumpsMade < jumpLimit)
					{
						play.PlayJumpBBW();
						rigidbody2D.AddForce (Vector2.up * jumpHeight);
						jumpsMade++;
						airMoves = airMoves + airMoveIncrement;
					}
				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		// Disable glow, if Player 1 accept pad and key input, if not just key
		glow.renderer.enabled = false;		
		if (other.gameObject.tag == "LRRHTrigger") 
		{
			if (gameObject.layer == 12)
			{
				//Jump
				if (Input.GetKeyDown (moveJump) || Input.GetButtonUp("Jump"))
				{
					if (jumpsMade < jumpLimit)
					{
						play.PlayJumpBBW();
						rigidbody2D.AddForce (Vector2.up * jumpHeight);
						jumpsMade++;
					}
				}
			}
			else
			{
				//Jump
				if (Input.GetKeyDown (moveJump))
				{
					if (jumpsMade < jumpLimit)
					{
						play.PlayJumpBBW();
						rigidbody2D.AddForce (Vector2.up * jumpHeight);
						jumpsMade++;
						airMoves = airMoves + airMoveIncrement;
					}
				}
			}
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
			if (moveScript.upAndDown == false)
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




