using UnityEngine;
using System.Collections;

public class ChargeEnemy : MonoBehaviour {

	// PUBLIC: Set in Unity
	public int speed; // Speed of the character
	public float sightSideDistance; // Line of sight for charge (speed increase)
	public float sightCloseDistance; // Line of sight for turning around
	public float damage; // Damage done
	public bool headingright; // Bool that controls direction of travel

	// PUBLIC: Axe attack
	public GameObject Axe; // Axe that we instantiate
	public float rangeOfAxe; // Range of the Axe attack
	public float axeDelay; // Delay on the Axe attack
	
	// PUBLIC: Charge
	public float chargeSpeed; // How fast should the enemy charge forward?
	public int chargeBuildUp; // How long should the enemy wait before charging?
	public int cooldownLength;  // How long should the enemy wait after charging?
	public float chargeBackup; // How far to back up from the char before charging
	public float backUpSpeed; // How fast to back up

	// PRIVATE: Used when interacting with BBW
	private bool chasing; // Is the enemy chasing a char?
	private GameObject bbw; // The BBW character

	// PRIVATE: Charge variables
	private int charge; // Int that we increment, controls the charge
	private bool charged; // Has the enemy charged?
	private int cooldown; // Cooldown after charging
	private bool seen; // Has the enemy spotted the char? 
	private float targetX; // The target spot on the X axis to charge at

	// PRIVATE: Charge variables when backed into a wall
	private bool wallcharge; // Should the enemy charge out of the wall?
	private bool wallseen; // Has the enemy spotted the point to charge to from the wall?
	private int wallcooldown; // Cooldown on the wall charge

	// PRIVATE: Axe attack
	private bool axeReady; // Is the enemy ready for another Axe attack?

	//Used to load in the textures for the swap (left text for moving left / right text for moving right)
	Texture leftTexture;
	Texture rightTexture;

	void Start() {
		// Ignore other enemies
		Physics2D.IgnoreLayerCollision (11, 11);

		// Variable for controlling whether the enemy is pursuing its target
		chasing = false;

		// Variables for the charge attack
		charge = 0;
		cooldown = 0;
		seen = false;
		charged = false;

		// Variables for the charge attack out of the wall.
		wallcharge = false;
		wallcooldown = 0;
		wallseen = false;

		// Ready for the axe attack
		axeReady = true;

		//Loading in the textures :D
		leftTexture = Resources.Load ("Textures/woodsman", typeof(Texture)) as Texture;
		rightTexture = Resources.Load ("Textures/woodsman_Flip", typeof(Texture)) as Texture;
	}


	void Update () {
		// Find the BBW game object
		bbw = GameObject.FindGameObjectWithTag("BBW");

		// Fix rotation every update
		transform.eulerAngles = new Vector3(0,0,0);

		// If backed against a wall by the char, charge immediately at the char
		if (wallcharge == true)
		{
			WallCharge();
		}

		// If the charge has built up to chargeBuildUp, CHARGE!
		else if (charge >= chargeBuildUp) 
		{
			ChargeAttack();
		}

		// Else, 
		else
		{
			// Manages reactions to having the char in sight
			ReactBBW ();

			// Turns the enemy around when the char is a certain distance behind them
			TurnAround ();

			// Move the enemy around
			KeepOnMoving ();
		}
	}

	// Standard charge attack
	void ChargeAttack ()
	{
		// Idenitfy x-axis position of target char
		if (seen == false)
		{
			targetX = bbw.transform.position.x;
			seen = true;
		}

		// Charge forward at chargeSpeed (direction dependent on the way the enemy is facing)
		if (headingright == true && gameObject.transform.position.x < targetX && charged == false) 
		{
			transform.Translate (Vector2.right * chargeSpeed * Time.deltaTime); 
		}
		if (headingright == false && gameObject.transform.position.x > targetX && charged == false) 
		{
			transform.Translate (-Vector2.right * chargeSpeed * Time.deltaTime); 
		}

		// When we reach the target point, set charged to true
		if (headingright == true && gameObject.transform.position.x >= targetX)
		{
			charged = true;
		}

		if (headingright == false && gameObject.transform.position.x <= targetX)
		{
			charged = true;
		}

		// Once we reach the charge point, back off for the length of cooldownLength
		if (charged == true)
		{

			if (headingright == true && (gameObject.transform.position.x > targetX-(chargeBackup*2) || (Vector3.Distance(gameObject.transform.position, bbw.transform.position)-10) < chargeBackup))
			{
				transform.Translate (-Vector2.right * backUpSpeed * Time.deltaTime);
			}

			if (headingright == false && (gameObject.transform.position.x < targetX+(chargeBackup*2) || (Vector3.Distance(gameObject.transform.position, bbw.transform.position)-10) < chargeBackup))
			{
				transform.Translate (Vector2.right * backUpSpeed * Time.deltaTime);
			}

			cooldown++;
			if (cooldown > cooldownLength)
			{
				charge = 0;
				cooldown = 0;
				seen = false;
				charged = false;
			}
		}
	}

