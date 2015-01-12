using UnityEngine;
using System.Collections;

public class TrollAttack : MonoBehaviour {
	// Set in Unity
	public float theDamage; // Amount of damage done with each attack
	public float theDistance; // Distance at which a good guy needs to be for the troll to start going to town
	public float rangeOfSkull; // Range of the attack
	public float attackWait; // Cooldown between each attack

	// The attack we instantiate
	public GameObject skullCrusher;

	// Stored timer value
	private float saveAttackWait;


	// Use this for initialization
	void Start () {
		saveAttackWait = Time.time + attackWait;
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.GetComponent<TrollController>().headingright == true)
		{
			RaycastHit2D[] hit = Physics2D.RaycastAll (transform.position, Vector2.right, theDistance);
			if (hit != null)
			{
				for (int i = 0; i < hit.Length; i++)
				{
					if (hit[i].collider.gameObject.layer == 12 || hit[i].collider.gameObject.layer == 13)
					{
						if (Time.time >= saveAttackWait)
						{
							Instantiate(skullCrusher, new Vector2(transform.position.x+rangeOfSkull, transform.position.y+1.5f), Quaternion.Euler(new Vector3(0, 0, 180)));
							saveAttackWait = Time.time + attackWait;
						}
					}
				}
			}
		}

		if (gameObject.GetComponent<TrollController>().headingright == false)
		{
			RaycastHit2D[] hit = Physics2D.RaycastAll (transform.position, -Vector2.right, theDistance);
			if (hit != null)
			{
				for (int i = 0; i < hit.Length; i++)
				{
					if (hit[i].collider.gameObject.layer == 12 || hit[i].collider.gameObject.layer == 13)
					{
						if (Time.time >= saveAttackWait)
						{
							Instantiate(skullCrusher, new Vector2(transform.position.x-rangeOfSkull, transform.position.y+1.5f), Quaternion.Euler(new Vector3(0, 0, 180)));
							saveAttackWait = Time.time + attackWait;
						}
					}
				}
			}
		}
	}
}
