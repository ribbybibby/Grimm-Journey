using UnityEngine;
using System.Collections;

public class HuffNPuff : MonoBehaviour {

	public float huffForce;
	public float killDelay;
	public string direction;

	public GameObject parentBBW;

	// Use this for initialization
	void Start () {
		StartCoroutine(waitThenKill());
		huffBlast(direction);
		SoundManager play = GameObject.Find("SoundManager").gameObject.GetComponent<SoundManager>();
		play.PlayHuffAndPuff ();
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.layer == 11)
		{
			other.transform.SendMessage("ApplyDamage", 20.0F, SendMessageOptions.DontRequireReceiver);
		}
	}

	void huffBlast(string direction){
		if(direction == "Left"){
			rigidbody2D.AddForce (new Vector2(-huffForce,0));
		}

		if(direction == "Right"){
			rigidbody2D.AddForce (new Vector2(huffForce,0));
		}
	}

	IEnumerator waitThenKill(){
		yield return new WaitForSeconds(killDelay);
		parentBBW.GetComponent<MeleeSystem> ().huffCooldown = true;
		Destroy(gameObject);
	}
}