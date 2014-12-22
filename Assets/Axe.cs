using UnityEngine;
using System.Collections;

public class Axe : MonoBehaviour {

	public float killDelay;
	public float damage;
	
	// Use this for initialization
	void Start () 
	{
		StartCoroutine(waitThenKill());
		SoundManager play = GameObject.Find("SoundManager").gameObject.GetComponent<SoundManager>();
		play.PlayWoodCutterAxe ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	void OnTriggerEnter2D(Collider2D other) 
	{
		if(other.gameObject.layer == 12 || other.gameObject.layer == 13)
		{
			other.transform.SendMessage("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
		}
	}
	
	IEnumerator waitThenKill(){
		yield return new WaitForSeconds(killDelay);
		Destroy(gameObject);
	}
}
