using UnityEngine;
using System.Collections;

public class HuffNPuff : MonoBehaviour {

	public float killDelay;
	public string direction;

	// Use this for initialization
	void Start () {
		StartCoroutine(waitThenKill());
		huffBlast(direction);
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if(other.name == "Enemy"){
			Debug.Log("Test collision");
			other.transform.SendMessage("ApplyDamage", 20.0F, SendMessageOptions.DontRequireReceiver);
		}
	}

	void huffBlast(string direction){
		if(direction == "Left"){
			rigidbody2D.AddForce (new Vector2(-400F,0));
		}

		if(direction == "Right"){
			rigidbody2D.AddForce (new Vector2(400F,0));
		}
	}

	IEnumerator waitThenKill(){
		yield return new WaitForSeconds(killDelay);
		Destroy(gameObject);
	}
}