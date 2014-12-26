using UnityEngine;
using System.Collections;

public class BBWController : MonoBehaviour {
	
	// Set these in Unity
	public float speed; //Player move speed
	public int jumpLimit; // Number of jumps allowed before having to return to the floor
	public int jumpHeight; //Jump Height
	public KeyCode moveRight; // Right
	public KeyCode moveLeft; // Left
	public KeyCode moveJump; // Jump
	public KeyCode moveDown; // Dash
	public Material[] materials; //0 = Skin, 1 = Attack
	public bool facingRight; // Allows Melee System to tell which direction BBW is facing 



	//Used to load in the textures for the swap (left text for moving left / right text for moving right)
	public Texture leftTexture;
	public Texture rightTexture;


	// Private
	private int jumpsMade; //Number of jumps performed since leaving the ground
	private SoundManager play;


	// At start we set the number of jumps performed since leaving the ground to 0
	void Start () 
	{	
		play = GameObject.Find("SoundManager").gameObject.GetComponent<SoundManager>();
		if (gameObject.tag == "BBW") 
		{
			jumpsMade = 0;
		}

		//Loading in the textures :D
		leftTexture = Resources.Load ("Textures/BBW", typeof(Texture)) as Texture;
		rightTexture = Resources.Load ("Textures/BBW_Flip", typeof(Texture)) as Texture;
	}

	// Movement controls
	void Update () 
	{
		transform.eulerAngles = new Vector3(0,0,0);
		//Debug.Log(Input.GetAxis("Horizontal_PLR1"));
		if (gameObject.tag == "BBW")
		{
			//Move Right
			if (Input.GetKey (moveRight)|| Input.GetAxis("Horizontal_PLR1") > 0)
			{
				transform.Translate (Vector2.right * speed * Time.deltaTime);
				GameObject.Find("BBW").gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", rightTexture);
				facingRight = true;

			}
			//Move Left
			if (Input.GetKey (moveLeft)|| Input.GetAxis("Horizontal_PLR1") < 0) 
			{
				transform.Translate (-Vector2.right * speed * Time.deltaTime);	
				GameObject.Find("BBW").gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", leftTexture);
				facingRight = false;
			}

		}	
	}


	// On Collision with LRRH, allow jumping
	void OnTriggerEnter2D(Collider2D other)
	{

		if (other.gameObject.tag == "LRRHTrigger") 
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
		
		//If BBW hits a piece of floor, reset the available jumps and switch back to normal texture 
		if (other.tag == "PlatformTrigger" || other.gameObject.tag == "LRRHTrigger") 
		{
			jumpsMade = 0;
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{

		if (other.gameObject.tag == "LRRHTrigger") 
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
	}

	void OnTriggerExit2D(Collider2D other)
	{

		if (other.gameObject.tag == "LRRHTrigger") 
		{

			//Jump
			if (Input.GetKeyDown (moveJump))
			{
				if (jumpsMade < jumpLimit)
				{
					play.PlayJumpBBW();
					rigidbody2D.AddForce (Vector2.up * jumpHeight);
					jumpsMade++;
				}
			}
		}
	}
	
}




