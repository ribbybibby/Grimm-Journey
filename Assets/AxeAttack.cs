using UnityEngine;
using System.Collections;

public class AxeAttack : MonoBehaviour {

	// Set in Unity
	public float theDamage; // Amount of damage done with each attack
	public float theDistance; // Distance at which a good guy needs to be for the WC to start going to town
	public float rangeOfAxe; // Distance the attack reaches
	public float attackWait; // How long to wait before swinging again
	public GameObject Axe; // Axe that we instantiate

	// Leave this alone in Unity
	private float saveAttackWait; // Variable value of the timer
	
	
	// Use this for initialization
	void Start () {
		saveAttackWait = Time.time + attackWait;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (gameObject.GetComponent<WoodCutterController>().headingright == true)
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
							Instantiate(Axe, new Vector2(transform.position.x+rangeOfAxe, transform.position.y), Quaternion.Euler(new Vector3(0, 0, 40)));
							saveAttackWait = Time.time + attackWait;
						}
					}
				}
			}
		}
		
		if (gameObject.GetComponent<WoodCutterController>().headingright == false)
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
							Instantiate(Axe, new Vector2(transform.position.x-rangeOfAxe, transform.position.y), Quaternion.Euler(new Vector3(0, 0, 40)));
							saveAttackWait = Time.time + attackWait;
						}
					}
				}
			}
		}
	}
}
