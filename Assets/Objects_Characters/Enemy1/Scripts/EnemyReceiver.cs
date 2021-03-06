﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnemyReceiver : MonoBehaviour {
	// Set in Unity
	public float health; // Health value
	public float healthDecay; // Value health decays by each update that it is above the starting value

	// Leave alone
	public float startingHealth; // The original health value
	public GameObject spawnParent; // If enemy, which spawn created you?
	public bool invuln; // Is the character invulnerable atm?

	// Save the initial health value
	void Start() 
	{
		invuln = false;
		startingHealth = health;
	}

	void Update()
	{
		//If health is above the initial value, apply health decay (BBW thing)
		if (health > startingHealth) 
		{
			health = health - healthDecay;
		}
	}
	
	public void ApplyDamage(float theDamage)
	{
		if (invuln == false)
		{
			// Load in sound manager
			SoundManager play = GameObject.Find("SoundManager").gameObject.GetComponent<SoundManager>();

			// Deplete health by theDamage
			health = health - theDamage;

			// If this is a player char, tell the Health Bar about the change in health
			if (gameObject.layer == 12 || gameObject.layer == 13)
			{
				gameObject.GetComponentInChildren<HealthBar>().SendMessage("TakeDamage", health, SendMessageOptions.DontRequireReceiver);
			}


			// Cause enemy to lose all applied force and movement
			transform.GetComponent<Rigidbody2D>().isKinematic = true;
			transform.GetComponent<Rigidbody2D>().isKinematic = false;

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

				// Let the spawn point that birthed this character (if enemy)
				// know that it has been destroyed. 
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
					case "WitchSpawn":
						spawnParent.GetComponent<WitchSpawn> ().child--;
						break;
					}
				}

				// If this is a player-character, then the game is over.
				if(gameObject.name == "BBW" || gameObject.name == "LRRH")
				{
					//Application.LoadLevel(6);
					SceneManager.LoadScene (6);
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
}
