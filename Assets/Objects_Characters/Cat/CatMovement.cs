using UnityEngine;
using System.Collections;

public class CatMovement : MonoBehaviour {

	// Set these in Unity
	public int jumpForce; //Force of jump 
	public float speed; // Speed of movement
	public string targetTag; // The tag of the object to hunt (BBW)
	public string targetTagTwo; // Another tag to hunt (LRRH)
	public float killDelay; // Time before cat offs itself
	public int damage; // Damage done by the cat
	public float attackDelay; // Time between attacks

	//Used to load in the textures for the swap (left text for moving left / right text for moving right)
	public Texture leftTexture;
	public Texture rightTexture;

	// Bool that controls whether we are ready for another attack
	private bool readyToAttack;
	
	// Start the count down to the cat's death, load in textures, make sure collisions with other
	// enemies are disabled
	void Start() {
		// Countdown to the Cat's auto-death begins
		StartCoroutine(waitThenKill());

		//Loading in the textures :D
		leftTexture = Resources.Load ("Textures/flycatflip", typeof(Texture)) as Texture;
		rightTexture = Resources.Load ("Textures/flycat", typeof(Texture)) as Texture;

		// Cat's attack is activated, 
		// We make sure that objects in the Enemy layer ignore each other,
		// We find the soundmanager,
		// And then play the cat noise.
		readyToAttack = true;
		Physics2D.IgnoreLayerCollision (11, 11);
		SoundManager play = GameObject.Find("SoundManager").gameObject.GetComponent<SoundManager>();
		play.PlayProjectileCat();

	}

	// Make sure cat stays upright
	void Update() {
		transform.eulerAngles = new Vector3(0,0,0);
	}

	// On hitting the floor, apply force upwards and then left/right depending on Player's relation to the cat on the X-axis
	// Also apply damage, if the timer on the attack has ran down
	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Ground")
		{
			GetComponent<Rigidbody2D>().AddForce (Vector3.up * jumpForce);
			MoveEnemy (jumpForce);
		}
	}

	// On collision with BBW or LRRH, apply damage
	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "BBW" || col.gameObject.tag == "LRRH")
		{
			if (readyToAttack == true)
			{
				col.transform.SendMessage("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
				StartCoroutine(waitForAttack());
			}
		}
	}
	
	// If the cat gets stuck for too long, we give it a strong force 
	//towards the player to try and dislodge it 
	void OnTriggerStay2D(Collider2D col) {
		if (col.tag == "Ground")
		{
			float rndNo = Random.Range (1, 20);
			if (rndNo == 3) {
				MoveEnemy(jumpForce * 1.5f);
			}
		}
	}
	
	/* 
	 * The method for moving the enemy:
	 * # Subtract the target's x and y co-ords from the cat's
	 * # Then, depending on whether the target is left or right of the cat's
	 * current orientation, apply force in that direction
	 * # If the target is above the cat, we apply up force
	*/
	public void MoveEnemy(float value) 
	{
		float targetdiffx = (FindTarget (targetTag, targetTagTwo).transform.position.x) - transform.position.x;
		float targetdiffy = (FindTarget (targetTag, targetTagTwo).transform.position.y) - transform.position.y; 
		if (targetdiffx > 0f) 
		{	
			gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", rightTexture);
			GetComponent<Rigidbody2D>().AddForce (Vector3.right * value);
		}
		if (targetdiffx < 0f) 
		{
			gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", leftTexture);
			GetComponent<Rigidbody2D>().AddForce (Vector3.left * value);
		}	
		if (targetdiffy > 0f) 
		{
			GetComponent<Rigidbody2D>().AddForce (Vector3.up * value);
		}
	}
	
	// Finds both enemies, returns the nearest
	GameObject FindTarget (string tagone, string tagtwo){
		GameObject targetsone;
		GameObject targetstwo;
		targetsone = GameObject.FindGameObjectWithTag (tag);
		targetstwo = GameObject.FindGameObjectWithTag (tagtwo);

		GameObject target;
		if (Vector3.Distance(targetsone.transform.position, transform.position) > Vector3.Distance(targetstwo.transform.position, transform.position))
		{
			target = targetsone;
		}
		else
		{
			target = targetstwo;
		}
		return target;
	}

	// Timeout on the attack
	IEnumerator waitForAttack(){
		readyToAttack = false;
		yield return new WaitForSeconds(attackDelay);
		readyToAttack = true;
	}

	// Die, cat, die
	IEnumerator waitThenKill(){
		yield return new WaitForSeconds(killDelay);
		Destroy(gameObject);
	}
	
	// Die even more
	public void ApplyDamage() {
		Destroy (gameObject);
	}
}
