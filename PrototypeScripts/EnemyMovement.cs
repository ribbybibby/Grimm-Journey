using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	// Set these in Unity
	public int jumpForce; //Force of jump 
	public float speed; // Speed of movement
	public float xRight; // Right bound
	public float xLeft; // Left bound
	public float yTop; // Top bound
	public float yBottom; // Bottom bound
	public string targetTag; // The tag of the object to hunt

	
	// On collision, apply force upwards and then left/right depending on the Weak Player's relation to this object on the X-axis
	void OnCollisionEnter2D(Collision2D col) {
		rigidbody2D.AddForce (Vector3.up * jumpForce);
		MoveEnemy (jumpForce);
	}

	// If this object gets stuck for too long on any surface, we give it a strong force 
	//towards the player to try and dislodge it 
	void OnCollisionStay2D(Collision2D col) {
		float rndNo = Random.Range (1, 20);
		if (rndNo == 3) {
			MoveEnemy(jumpForce * 1.5f);
		}
	}

	/* 
	 * The method for moving the enemy:
	 * # Subtract the target's x and y co-ords from the enemy's
	 * # Then, depending on whether the target is left or right of the enemy's
	 * current orientation, apply force in that direction
	 * # If the target is above the enemy, we apply up force
	*/
	public void MoveEnemy(float value) 
	{
		float targetdiffx = (FindTarget (targetTag).transform.position.x) - transform.position.x;
		float targetdiffy = (FindTarget (targetTag).transform.position.y) - transform.position.y; 
		if (targetdiffx > 0f) 
		{	
			transform.eulerAngles = new Vector2(0,0); 
			rigidbody2D.AddForce (Vector3.right * value);
		}
		if (targetdiffx < 0f) 
		{
			transform.eulerAngles = new Vector3(0,0,180); //flip the character on its x axis
			rigidbody2D.AddForce (Vector3.left * value);
		}	
		if (targetdiffy > 0f) 
		{
			rigidbody2D.AddForce (Vector3.up * value);
		}
	}

	// Method that gets an array of all the players with the target tag and heads for a random one
	GameObject FindTarget (string tag){
		GameObject[] targets;
		targets = GameObject.FindGameObjectsWithTag (tag);
		int rndtarget = Random.Range (0, targets.Length);
		GameObject target = targets[rndtarget];
		return target;
	}
}

