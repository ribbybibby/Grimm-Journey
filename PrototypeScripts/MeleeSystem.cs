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

	//Bite Attack
	public GameObject bite;


		
	// Press attackKeyUp to attack in the direction you're facing
	// We do a directional attack if Up or Down are held !!! CURRENTLY NOT WORKING IF THE CHARACTER TIPS OVER !!!
	// ^ TODO: Possible workaround would be to flip the character back to Y orientation on hitting the ground 

	/*Rob Update - Added Left and Right to the Input.GetKey due to how I now spawn attacks
	 Attacks are now spawned as their own objects. For example the claw attack will spawn a claw that does the attack
	 All attack damage and collision detection is handled by the claw object rather than here. This class only instantiates each attack :) */
	void Update () {
		Debug.Log(Input.GetAxis("ClawBitePad"));
		//Claw Attack
		if (Input.GetKeyDown (attackClawKey) || Input.GetButtonDown("ClawBitePad"))
		{

			if (Input.GetKey (attackKeyUp) || Input.GetAxis("Vertical_PLR1") > 0)
			{
				//DoMelee (materials[1], transform.position, collider2D.bounds.size, 0f, transform.up, theDistance, theDamage);
				Instantiate(sword, new Vector3(transform.position.x, transform.position.y+swordLength, transform.position.z), Quaternion.Euler(new Vector3(0, 0, 100)));
				//Instantiate(bite, new Vector3(transform.position.x, transform.position.y+swordLength, transform.position.z), Quaternion.Euler(new Vector3(0, 0, 100)));
			}
			if (Input.GetKey (attackKeyDown) || Input.GetAxis("Vertical_PLR1") < 0)
			{
				//DoMelee (materials[1], transform.position, collider2D.bounds.size, 0f, -transform.up, theDistance, theDamage);
				Instantiate(sword, new Vector3(transform.position.x, transform.position.y-swordLength, transform.position.z), Quaternion.Euler(new Vector3(0, 0, -100)));
				//Instantiate(bite, new Vector3(transform.position.x, transform.position.y-swordLength, transform.position.z), Quaternion.Euler(new Vector3(0, 0, -100)));
			}
			if (Input.GetKey (attackKeyRight) || Input.GetAxis("Horizontal_PLR1") > 0)
			{
				//DoMelee (materials[1], transform.position, collider2D.bounds.size, 0f, -transform.up, theDistance, theDamage);
				Instantiate(sword, new Vector3(transform.position.x+swordLength, transform.position.y, transform.position.z), Quaternion.Euler(new Vector3(0, 0, 0)));
				//Instantiate(huffpuffR, new Vector3(transform.position.x+swordLength, transform.position.y, transform.position.z), Quaternion.Euler(new Vector3(0, 0, 0)));
			}
			if (Input.GetKey (attackKeyLeft) || Input.GetAxis("Horizontal_PLR1") < 0)
			{
				//DoMelee (materials[1], transform.position, collider2D.bounds.size, 0f, -transform.up, theDistance, theDamage);
				Instantiate(sword, new Vector3(transform.position.x-swordLength, transform.position.y, transform.position.z), Quaternion.Euler(new Vector3(0, 0, 0)));
				//Instantiate(huffpuffL, new Vector3(transform.position.x-swordLength, transform.position.y, transform.position.z), Quaternion.Euler(new Vector3(0, 0, 0)));
			}
		}
		//Huff Attack
		if (Input.GetKeyDown (attackHuffKey)|| Input.GetButtonUp("HuffPad"))
		{
			if (Input.GetKey (attackKeyRight) || Input.GetAxis("Horizontal_PLR1") > 0)
			{
				Instantiate(huffpuffR, new Vector3(transform.position.x+swordLength, transform.position.y, transform.position.z), Quaternion.Euler(new Vector3(0, 0, 0)));
			}
			if (Input.GetKey (attackKeyLeft)|| Input.GetAxis("Horizontal_PLR1") < 0)
			{
				Instantiate(huffpuffL, new Vector3(transform.position.x-swordLength, transform.position.y, transform.position.z), Quaternion.Euler(new Vector3(0, 0, 0)));
			}
		}
		//Bite Attack
		if (Input.GetKeyDown (attackBiteKey)|| Input.GetButtonUp("BitePad"))
		{
			
			if (Input.GetKey (attackKeyUp) || Input.GetAxis("Vertical_PLR1") > 0)
			{
				Instantiate(bite, new Vector3(transform.position.x, transform.position.y+swordLength, transform.position.z), Quaternion.Euler(new Vector3(0, 0, 100)));
			}
			if (Input.GetKey (attackKeyDown) || Input.GetAxis("Vertical_PLR1") < 0)
			{
				Instantiate(bite, new Vector3(transform.position.x, transform.position.y-swordLength, transform.position.z), Quaternion.Euler(new Vector3(0, 0, -100)));
			}
			if (Input.GetKey (attackKeyRight) || Input.GetAxis("Horizontal_PLR1") > 0)
			{
				Instantiate(bite, new Vector3(transform.position.x+swordLength, transform.position.y, transform.position.z), Quaternion.Euler(new Vector3(0, 0, 0)));
			}
			if (Input.GetKey (attackKeyLeft) || Input.GetAxis("Horizontal_PLR1") < 0)
			{
				Instantiate(bite, new Vector3(transform.position.x-swordLength, transform.position.y, transform.position.z), Quaternion.Euler(new Vector3(0, 0, 0)));
			}

		}
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

