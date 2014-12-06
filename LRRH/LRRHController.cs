using UnityEngine;
using System.Collections;

public class LRRHController : MonoBehaviour {
	
	// Set these in Unity
	public float speed; //Player move speed
	public int jumpLimit; // Number of jumps allowed before having to return to the floor
	public int jumpHeight; //Jump Height
	public KeyCode moveRight; // Right
	public KeyCode moveLeft; // Left
	public KeyCode moveJump; // Jump
	public KeyCode moveDown; // Dash
	public Material[] materials; //0 = Character Skin, 1 = Attack

	// Private
	private int jumpsMade; //Number of jumps performed since leaving the ground

	// At start we set the number of jumps performed since leaving the ground to 0
	void Start () {
		if (gameObject.tag == "LRRH") 
		{
			jumpsMade = 0;
		}
	}

	// Movement and jumping; reset rotation to 0,0,0 every frame to avoid Little Red Rotation Hood
	void Update () 
	{
		transform.eulerAngles = new Vector3(0,0,0); 

		if (gameObject.tag == "LRRH")
		{
			//Move Right
			if (Input.GetKey (moveRight)) 
			{
				transform.Translate (Vector2.right * speed * Time.deltaTime);
				//transform.eulerAngles = new Vector2(0,0); 
			}
			//Move Left
			if (Input.GetKey (moveLeft)) 
			{
				transform.Translate (-Vector2.right * speed * Time.deltaTime);
				//transform.eulerAngles = new Vector2(0,0); //flip the character on its x axis
			}
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

	void OnCollisionEnter2D(Collision2D col)
	{
		// If she hits a piece of floor, reset the available jumps
		if (col.gameObject.tag == "Ground" || col.gameObject.tag == "BBW") 
		{
			jumpsMade = 0;
		}
	}
}





