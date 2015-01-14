using UnityEngine;
using System.Collections;

public class EnemyReceiver : MonoBehaviour {
	// Set in Unity
	public float health; // Health value
	public float healthDecay; // Value health decays by each update that it is above the starting value

	// Leave alone
	public float startingHealth; // The original health value
	public GameObject spawnParent; // If enemy, which spawn created you?

	// Save the initial health value
	void Start() 
	{
		startingHealth = health;
	}

	void Update()
	{
		// If health is above the initial value, apply health decay (BBW thing)
		if (health > startingHealth) 
		{
			health = health - healthDecay;
		}
	}
	
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

			if (spawnParent != null)
			{
				switch (spawnParent.name)
				{
				case "WoodSpawn":
					spawnParent.GetComponent<WoodSpawn> ().child--;
					break;
				case "TrollSpawn":
					spawnParent.GetComponent<TrollSpawn> ().child--;
					break;
				}
			}

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
