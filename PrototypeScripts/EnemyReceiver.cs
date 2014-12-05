using UnityEngine;
using System.Collections;

public class EnemyReceiver : MonoBehaviour {
	public float health;
	public float jumpBack;
	
	public void ApplyDamage(float theDamage)
	{
		health = health - theDamage;

		// Cause enemy to lose all applied force and movement
		transform.rigidbody2D.isKinematic = true;
		transform.rigidbody2D.isKinematic = false;

		if (health <= 0) 
		{
			Destroy(gameObject);
		}

	}
}
