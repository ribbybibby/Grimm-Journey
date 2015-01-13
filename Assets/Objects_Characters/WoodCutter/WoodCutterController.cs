using UnityEngine;
using System.Collections;

public class WoodCutterController : MonoBehaviour {

	// Set in Unity
	public int speed; // Speed of the character
	public int losIncrease; // Multiplier to * speed by if WC spots BBW
	public float sightDownDistance; // LOS downwards
	public float sightSideDistance; // Line of sight for charge (speed increase)
	public float sightCloseDistance; // Line of sight for turning around
	public int timeout; // Time out for FallThroughFloor, so he doesn't just drop through 3 platforms in a second
	public float wolfRangeLeft;
	public float wolfRangeRight;

	// Private
	private int startTimeOut; //
	public bool headingright;
	private int startSpeed;
	private bool chasing;

	//Used to load in the textures for the swap (left text for moving left / right text for moving right)
	Texture leftTexture;
	Texture rightTexture;

	// Remember starting speed, the start of the timeout and make sure chars in the 'Enemy' layer ignore each other
	void Start() {
		startSpeed = speed;
		startTimeOut = timeout;
		chasing = false;
		Physics2D.IgnoreLayerCollision (11, 11);

		//Loading in the textures :D
		leftTexture = Resources.Load ("Textures/woodsman", typeof(Texture)) as Texture;
		rightTexture = Resources.Load ("Textures/woodsman_Flip", typeof(Texture)) as Texture;
	}

	/* Main method:
	 * - Fix orientation every update
	 * - Move timeout along for FallThroughFloor
	 * - Speed up if BBW is in LOS
	 * - Turn Around if BBW is within sightCloseDistance
	 * - Translate left or right based on current heading
	 * - Drop down if conditions are satisfied
	 */
	void FixedUpdate () {
		transform.eulerAngles = new Vector3(0,0,0);
		timeout--;
		SpeedUpOnLOS ();
		TurnAround ();
		KeepOnMoving ();
		//FallThroughFloor ();
	}
	
	/*// Woodcutter will only fall through the floor if he is above BBW and there is a platform to land on
	// There is also a slight timeout on it.
	void FallThroughFloor ()
	{
		GameObject bbw = GameObject.FindGameObjectWithTag ("BBW");

		float bbwdiffx = bbw.transform.position.x - transform.position.x;
		float bbwdiffy = transform.position.y - bbw.transform.position.y;

		if ((bbwdiffx > wolfRangeLeft & bbwdiffx < wolfRangeRight) & bbwdiffy > 2 & timeout <= 0)
		{
			RaycastHit2D[] hit = Physics2D.RaycastAll (transform.position, -Vector2.up, sightDownDistance);
			if (hit.Length > 3)
			{
				for (int i = 0; i < hit.Length; i++)
				{
					if (hit[i].collider.tag == "Ground" || hit[i].collider.name == "Platform")
					{
						Physics2D.IgnoreCollision(gameObject.collider2D, hit[i].rigidbody.collider2D);
						timeout = startTimeOut;
						break;
					}
				}
			}
		}
	}*/

	// Tag the left and right boundary walls as LeftBound and RightBound
	// Woody will turn around when he hits one
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "LeftBound" || (col.gameObject.tag == "LeftBoundPatrol" && chasing != true)) 
		{
			headingright = true;
		}
		if (col.gameObject.tag == "RightBound" || (col.gameObject.tag == "RightBoundPatrol" && chasing != true))
		{
			headingright = false;
		}
	}

	// Woodsman moves along the X axis until he reaches near the camera's edge, 
	// then he turns around and goes the other way
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

	//Woodsman will increase speed if he sees the BBW
	void SpeedUpOnLOS() {
		if (headingright == true)
		{
			RaycastHit2D[] hit = Physics2D.RaycastAll (transform.position, Vector2.right, sightSideDistance);
			for (int i = 0; i < hit.Length; i++)
			{
				if (hit[i].collider.tag == "BBW")
				{
					speed = speed * losIncrease;
					chasing = true;
					break;
				}
				else 
				{
					speed = startSpeed;
					chasing = false;
				}
			}
		}
		if (headingright == false)
		{
			RaycastHit2D[] hit = Physics2D.RaycastAll (transform.position, -Vector2.right, sightSideDistance);
			for (int i = 0; i < hit.Length; i++)
			{
				if (hit[i].collider.tag == "BBW")
				{
					speed = speed * losIncrease;
					chasing = true;
					break;
				}
				else 
				{
					speed = startSpeed;
					chasing = false;
				}
			}
		}

	}

	// If the BBW comes within sightCloseDistance of WC, WC will turn around to face BBW
	void TurnAround ()
	{
		RaycastHit2D[] hitleft = Physics2D.RaycastAll (transform.position, -Vector2.right, sightCloseDistance);
		RaycastHit2D[] hitright = Physics2D.RaycastAll (transform.position, Vector2.right, sightCloseDistance);

		for (int i = 0; i < hitright.Length; i++)
		{
			if (hitright[i].collider.tag == "BBW")
			{
				headingright = true;
			}
		}

		for (int i = 0; i < hitleft.Length; i++)
		{
			if (hitleft[i].collider.tag == "BBW")
			{
				headingright = false;
			}
		}



	}

}