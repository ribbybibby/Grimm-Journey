using UnityEngine;
using System.Collections;

public class PlatformSwitch : MonoBehaviour {
	
	public GameObject platform;
	public float timeout;

	private bool active;
	private bool onTop;
	void Start ()
	{
		active = true;
		onTop = false;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "LRRH" && active == true)
		{
			onTop = true;

			if (platform.GetComponent<PlatformMovement>().moving == false)
			{
				platform.GetComponent<PlatformMovement>().moving = true;
			}
			else if (platform.GetComponent<PlatformMovement>().moving == true)
			{
				platform.GetComponent<PlatformMovement>().moving = false;
			}
			StartCoroutine(waitThenActivate());
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.tag == "LRRH")
		{
			onTop = false;
		}
	}

	IEnumerator waitThenActivate ()
	{
		active = false;
		gameObject.transform.localScale = new Vector3 (gameObject.transform.localScale.x / 2, gameObject.transform.localScale.y / 2, gameObject.transform.localScale.z);
		while (onTop == true)
		{
			yield return new WaitForSeconds(timeout);
		}
		active = true;
		gameObject.transform.localScale = new Vector3 (gameObject.transform.localScale.x * 2, gameObject.transform.localScale.y * 2, gameObject.transform.localScale.z);
	}
}
