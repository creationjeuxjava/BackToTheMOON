using UnityEngine;
using System.Collections;

public class SinusMovement : MonoBehaviour {

	public int maxX, maxY, minX, minY;
	public int boxMovementWidth = 20, boxMovementHeight = 1;
	public float locX = 0;
	public float rx, ry;
	public float speed = 0.3f;
	public float sigma, amp;
	public bool back = false;

	public float piX5 = Mathf.PI * 5;
	public float theta;

	// Use this for initialization
	void Start () {
		//initialize the "movement box"

		rx = transform.localPosition.x;

		locX += Random.Range (-boxMovementWidth / 2, boxMovementWidth / 2);
		ry = transform.localPosition.y;

		sigma = piX5 / boxMovementWidth;
		amp = boxMovementHeight/10;
		theta = Random.Range (-Mathf.PI, Mathf.PI);
	}
	
	// Update is called once per frame
	void Update () {

		float delta = Time.deltaTime;

		float incX = back ? -delta * speed : delta * speed;
		locX += incX;
		float locY = amp * Mathf.Sin (sigma * locX + theta);

		if (locX < -boxMovementWidth / 2)
			back = false;
		if (locX > boxMovementWidth / 2)
			back = true;

		Debug.Log (back);
		transform.localPosition = new Vector3 (locX + rx, locY + ry, transform.position.z);
	}
}
