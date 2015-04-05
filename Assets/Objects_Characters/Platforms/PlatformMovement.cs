using UnityEngine;
using System.Collections;

public class PlatformMovement : MonoBehaviour {
	public float speed; // Speed the platform moves at
	public bool upAndDown; // Is the platform going vertically? (or horizontally, i.e false)
	public bool goingDown; // Is the platform heading down? (or up?)
	public bool goingRight; // Is the platform heading right? (or left?)
	public float rightXBound; // The right-most point on the x-axis the platform should go
	public float leftXBound; // The left-most point on the x-acis the platform should go
	public float topYBound; // The highest point on the y-axis the platform should go
	public float bottomYBound; // The lowest point on the y-axis the platform should go
	public bool moving; // Is the platform moving?

	// Left these in, just in case it is ever useful to know where the platforms are starting from
	//private float origXPosition;
	//private float origYPosition;


	// Use this for initialization
	void Start () {
		//origXPosition = gameObject.transform.position.x;
		//origYPosition = gameObject.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		if (moving == true) 
		{
			switch (upAndDown)
			{
			// Up and Down movement
			case true:
				if (transform.position.y >= topYBound)
				{
					goingDown = true;
				}
				if (transform.position.y <= bottomYBound)
				{
					goingDown = false;
				}

				if (goingDown == true)
				{
					transform.Translate (-Vector2.up * speed * Time.deltaTime);
				}

				if (goingDown == false)
				{
					transform.Translate (Vector2.up * speed * Time.deltaTime);
				}
				break;
			// Left and Right movement
			case false:
				if (transform.position.x >= rightXBound)
				{
					goingRight = false;
				}
				if (transform.position.x < leftXBound)
				{
					goingRight = true;
				}
				
				if (goingRight == true)
				{
					transform.Translate (Vector2.right * speed * Time.deltaTime);
				}
				
				if (goingRight == false)
				{
					transform.Translate (-Vector2.right * speed * Time.deltaTime);
				}
				break;
			}
		}
	}
}
