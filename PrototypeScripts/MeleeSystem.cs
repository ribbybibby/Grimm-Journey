using UnityEngine;
using System.Collections;

public class MeleeSystem : MonoBehaviour {
	// Set in Unity
	public float theDamage; // Amount of damage done with each attack
	public float theDistance; // Range/distance of the boxcast/the attack
	public KeyCode attackKey; // The key to attack
	public KeyCode attackKeyUp; // The up key
	public KeyCode attackKeyDown; // The down key
	public KeyCode attackKeyLeft; // The left key
	public KeyCode attackKeyRight; // The right key
	public Material[] materials; // 0 normal, 1 attack


		
	// Press attackKeyUp to attack in the direction you're facing
	// We do a directional attack if Up or Down are held !!! CURRENTLY NOT WORKING IF THE CHARACTER TIPS OVER !!!
	// ^ TODO: Possible workaround would be to flip the character back to Y orientation on hitting the ground 
	void Update () {
		gameObject.renderer.material = materials[0];
		if (Input.GetKeyDown (attackKey))
		{

			if (Input.GetKey (attackKeyUp))
			{
				DoMelee (materials[1], transform.position, collider2D.bounds.size, 0f, transform.up, theDistance, theDamage);
			}
			if (Input.GetKey (attackKeyDown))
			{
				DoMelee (materials[1], transform.position, collider2D.bounds.size, 0f, -transform.up, theDistance, theDamage);
			}
			else
			{
				DoMelee (materials[1], transform.position, new Vector2 (collider2D.bounds.size.x, collider2D.bounds.size.y * 1.3f), 0f, transform.right, theDistance, theDamage);
			}	
		}

	}

	// Boxcast melee attack method 
	void DoMelee (Material skin, Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, float damage)
	{
		gameObject.renderer.material = skin;
		RaycastHit2D hit = Physics2D.BoxCast(origin, size, angle, direction, distance);
		if (hit)
		{
			hit.transform.SendMessage ("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
		}
	}
}

