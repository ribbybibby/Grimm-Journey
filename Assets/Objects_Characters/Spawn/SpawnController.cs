using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour {

	// Define in Unity
	public Transform[] spawnPoints; // Spawn point transforms
	public float woodInterval; // Interval between attempting to spawn a WoodCutter
	public float witchInterval; // Interval between attempting to spawn a Witch
	public float trollInterval; // Interval between attempting to spawn a Troll
	public float aboveWood; // How far above the player(s) should a Wood spawn be?
	public float aboveWitch; // How far above the player(s) should a Witch spawn be?
	public float aboveTroll; // How far above the player(s) should a Troll spawn be?
	public GameObject woody; // Woodcutter prefab
	public GameObject witch; // Witch prefab
	public GameObject troll; // Troll prefab

	// Private
	private float woodtimer;
	private float witchtimer;
	private float trolltimer;
	private RaycastHit2D[] hitter;
	private RaycastHit2D[] hitter2;

	// Set the timer for each spawn attempt
	void Start () {
		woodtimer = Time.time + woodInterval;
		witchtimer = Time.time + witchInterval;
		trolltimer = Time.time + trollInterval;
	}
	
	// Fixed Update, for the sake of the timers
	void FixedUpdate () 
	{
		//Debug.Log ("Time:" + Time.time);
		//Debug.Log ("Wood Timer:" + woodtimer);
		//Debug.Log ("Wood Interval:" + woodInterval);

		// Cast a ray out to the left and right of the spawn points, if it hits
		// a player char, instantiate a new WoodCutter
		if (Time.time >= woodtimer)
		{
			//int rndNum = Random.Range(0,9);
			//if (rndNum < 6)
			//{
				Vector2 nearSpawn = GetClosestSpawn(aboveWood, "wood");
				hitter = Physics2D.RaycastAll(nearSpawn, Vector2.right, Mathf.Infinity);
				hitter2 = Physics2D.RaycastAll(nearSpawn, -Vector2.right, Mathf.Infinity);
				for (int i = 0; i < hitter.Length; i++)
				{
					if (hitter[i].collider.tag == "BBW" || hitter[i].collider.tag == "LRRH")
					{

						SoundManager play = GameObject.Find("SoundManager").gameObject.GetComponent<SoundManager>();
						play.PlayPlayerSpawn();
						GameObject newWood = (GameObject)Instantiate(woody, nearSpawn, Quaternion.identity);
						newWood.name = "WoodCutter";
					}
				}

				for (int i = 0; i < hitter2.Length; i++)
				{
					if (hitter2[i].collider.tag == "BBW" || hitter2[i].collider.tag == "LRRH")
					{
						SoundManager play = GameObject.Find("SoundManager").gameObject.GetComponent<SoundManager>();
						play.PlayPlayerSpawn();
						GameObject newWood = (GameObject)Instantiate(woody, nearSpawn, Quaternion.identity);
						newWood.name = "WoodCutter";
					}
					
				}
			//}	
			woodtimer = Time.time + woodInterval;
		}


		// Spawn witch at the Vector returned by nearSpawn
		if (Time.time >= witchtimer)
		{
			int rndNum = Random.Range(0,9);
			if (rndNum < 3)
			{
				SoundManager play = GameObject.Find("SoundManager").gameObject.GetComponent<SoundManager>();
				play.PlayPlayerSpawn();
				Vector3 nearSpawn = GetClosestSpawn(aboveWitch, "witch");
				GameObject newWitch = (GameObject)Instantiate(witch, nearSpawn, Quaternion.identity);
				newWitch.name = "Witch";
			}
			witchtimer = Time.time + witchInterval;
		}

		// Spawn troll at the Vector returned by GetClosestSpawn 
		if (Time.time >= trolltimer)
		{
			int rndNum = Random.Range(0,9);
			if (rndNum < 2)
			{
				SoundManager play = GameObject.Find("SoundManager").gameObject.GetComponent<SoundManager>();
				play.PlayPlayerSpawn();
				Vector3 nearSpawn = GetClosestSpawn(aboveTroll, "troll");
				GameObject newTroll = (GameObject)Instantiate(troll, nearSpawn, Quaternion.identity);
				newTroll.name = "Troll";
			}
			trolltimer = Time.time + trollInterval;
		}

	}

	// Find the closest valid Spawn point for each enemy type
	public Vector2 GetClosestSpawn(float distanceAbove, string enemytype) {
		GameObject bbw;
		GameObject lrrh;
		bbw = GameObject.FindGameObjectWithTag ("BBW");
		lrrh = GameObject.FindGameObjectWithTag ("LRRH");
		Vector2 closestSpawn;
		closestSpawn = new Vector2(transform.position.x, transform.position.y);


		// For the witch, we find the middle vector between BBW and LRRH and only return a Vector 
		//if it is distanceAbove the midVector
		if (enemytype == "witch" || enemytype == "troll") 
		{
			Vector2 midVector = bbw.transform.position + lrrh.transform.position;

			foreach (Transform point in spawnPoints)
			{
				if (closestSpawn == new Vector2 (transform.position.x, transform.position.y))
				{
					closestSpawn = point.position;
				}
				else if (Vector2.Distance(midVector, point.position) <= Vector2.Distance(midVector, closestSpawn) & (point.position.y - midVector.y) > distanceAbove)
				{
					closestSpawn = point.position;
				}
			}

		}

		// For the woodcutter we find the closest spawn to BBW
		if (enemytype == "wood")
		{
			foreach (Transform point in spawnPoints)
			{
				if (closestSpawn == new Vector2 (transform.position.x, transform.position.y))
				{
					closestSpawn = point.position;
				}
				else if (Vector3.Distance(bbw.transform.position, point.position) <= Vector3.Distance(bbw.transform.position, closestSpawn) & (point.position.y - bbw.transform.position.y) > distanceAbove)
				{
					closestSpawn = point.position;
				}
			}

	
		}

		return closestSpawn;
	}
}