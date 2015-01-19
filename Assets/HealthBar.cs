using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
	public float hurtDelay;

	private float totalHealth;	
	private float totalBarSize;
	private float barToHealthRatio;
	private float newScale;
	private float currhealth;
	private bool hurt;

	// Use this for initialization
	void Start () {
		totalHealth = gameObject.GetComponentInParent<EnemyReceiver>().health;
		totalBarSize = gameObject.transform.localScale.x;
		currhealth = totalHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if (hurt == false)
		{
			renderer.material.color = Color.white;
		}
		
	}

	/* TAKE DAMAGE
	 * When the character takes damage, the updated value is sent to this script
	 * via a SendMessage to the TakeDamage method.
	 * 
	 * TakeDamage then does the following:
	 * 1. Makes the bar flash red via the render.material, the hurt bool
	 * and the timer Coroutine
	 * 
	 * 2. Resizes the x-axis scale of the bar proportional to the character's health
	 * 
	 */
	public void TakeDamage (float health){
		renderer.material.color = Color.red;
		hurt = true;
		StartCoroutine(waitWhileHurt());
		newScale = totalBarSize * (health/totalHealth);
		currhealth = health;

		if (newScale > totalBarSize)
		{
			newScale = totalBarSize;
		}
		gameObject.transform.localScale = new Vector3 (newScale, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
	}

	/* GAIN HEALTH
	 * Gain health works exactly like TakeDamage, except it is invoked
	 * by the proto-lifesteal of the Bite attack. The only difference code wise is that 
	 * Gain Health flashes green.
	 */

	public void GainHealth (float health){
		renderer.material.color = Color.green;
		hurt = true;
		StartCoroutine(waitWhileHurt());
		newScale = totalBarSize * (health/totalHealth);
		currhealth = health;

		if (newScale > totalBarSize)
		{
			newScale = totalBarSize;
		}
		gameObject.transform.localScale = new Vector3 (newScale, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
	}
	


	IEnumerator waitWhileHurt ()
	{
		yield return new WaitForSeconds(hurtDelay);
		hurt = false;
	}
}
