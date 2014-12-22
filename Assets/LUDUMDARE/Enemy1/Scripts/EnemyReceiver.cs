using UnityEngine;
using System.Collections;

public class EnemyReceiver : MonoBehaviour {
	public float health;
	
	public void ApplyDamage(float theDamage)
	{
		// Load in sound manager
		SoundManager play = GameObject.Find("SoundManager").gameObject.GetComponent<SoundManager>();

		// Lose health
		health = health - theDamage;

		// Cause enemy to lose all applied force and movement
		transform.rigidbody2D.isKinematic = true;
		transform.rigidbody2D.isKinematic = false;

		// If health is depleted, play kill sound 
		if (health <= 0) 
		{
			switch (gameObject.name)
			{
			case "LRRH":
				play.PlayLRRHKilled ();
				break;
			case "BBW":
				play.PlayBBWKilled ();
				break;
			case "WoodCutter":
				play.PlayWCKilled ();
				break;
			case "Witch":
				play.PlayCWKilled ();
				break;
			case "Troll":
				play.PlayTKilled ();
				break;
			}

			Destroy(gameObject);

			if(gameObject.name == "BBW" || gameObject.name == "LRRH")
			{
				Application.LoadLevel(0);
			}
		}

		// Else, play hit sound
		else
		{
			switch (gameObject.name)
			{
			case "LRRH":
				play.PlayLRRHHit ();
				break;
			case "BBW":
				play.PlayBBWHit ();
				break;
			case "WoodCutter":
				play.PlayWCHit ();
				break;
			case "Witch":
				play.PlayCWHurt ();
				break;
			case "Troll":
				play.PlayTHit ();
				break;
			}
		}
	}
}
