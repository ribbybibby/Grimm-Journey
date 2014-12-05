using UnityEngine;
using System.Collections;

public class MeleeSystem : MonoBehaviour {
	public float theDamage; // 
	public float theDistance; //
	public float theRadius; //
	public KeyCode attackKey; //
	public KeyCode attackKeyUp; //
	public KeyCode attackKeyDown; //
	public KeyCode attackKeyLeft; //
	public KeyCode attackKeyRight; //
	public Material[] materials; // 0 normal, 1 attack


		
	// Update is called once per frame
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

	// Boxcast 
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

