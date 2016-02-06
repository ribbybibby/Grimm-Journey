using UnityEngine;
using System.Collections;

public class PlatformCollisions : MonoBehaviour {

	public GameObject platformChild;
	
	// If LRRH presses down, we remove collisions between her and the platform
	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.tag == "LRRH")
		{
			if (Input.GetKey(col.gameObject.GetComponent<LRRHController>().moveDown))
			{
				if ((col.gameObject.transform.position.y - platformChild.transform.position.y) > 0)
				{
					Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), platformChild.GetComponent<Collider2D>());
				}
			}
		}
	}

	// Once LRRH is outside the trigger, we reset collisions
	void OnTriggerExit2D(Collider2D col)
	{
		Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), platformChild.GetComponent<Collider2D>(), false);
	}
}
