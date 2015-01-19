using UnityEngine;
using System.Collections;

public class TrollSpawn : MonoBehaviour {

	// Edit in Unity
	public int childLimit;
	public GameObject trolly; // Troll prefab
	public float timer;
	public float distanceFromPlayers;

	
	// Don't edit in Unity
	public int child;
	
	// Private vars
	private GameObject bbw;
	private GameObject lrrh;
	private float timerUpdate;
	private float bbwDistance;
	private float lrrhDistance;

	
	// Use this for initialization
	void Start () {
		bbw = GameObject.FindGameObjectWithTag ("BBW");
		lrrh = GameObject.FindGameObjectWithTag ("LRRH");
		timerUpdate = UpdateTimer (timer);
		child = 0;
	}

	// Fixed Update:
	/* We only spawn an enemy if:
	 * 1. LRRH is below or level with the spawn
	 * 2. The number of spawned children is lower than childLimit
	 * 3. The timer has proc'd
	 * 4. BBW and LRRH are distanceFromPlayers away from the spawn 
	 */
	void FixedUpdate () {
		bbw = GameObject.FindGameObjectWithTag ("BBW");
		lrrh = GameObject.FindGameObjectWithTag ("LRRH");
		bbwDistance = Vector3.Distance(gameObject.transform.position, bbw.gameObject.transform.position);
		lrrhDistance = Vector3.Distance(gameObject.transform.position, lrrh.gameObject.transform.position);
		if ((gameObject.transform.position.y - lrrh.gameObject.transform.position.y) >= -1.5f && child < childLimit 
		    && Time.time >= timerUpdate && bbwDistance > distanceFromPlayers && lrrhDistance > distanceFromPlayers)		
		{
			GameObject newTroll = (GameObject)Instantiate(trolly, gameObject.transform.position, Quaternion.identity);
			newTroll.GetComponent<EnemyReceiver>().spawnParent = gameObject;
			newTroll.name = "Troll";
			child++;
			timerUpdate = UpdateTimer (timer);
		}
	}

	// Update Timer:
	// Returns a value to set timerUpdate to
	// This is the current time + (timer value + the timer value divided by a random number between 1 and 5)
	public float UpdateTimer (float thetime) 
	{
		float rndNo = Random.Range(1,5);
		float newTime = Time.time + (thetime + (thetime / rndNo));
		return newTime;
	}
}
