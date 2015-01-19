using UnityEngine;
using System.Collections;

public class MeleeSystem : MonoBehaviour {
	// Set in Unity
	public float theDamage; // Amount of damage done with each attack
	public float theDistance; // Range/distance of the boxcast/the attack

	public KeyCode attackClawKey; // The key to attack
	public KeyCode attackHuffKey; //Key to Huff and puff attack
	public KeyCode attackBiteKey; //Key to the Bite attack

	public KeyCode attackKeyUp; // The up key
	public KeyCode attackKeyDown; // The down key
	public KeyCode attackKeyLeft; // The left key
	public KeyCode attackKeyRight; // The right key
	public Material[] materials; // 0 normal, 1 attack

	//Claw attack code
	public GameObject sword;
	public float swordLength;

	//Huff and Puff attack
	public GameObject huffpuffL;
	public GameObject huffpuffR;
	public bool huffCooldown;
	
	//Bite Attack
	public GameObject bite;
	public float biteCooldown;
	private float biteTimer;


	void Start () 
	{
		huffCooldown = true;
		biteTimer = Time.time;
	}
	
	/*Rob J Update - Added Left and Right to the Input.GetKey due to how I now spawn attacks
	 Attacks are now spawned as their own objects. For example the claw attack will spawn a claw that does the attack
	 All attack damage and collision detection is handled by the claw object rather than here. This class only instantiates each attack :) */

	/* A few notes for future Rob(s):
	 * 
	 * 1. We use && game.object.layer ==12' alongside pad controls to ensure
	 * that it is player one who is attacking with the pad. This is because
	 * we only support one pad.
	 * 
	 * 2. We use the facingDirection bool to tell whether BBW is facing right 
	 * or left in cases where he is standing still. facingDirection essentially
	 * subs in for attackKeyRight and attackKeyLeft with an '||' in the if 
	 *  statements in these situations.
	 * 
	 * 3. In cases where we need to hear back from the attacks we instantiate,
	 * we set this object as a parent variable in the instantiated object 

	 */

