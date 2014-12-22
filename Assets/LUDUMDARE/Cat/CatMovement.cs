using UnityEngine;
using System.Collections;

public class CatMovement : MonoBehaviour {

	// Set these in Unity
	public int jumpForce; //Force of jump 
	public float speed; // Speed of movement
	public string targetTag; // The tag of the object to hunt (BBW)
	public string targetTagTwo; // Another tag to hunt (LRRH)
	public float killDelay; // Time before cat offs itself
	public int damage;

	//Used to load in the textures for the swap (left text for moving left / right text for moving right)
	public Texture leftTexture;
	public Texture rightTexture;
	
	// Start the count down to the cat's death, load in textures, make sure collisions with other
	// enemies are disabled
	void Start() {

		StartCoroutine(waitThenKill());
		//Loading in the textures :D
		leftTexture = Resources.Load ("Textures/flycatflip", typeof(Texture)) as Texture;
		rightTexture = Resources.Load ("Textures/flycat", typeof(Texture)) as Texture;

		Physics2D.IgnoreLayerCollision (11, 11);
		SoundManager play = GameObject.Find("SoundManager").gameObject.GetComponent<SoundManager>();
		play.PlayProjectileCat();

	}

	// Make sure cat stays upright
	void Update() {
		transform.eulerAngles = new Vector3(0,0,0);
	}

	// On collision, apply force upwards and then left/right depending on Player's relation to the cat on the X-axis
	void OnCollisionEnter2D(Collision2D col) {
		rigidbody2D.AddForce (Vector3.up * jumpForce);
		MoveEnemy (jumpForce);
		col.transform.SendMessage("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);

	}
	
	// If the cat gets stuck for too long on any surface, we give it a strong force 
	//towards the player to try and dislodge it 
	void OnCollisionStay2D(Collision2D col) {
		float rndNo = Random.Range (1, 20);
		if (rndNo == 3) {
			MoveEnemy(jumpForce * 1.5f);
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
			rigidbody2D.AddForce (Vector3.right * value);
		}
		if (targetdiffx < 0f) 
		{
			gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", leftTexture);
			rigidbody2D.AddForce (Vector3.left * value);
		}	
		if (targetdiffy > 0f) 
		{
			rigidbody2D.AddForce (Vector3.up * value);
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
