using UnityEngine;
using System.Collections;

public class SwordMechanic : MonoBehaviour {

	public float killDelay;
	public float damage;

	// Use this for initialization
	void Start () {
		StartCoroutine(waitThenKill());
		SoundManager test = GameObject.Find("SoundManager").gameObject.GetComponent<SoundManager>();
		test.PlayClaw ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//I am SO SO sorry for this horrible code but it's really late -.-
	//We should be renaming the Clone characters on instantiate in the SpawnController, not writing a giant OR clause like a lemon!
	//Note to self - fix this
	void OnTriggerEnter2D(Collider2D other) {
		if(other.name == "Cat" || other.name == "WoodCutter" || other.name == "Troll" || other.name == "Witch"){
			other.transform.SendMessage("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
		}
	}

	IEnumerator waitThenKill(){
		yield return new WaitForSeconds(killDelay);
		Destroy(gameObject);
	}
}
