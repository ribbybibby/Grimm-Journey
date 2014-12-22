using UnityEngine;
using System.Collections;

public class TrollController : MonoBehaviour {

	// Set in Unity
	public int speed; // Speed of the character
	public float sightDownDistance; // LOS downwards
	public float sightCloseDistance; // Line of sight for turning around

	// Private
	private int startTimeOut; //
	public bool headingright;
	private bool dropped;

	//Used to load in the textures for the swap (left text for moving left / right text for moving right)
	Texture leftTexture;
	Texture rightTexture;

	// Make sure chars in the 'Enemy' layer ignore each other, set 'dropped' to false
	void Start() {
		dropped = false;
		Physics2D.IgnoreLayerCollision (11, 11);

		//Loading in the textures :D
		leftTexture = Resources.Load ("Textures/troll", typeof(Texture)) as Texture;
		rightTexture = Resources.Load ("Textures/troll_Flip", typeof(Texture)) as Texture;
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
		FallThroughFloor ();
	}
	
	// Troll will only fall through the floor if he is above LRRH and there is a platform to land on
	// Once he has dropped once, he will never drop again
	void FallThroughFloor ()
	{
		if (dropped == false)
		{
			GameObject lrrh = GameObject.FindGameObjectWithTag ("LRRH");
		
			float bbwdiffx = lrrh.transform.position.x - transform.position.x;
			float bbwdiffy = transform.position.y - lrrh.transform.position.y;
		
			if ((bbwdiffx > -10 & bbwdiffx < 10) & bbwdiffy > 2)
			{
				RaycastHit2D[] hit = Physics2D.RaycastAll (transform.position, -Vector2.up, sightDownDistance);
				if (hit.Length > 3)
				{
					for (int i = 0; i < hit.Length; i++)
					{
						if (hit[i].collider.tag == "Ground" || hit[i].collider.name == "Platform")
						{
							Physics2D.IgnoreCollision(gameObject.collider2D, hit[i].rigidbody.collider2D);
							dropped = true;
							break;
						}
					}
				}
			}
		}
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
	}
	
	// T moves in one direction along the X axis until 'headright' changes
	void KeepOnMoving ()
	{
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
	}
	
	
	// If LLRH comes within sightCloseDistance of T, T will turn around to face LRRH
	void TurnAround ()
	{
		RaycastHit2D[] hitleft = Physics2D.RaycastAll (transform.position, -Vector2.right, sightCloseDistance);
		RaycastHit2D[] hitright = Physics2D.RaycastAll (transform.position, Vector2.right, sightCloseDistance);
		
		for (int i = 0; i < hitright.Length; i++)
		{
			if (hitright[i].collider.tag == "LRRH")
			{
				headingright = true;
			}
		}
		
		for (int i = 0; i < hitleft.Length; i++)
		{
			if (hitleft[i].collider.tag == "LRRH")
			{
				headingright = false;
			}
		}
		
		
		
	}
}
