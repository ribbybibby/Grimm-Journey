using UnityEngine;
using System.Collections;

public class WoodSpawn : MonoBehaviour {

	// Edit in Unity
	public int childLimit;
	public GameObject woody; // Woodcutter prefab
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
		timerUpdate = Time.time + timer;

	}
	
	void FixedUpdate () {
		bbwDistance = Vector3.Distance(gameObject.transform.position, bbw.gameObject.transform.position);
		lrrhDistance = Vector3.Distance(gameObject.transform.position, lrrh.gameObject.transform.position);
		if ((gameObject.transform.position.y - bbw.gameObject.transform.position.y) >= -1.5f && child < childLimit 
		    && Time.time >= timerUpdate && bbwDistance > distanceFromPlayers && lrrhDistance > distanceFromPlayers) 
		{
			GameObject newWood = (GameObject)Instantiate(woody, gameObject.transform.position, Quaternion.identity);
			newWood.GetComponent<EnemyReceiver>().spawnParent = gameObject;
			newWood.name = "WoodCutter";
			child++;
			timerUpdate = Time.time + timer;
		}
	}
}