	void Update () {
		// Which direction is BBW facing?
		bool facingDirection = gameObject.GetComponent<BBWController>().facingRight;

		//Claw Attack
		/* 
		 * 1. No cooldown on this attack, it can be spammed as much as you like
		 */
		//if (gameObject.layer == 12)
			if (Input.GetKeyDown (attackClawKey) || (Input.GetButtonDown("ClawBitePad") && gameObject.layer == 12))
			{

				if (Input.GetKey (attackKeyUp) || (Input.GetAxis("Vertical_PLR1") > 0 && gameObject.layer == 12))
				{
					//DoMelee (materials[1], transform.position, collider2D.bounds.size, 0f, transform.up, theDistance, theDamage);
					Instantiate(sword, new Vector3(transform.position.x, transform.position.y+swordLength, transform.position.z), Quaternion.Euler(new Vector3(0, 0, 100)));
					//Instantiate(bite, new Vector3(transform.position.x, transform.position.y+swordLength, transform.position.z), Quaternion.Euler(new Vector3(0, 0, 100)));
				}
				if (Input.GetKey (attackKeyDown) || (Input.GetAxis("Vertical_PLR1") < 0 && gameObject.layer == 12))
				{
					//DoMelee (materials[1], transform.position, collider2D.bounds.size, 0f, -transform.up, theDistance, theDamage);
					Instantiate(sword, new Vector3(transform.position.x, transform.position.y-swordLength, transform.position.z), Quaternion.Euler(new Vector3(0, 0, -100)));
					//Instantiate(bite, new Vector3(transform.position.x, transform.position.y-swordLength, transform.position.z), Quaternion.Euler(new Vector3(0, 0, -100)));
				}
				if (Input.GetKey (attackKeyRight) || (Input.GetAxis("Horizontal_PLR1") > 0 && gameObject.layer == 12) || (facingDirection == true && (!Input.GetKey (attackKeyUp) && !Input.GetKey (attackKeyDown))))
				{
					//DoMelee (materials[1], transform.position, collider2D.bounds.size, 0f, -transform.up, theDistance, theDamage);
					Instantiate(sword, new Vector3(transform.position.x+swordLength, transform.position.y, transform.position.z), Quaternion.Euler(new Vector3(0, 0, 0)));
					//Instantiate(huffpuffR, new Vector3(transform.position.x+swordLength, transform.position.y, transform.position.z), Quaternion.Euler(new Vector3(0, 0, 0)));
				}
				if (Input.GetKey (attackKeyLeft) || (Input.GetAxis("Horizontal_PLR1") < 0 && gameObject.layer == 12) || (facingDirection == false && (!Input.GetKey (attackKeyUp) && !Input.GetKey (attackKeyDown))))
				{
					//DoMelee (materials[1], transform.position, collider2D.bounds.size, 0f, -transform.up, theDistance, theDamage);
					Instantiate(sword, new Vector3(transform.position.x-swordLength, transform.position.y, transform.position.z), Quaternion.Euler(new Vector3(0, 0, 0)));
					//Instantiate(huffpuffL, new Vector3(transform.position.x-swordLength, transform.position.y, transform.position.z), Quaternion.Euler(new Vector3(0, 0, 0)));
				}
			}
		//}
		//Huff Attack
		/* 
		 * 1. Cooldown is controlled by the huffCooldown bool
		 * 
		 * 2. When we instantiate the huff object we set this object 
		 * as the parent so that huff can set huffCooldown back to true
		 * when it destroys itself
		 * 
		 */
		//if (gameObject.layer == 12) {
			if (Input.GetKeyDown (attackHuffKey)|| (Input.GetButtonUp("HuffPad") && gameObject.layer == 12))
			{
				if (huffCooldown == true) 
				{
					if (Input.GetKey (attackKeyRight) || (Input.GetAxis("Horizontal_PLR1") > 0 && gameObject.layer == 12) || facingDirection == true)
					{
						huffCooldown = false;
						GameObject newHuff = (GameObject)Instantiate(huffpuffR, new Vector3(transform.position.x+swordLength, transform.position.y, transform.position.z), Quaternion.Euler(new Vector3(0, 0, 0)));
						newHuff.GetComponent<HuffNPuff>().parentBBW = gameObject;

					}
					if (Input.GetKey (attackKeyLeft)|| (Input.GetAxis("Horizontal_PLR1") < 0 && gameObject.layer == 12) || facingDirection == false)
					{
						huffCooldown = false;
						GameObject newHuff = (GameObject)Instantiate(huffpuffL, new Vector3(transform.position.x-swordLength, transform.position.y, transform.position.z), Quaternion.Euler(new Vector3(0, 0, 0)));
						newHuff.GetComponent<HuffNPuff>().parentBBW = gameObject;
					}
				}
			}
		//}
		//Bite Attack
		/* 
		 * 1. Cooldown is controlled by the biteCooldown public variable
		 * 
		 * 2. When we instantiate the Bite object we set this object as 
		 * the parent so that Bite can send stuff back to it (health)
		 * 
		 */
		//if (gameObject.layer == 12) { //Layer 12 = player 1
			if (Input.GetKeyDown (attackBiteKey)|| (Input.GetButtonUp("BitePad") && gameObject.layer == 12))
			{
				
				if (Time.time >= biteTimer)
				{
					biteTimer = Time.time + biteCooldown;
					if (Input.GetKey (attackKeyUp) || (Input.GetAxis("Vertical_PLR1") > 0 && gameObject.layer == 12))
					{
						GameObject newBite = (GameObject)Instantiate(bite, new Vector3(transform.position.x, transform.position.y+swordLength, transform.position.z), Quaternion.Euler(new Vector3(0, 0, 100)));
						newBite.GetComponent<BiteAttack>().parentBBW = gameObject;
					}
					if (Input.GetKey (attackKeyDown) || (Input.GetAxis("Vertical_PLR1") < 0 && gameObject.layer == 12))
					{
						GameObject newBite = (GameObject)Instantiate(bite, new Vector3(transform.position.x, transform.position.y-swordLength, transform.position.z), Quaternion.Euler(new Vector3(0, 0, -100)));
						newBite.GetComponent<BiteAttack>().parentBBW = gameObject;
					}
					if (Input.GetKey (attackKeyRight) || (Input.GetAxis("Horizontal_PLR1") > 0 && gameObject.layer == 12) || (facingDirection == true && (!Input.GetKey (attackKeyUp) && !Input.GetKey (attackKeyDown))))
					{
						GameObject newBite = (GameObject)Instantiate(bite, new Vector3(transform.position.x+swordLength, transform.position.y, transform.position.z), Quaternion.Euler(new Vector3(0, 0, 0)));
						newBite.GetComponent<BiteAttack>().parentBBW = gameObject;
					}
					if (Input.GetKey (attackKeyLeft) || (Input.GetAxis("Horizontal_PLR1") < 0 && gameObject.layer == 12) || (facingDirection == false && (!Input.GetKey (attackKeyUp) && !Input.GetKey (attackKeyDown))))
					{
						GameObject newBite = (GameObject)Instantiate(bite, new Vector3(transform.position.x-swordLength, transform.position.y, transform.position.z), Quaternion.Euler(new Vector3(0, 0, 0)));
						newBite.GetComponent<BiteAttack>().parentBBW = gameObject;
					}
				}

			}
		//}
	}


	/* Boxcast melee attack method 
	void DoMelee (Material skin, Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, float damage)
	{
		gameObject.renderer.material = skin;
		RaycastHit2D hit = Physics2D.BoxCast(origin, size, angle, direction, distance);
		if (hit)
		{
			hit.transform.SendMessage ("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
		}
	}*/
}

