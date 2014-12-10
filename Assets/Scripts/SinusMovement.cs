using UnityEngine;
using System.Collections;

public class SinusMovement : MonoBehaviour {

	public int maxX, maxY, minX, minY;
	public int boxMovementWidth = 20, boxMovementHeight = 1;
	public float locX = 0;
	public float rx, ry;
	public float speed = 10f;
	public float sigma, amp;
	public bool back = false;

	public float piX5 = Mathf.PI * 5;
	public float theta;

	// Use this for initialization
	void Start () {
		//initialize the "movement box"
		rx = transform.localPosition.x;
		//introduce a random horizontal gap
		locX = Random.Range (-boxMovementWidth / 2, boxMovementWidth / 2);

		// keep the original vertical position
		ry = transform.localPosition.y;
		sigma = piX5 / boxMovementWidth;

		amp = boxMovementHeight/10;
		theta = Random.Range (-Mathf.PI, Mathf.PI);
	}
	
	// Update is called once per frame
	void Update () {
		if (!GameController.isGamePaused ()) {
			float delta = Time.deltaTime;
			
			float incX = back ? -delta * speed : delta * speed;
			locX += incX;
			float locY = amp * Mathf.Sin (sigma * locX + theta);
			
			if (locX < -boxMovementWidth / 2) {
				back = false;
				flip ();
			}
			if (locX > boxMovementWidth / 2) {
				back = true;
				flip ();
			}
			//Debug.Log (locX);
			transform.localPosition = new Vector3 (locX + rx, locY + ry, 0.9f);	//transform.position.z
		
		}

	}

	private void flip() {
		Vector2 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
}
