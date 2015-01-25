using UnityEngine;
using System.Collections;

public class WitchSpawn : MonoBehaviour {

	// Edit in Unity
	public int childLimit; // Number of witches that can be spawned and exist at once
	public GameObject witchy; // Witch prefab
	public float timer; // Cooldown time
	public float distanceFromPlayers; // Distance from the players at which a spawn can happen

	// Don't touch in unity
	public int child; // Current number of children of the spawn

	// Private vars
	private GameObject bbw; // BBW
	private GameObject lrrh; // LRRH
	private float bbwDistance; // BBW distance
	private float lrrhDistance; // LRRH distance
	private bool readyToSpawn; // Has the cooldown ran down?

	// Find BBW and LRRH, start the cooldown, set children to 0
	void Start () {
		bbw = GameObject.FindGameObjectWithTag ("BBW");
		lrrh = GameObject.FindGameObjectWithTag ("LRRH");
		StartCoroutine (spawnCooldown());
		child = 0;
	}
	
	// Update is called once per frame
	void Update () {
		// Work out the distance from both BBW and LRRH
		bbw = GameObject.FindGameObjectWithTag ("BBW");
		lrrh = GameObject.FindGameObjectWithTag ("LRRH");
		bbwDistance = Vector3.Distance(gameObject.transform.position, bbw.gameObject.transform.position);
		lrrhDistance = Vector3.Distance(gameObject.transform.position, lrrh.gameObject.transform.position);

		// If the number of children created by the spawn is less than the limit,
		// The cooldown has expired,
		// And the two chars are far enough away:
		// Spawn a witch.
		if (child < childLimit && readyToSpawn == true && bbwDistance > distanceFromPlayers && lrrhDistance > distanceFromPlayers)		
		{
			GameObject newWitch = (GameObject)Instantiate(witchy, gameObject.transform.position, Quaternion.identity);
			newWitch.GetComponent<EnemyReceiver>().spawnParent = gameObject;
			newWitch.name = "Witch";
			child++;
			StartCoroutine (spawnCooldown());
		}
	}

	// Cooldown that controls the readyToSpawn bool
	IEnumerator spawnCooldown(){
		readyToSpawn = false;
		float rndNo = Random.Range (1, 5);
		yield return new WaitForSeconds(timer + (timer / rndNo));
		readyToSpawn = true;
	}
}
