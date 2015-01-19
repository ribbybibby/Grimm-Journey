using UnityEngine;
using System.Collections;

public class BiteAttack : MonoBehaviour {

	public float lifestealDivider; // How much should we divide the enemy's health by before giving it back to BBW? 
	public float killDelay;// Delay before destroying the bite object
	public float damage; // Damage this attack does to enemies
	public GameObject parentBBW; // The object that birthed this Bite attack (BBW probably)

	private float parentHealthStart;
	private float parentHealthNew;
	private float parentHealthNow;
	
	// Use this for initialization
	void Start () {
		SoundManager test = GameObject.Find("SoundManager").gameObject.GetComponent<SoundManager>();
		test.PlayBite ();
		parentHealthStart = parentBBW.GetComponent<EnemyReceiver> ().startingHealth;
		StartCoroutine(waitThenKill());
	}

	// Attack
	/*
	 * If the object we hit is an enemy we:
	 * 1. Gift a fraction of the enemy's total health back to BBW
	 * 2. Apply 'damage' to the enemy
	 * 
	 */
	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.layer == 11)
		{
			parentHealthNow = parentBBW.GetComponent<EnemyReceiver>().health;
			parentHealthNew = parentHealthNow + (other.GetComponent<EnemyReceiver>().startingHealth/lifestealDivider);
			parentBBW.GetComponent<EnemyReceiver>().health = parentHealthNew;
			parentBBW.GetComponentInChildren<HealthBar>().SendMessage("GainHealth", parentBBW.GetComponent<EnemyReceiver>().health, SendMessageOptions.DontRequireReceiver);
			other.transform.SendMessage("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
		}
		//gameObject.transform.SendMessage("ApplyDamage", 5.0F, SendMessageOptions.DontRequireReceiver);
	}
	
	IEnumerator waitThenKill(){
		yield return new WaitForSeconds(killDelay);
		Destroy(gameObject);
	}
}
