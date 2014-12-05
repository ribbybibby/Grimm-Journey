using UnityEngine;
using System.Collections;

public class EnemyReceiver : MonoBehaviour {
	public float health;
	public float jumpBack;

	void Start()
	{
			
	}

	public void ApplyDamage(float theDamage)
	{
		health = health - theDamage;
		transform.rigidbody2D.isKinematic = true;
		transform.rigidbody2D.isKinematic = false;
		//if (transform.eulerAngles.z > 0) 
		//{
		//	transform.rigidbody2D.AddForce (-Vector2.right * jumpBack);
		//}
		//else
		//{
		//	transform.rigidbody2D.AddForce (Vector2.right * jumpBack);
		//}

		if (health <= 0) 
		{
			Destroy(gameObject);
		}

	}
}
