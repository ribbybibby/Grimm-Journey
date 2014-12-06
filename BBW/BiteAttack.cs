using UnityEngine;
using System.Collections;

public class BiteAttack : MonoBehaviour {

	public float killDelay;
	public float damage;
	
	// Use this for initialization
	void Start () {
		StartCoroutine(waitThenKill());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if(other.name == "Enemy"){
			Debug.Log("Test collision");
			other.transform.SendMessage("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
		}
		//gameObject.transform.SendMessage("ApplyDamage", 5.0F, SendMessageOptions.DontRequireReceiver);
	}
	
	IEnumerator waitThenKill(){
		yield return new WaitForSeconds(killDelay);
		Destroy(gameObject);
	}
}