	// Charge when backed against the wall
	void WallCharge () 
	{
		// Idenitfy x-axis position of target char
		if (wallseen == false)
		{
			bbw = GameObject.FindGameObjectWithTag("BBW");
			targetX = bbw.transform.position.x;
			wallseen = true;
		}

		// Charge forward at chargeSpeed (direction dependent on the way the enemy is facing)
		if (headingright == true && gameObject.transform.position.x < targetX) 
		{
			transform.Translate (Vector2.right * chargeSpeed * Time.deltaTime); 
		}
		if (headingright == false && gameObject.transform.position.x > targetX) 
		{
			transform.Translate (-Vector2.right * chargeSpeed * Time.deltaTime); 
		}

		// Start running a cooldown from the minute the charge is initiated
		wallcooldown++;
		if (wallcooldown > cooldownLength)
		{
			wallcooldown = 0;
			wallseen = false;
			wallcharge = false;
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{

		// Turn around when hitting a wall or the edge of a platform
		if (col.gameObject.tag == "LeftBound" || (col.gameObject.tag == "LeftBoundPatrol" && chasing != true)) 
		{
			headingright = true;

			// If the char is in sight, charge!
			if (chasing == true)
			{
				wallcharge = true;
			}
		}

		// Turn around when hitting a wall or the edge of a platform
		if (col.gameObject.tag == "RightBound" || (col.gameObject.tag == "RightBoundPatrol" && chasing != true))
		{
			headingright = false;

			// If the char is in sight, charge!
			if (chasing == true)
			{
				wallcharge = true;
			}
		}

		// If charging, and the collider is a char, damage the char
		if ((charge >= chargeBuildUp || wallcharge == true) && charged == false && (col.gameObject.tag == "BBW" || col.gameObject.tag == "LRRH")) 
		{
			col.transform.SendMessage("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
		}
	}
	
	// Movement
	void KeepOnMoving ()
	{
		// Find BBW
		bbw = GameObject.FindGameObjectWithTag("BBW");

		// Backup if BBW is in sight (chasing) but the distance is less than chargeBackup
		if (chasing == true && (Vector3.Distance(gameObject.transform.position, bbw.transform.position)-10) < chargeBackup)
		{
			if (headingright == true) 
			{
				charge = 0;
				transform.Translate (-Vector2.right * backUpSpeed * Time.deltaTime);
				gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", rightTexture);
			}
			if (headingright == false) 
			{
				charge = 0;
				transform.Translate (Vector2.right * backUpSpeed * Time.deltaTime);
				gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", leftTexture);
			}

		}

		// Regular movement
		else if (headingright == true && chasing == false)
		{
			transform.Translate (Vector2.right * speed * Time.deltaTime);
			gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", rightTexture);
		}
		else if (headingright == false && chasing == false)
		{
			transform.Translate (-Vector2.right * speed * Time.deltaTime);
			gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", leftTexture);
		}

		// If chasing BBW and BBW is outside chargeBackup
		if (chasing == true && (Vector3.Distance(gameObject.transform.position, bbw.transform.position)-10) > chargeBackup)
		{
			charge++;
		}
	}

	// If BBW is within sightSideDistance, then chasing = true
	void ReactBBW() {
		if (headingright == true)
		{
			RaycastHit2D[] hit = Physics2D.RaycastAll (transform.position, Vector2.right, sightSideDistance);
			for (int i = 0; i < hit.Length; i++)
			{
				if (hit[i].collider.tag == "BBW")
				{
					chasing = true;
					// Axe attack if char comes too close
					if ((Vector3.Distance (hit[i].collider.transform.position, gameObject.transform.position)-10) < 0.1f && axeReady == true)
					{
						Instantiate(Axe, new Vector2(transform.position.x+rangeOfAxe, transform.position.y), Quaternion.Euler(new Vector3(0, 0, 40)));
						StartCoroutine(waitAxe());
					}
					break;
				}
				else 
				{
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
					chasing = true;
					// Axe attack if char comes too close
					if ((Vector3.Distance (hit[i].collider.transform.position, gameObject.transform.position)-10) < 0.1f && axeReady == true)
					{
						Instantiate(Axe, new Vector2(transform.position.x-rangeOfAxe, transform.position.y), Quaternion.Euler(new Vector3(0, 0, 40)));
						StartCoroutine(waitAxe());
					}
					break;
				}
				else 
				{
					chasing = false;
				}
			}
		}

	}

	// If the BBW comes within sightCloseDistance of the enemy, BBW 
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

	// Cooldown on the Axe attack
	IEnumerator waitAxe(){
		axeReady = false;
		yield return new WaitForSeconds(axeDelay);
		axeReady = true;
	}

}