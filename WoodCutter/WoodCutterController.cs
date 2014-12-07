using UnityEngine;
using System.Collections;

public class WoodCutterController : MonoBehaviour {
	public int speed;
	public Camera camera;
	public float sightdistance;
	public float chance;
	public int timeout;
	private int startTimeOut;
	public int losIncrease;

	private bool headingright;
	private int startSpeed;
	
	void Start() {
		startSpeed = speed;
		startTimeOut = timeout;

	}
	// Update is called once per frame
	void Update () {
		timeout--;
		SpeedUpOnLOS ();
		KeepOnMoving ();
		FallThroughFloor ();
	}
	
	// Woodcutter will only fall through the floor if he is above BBW and there is a platform to land on
	// There is also a slight timeout on it.
	void FallThroughFloor ()
	{
		GameObject bbw = GameObject.FindGameObjectWithTag ("BBW");

		float bbwdiffx = bbw.transform.position.x - transform.position.x;
		float bbwdiffy = transform.position.y - bbw.transform.position.y;

		if ((bbwdiffx > -10 & bbwdiffx < 10) & bbwdiffy > 2 & timeout <= 0)
		{
			RaycastHit2D[] hit = Physics2D.RaycastAll (transform.position, -Vector2.up, sightdistance);

			if (hit.Length >= 5 & hit[1].collider.tag == "PlatformTrigger") 
			{
				Physics2D.IgnoreCollision(gameObject.collider2D, hit[2].rigidbody.collider2D);
				timeout = startTimeOut;
			}
		}
	}

	// Woodsman moves along the X axis until he reaches near the camera's edge, 
	// then he turns around and goes the other way
	void KeepOnMoving ()
	{
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
	}

	//Woodsman will increase speed if he sees the BBW
	void SpeedUpOnLOS() {
		if (headingright == true)
		{
			RaycastHit2D[] hit = Physics2D.RaycastAll (transform.position, Vector2.right, sightdistance);
			for (int i = 0; i < hit.Length; i++)
			{
				if (hit[i].collider.tag == "BBW")
				{
					speed = speed * losIncrease;
					break;
				}
				else 
				{
					speed = startSpeed;
				}
			}
		}
		else if (headingright == false)
		{
			RaycastHit2D[] hit = Physics2D.RaycastAll (transform.position, -Vector2.right, sightdistance);
			for (int i = 0; i < hit.Length; i++)
			{
				if (hit[i].collider.tag == "BBW")
				{
					speed = speed * losIncrease;
					break;
				}
				else 
				{
					speed = startSpeed;
				}
			}
		}

	}
}