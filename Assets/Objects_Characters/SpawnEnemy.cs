using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour {

	// Set in unity
	public GameObject Cena; // Object to spawn
	public float xRight; // Right bound
	public float xLeft; // Left bound
	public float yTop; // Top bound
	public float yBottom; // Bottom bound
	public float chance; // How many frames between each spawn, on average?	

	void Update ()
	{
		// Chance to Spawn a new object every frame at a point in the designated range
		float rndA = Random.Range (0f, chance);
		if (rndA < 5) 
		{
			float x = Random.Range (xLeft, xRight);
			float y = Random.Range (yBottom, yTop);
			Instantiate (Cena, new Vector3 (x,y,-1), Quaternion.identity);
		}
	}
}
