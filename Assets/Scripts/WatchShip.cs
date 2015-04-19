using UnityEngine;
using System.Collections;

public class WatchShip : MonoBehaviour {

	Vector3 pos;	
	bool back;
	float limit;
	public float speed;
	public Vector2 velocity;
	public int min = -10, max= -2;
	void Awake() {
		back = Random.Range (0, 2) == 0;
		speed = back ? -speed : speed;
		velocity = new Vector2 (speed, 0);
		limit = Random.Range (min, max);
		changeLimit ();
	}
	// Update is called once per frame
	void Update () {
		if ((back && transform.localPosition.x < limit) || (!back && transform.localPosition.x > limit)) {
			changeLimit();
		}
		Debug.Log ("transform " + transform.localPosition);
		rigidbody2D.velocity = velocity;
	}

	private void changeLimit() {

		if (back) {
			limit = Random.Range(limit, max);
		} else {
			limit = Random.Range (min,limit);
		}
		back = !back;
		velocity.x = -velocity.x;
	}
}
