using UnityEngine;
using System.Collections;

public class BBWController : MonoBehaviour {
	
	// Set these in Unity
	public float speed; //Player move speed
	public int kickForce; // Force of kick
	public int jumpLimit; // Number of jumps allowed before having to return to the floor
	public int jumpHeight; //Jump Height
	public KeyCode moveRight; // Right
	public KeyCode moveLeft; // Left
	public KeyCode moveJump; // Jump
	public KeyCode moveDash; // Dash
	public Material[] materials; //0 = Skin, 1 = Attack

	// Private
	private int jumpsMade; //Number of jumps performed since leaving the ground

	// At start we set the number of jumps performed since leaving the ground to 0
	void Start () 
	{
		if (gameObject.tag == "BBW") 
		{
			jumpsMade = 0;
		}
	}
	
	void Update () 
	{
		
		if (gameObject.tag == "BBW")
		{
			//Move Right
			if (Input.GetKey (moveRight)) 
			{
				transform.Translate (Vector2.right * speed * Time.deltaTime);
				transform.eulerAngles = new Vector2(0,0); 
			}
			//Move Left
			if (Input.GetKey (moveLeft)) 
			{
				transform.Translate (Vector2.right * speed * Time.deltaTime);
				transform.eulerAngles = new Vector3(0,0,180); //flip the character on its x axis
			}

		}	
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "LRRH") 
		{
			//Jump
			if (Input.GetKeyDown (moveJump))
			{
				if (jumpsMade < jumpLimit)
				{
					rigidbody2D.AddForce (Vector2.up * jumpHeight);
					jumpsMade++;
				}
			}
		}
		
		// If Orton hits a piece of floor, reset the available jumps and switch back to normal texture 
		if (col.gameObject.tag == "Ground" || col.gameObject.tag == "LRRH") 
		{
			jumpsMade = 0;
			renderer.material = materials[0];
		}
	}

	void OnCollisionStay2D(Collision2D col)
	{
		if (col.gameObject.tag == "LRRH") 
		{
			//Jump
			if (Input.GetKeyDown (moveJump))
			{
				if (jumpsMade < jumpLimit)
				{
					rigidbody2D.AddForce (Vector2.up * jumpHeight);
					jumpsMade++;
				}
			}
		}
	}

	void OnCollisionExit2D(Collision2D col)
	{
		if (col.gameObject.tag == "LRRH") 
		{
			//Jump
			if (Input.GetKeyDown (moveJump))
			{
				if (jumpsMade < jumpLimit)
				{
					rigidbody2D.AddForce (Vector2.up * jumpHeight);
					jumpsMade++;
				}
			}
		}
	}
	
}




