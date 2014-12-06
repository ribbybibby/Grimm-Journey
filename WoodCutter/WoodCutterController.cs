using UnityEngine;
using System.Collections;

public class WoodCutterController : MonoBehaviour {
	public int speed;
	public Camera camera;
	public float sightdistance;
	public float chance;
	public GameObject bbw;
	public GameObject lrrh;
	


	private bool headingright;
	

	// Update is called once per frame
	void Update () {

		transform.eulerAngles = new Vector3(0,0,0);
		Vector3 cam = camera.WorldToScreenPoint(transform.position);
		if (cam.x < 100)
		{
			headingright = true;
		}
		else if (cam.x > 500)
		{
			headingright = false;
			transform.Translate (-Vector2.right * speed * Time.deltaTime);
		}

		if (headingright == true)
		{
			transform.Translate (Vector2.right * speed * Time.deltaTime);
		}
		else if (headingright == false)
		{
			transform.Translate (-Vector2.right * speed * Time.deltaTime);
		}

		FallThroughFloor ();


	}

	void FallThroughFloor ()
	{
		float bbwdiff = bbw.transform.position;
		RaycastHit2D[] hit = Physics2D.RaycastAll (transform.position, -Vector2.up, sightdistance);
		Debug.Log (hit.Length);
		if (hit.Length >= 5 & hit[1].collider.tag == "PlatformTrigger") 
		{
			Physics2D.IgnoreCollision(gameObject.collider2D, hit[2].rigidbody.collider2D);
		}
	}
}
