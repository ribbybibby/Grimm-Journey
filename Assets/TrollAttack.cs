using UnityEngine;
using System.Collections;

public class TrollAttack : MonoBehaviour {
	// Set in Unity

	public float theDamage; // Amount of damage done with each attack
	public float theDistance; // Distance at which a good guy needs to be for the troll to start going to town
	public float rangeOfSkull;
	public float attackWait;


	public GameObject skullCrusher;

	private bool readyToHit;
	private float saveAttackWait;


	// Use this for initialization
	void Start () {
		saveAttackWait = attackWait;
		readyToHit = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (readyToHit == false)
		{
			attackWait--;
			if (attackWait <= 0)
			{
				readyToHit = true;
			}
		}

		if (gameObject.GetComponent<TrollController>().headingright == true & readyToHit == true)
		{
			RaycastHit2D[] hit = Physics2D.RaycastAll (transform.position, Vector2.right, theDistance);
			if (hit != null)
			{
				for (int i = 0; i < hit.Length; i++)
				{
					if (hit[i].collider.gameObject.layer == 12 || hit[i].collider.gameObject.layer == 13)
					{
						Instantiate(skullCrusher, new Vector2(transform.position.x+rangeOfSkull, transform.position.y+1.5f), Quaternion.Euler(new Vector3(0, 0, 0)));
						readyToHit = false;
						attackWait = saveAttackWait;
					}
				}
			}
		}

		if (gameObject.GetComponent<TrollController>().headingright == false & readyToHit == true)
		{
			RaycastHit2D[] hit = Physics2D.RaycastAll (transform.position, -Vector2.right, theDistance);
			if (hit != null)
			{
				for (int i = 0; i < hit.Length; i++)
				{
					if (hit[i].collider.gameObject.layer == 12 || hit[i].collider.gameObject.layer == 13)
					{
						Instantiate(skullCrusher, new Vector2(transform.position.x-rangeOfSkull, transform.position.y+1.5f), Quaternion.Euler(new Vector3(0, 0, 180)));
						readyToHit = false;
						attackWait = saveAttackWait;
					}
				}
			}
		}
	}
}
