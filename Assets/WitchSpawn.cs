using UnityEngine;
using System.Collections;

public class WitchSpawn : MonoBehaviour {

	// Edit in Unity
	public int childLimit;
	public GameObject witchy; // Woodcutter prefab
	public float timer;
	public float distanceFromPlayers;

	// Don't touch in unity
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
	
	// Update is called once per frame
	void Update () {
		bbwDistance = Vector3.Distance(gameObject.transform.position, bbw.gameObject.transform.position);
		lrrhDistance = Vector3.Distance(gameObject.transform.position, lrrh.gameObject.transform.position);
		if (child < childLimit && Time.time >= timerUpdate && bbwDistance > distanceFromPlayers && lrrhDistance > distanceFromPlayers)		
		{
			GameObject newWitch = (GameObject)Instantiate(witchy, gameObject.transform.position, Quaternion.identity);
			newWitch.GetComponent<EnemyReceiver>().spawnParent = gameObject;
			newWitch.name = "Witch";
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
