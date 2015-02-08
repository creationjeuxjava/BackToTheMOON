using UnityEngine;
using System.Collections;

public class TranslationMovement : MonoBehaviour {

	public float speed = 10f;
	public Vector2 velocity;
	public bool triggered = false;

	// Use this for initialization
	void Start () {
		velocity = new Vector2 (Random.Range (-speed, speed), Random.Range (-speed, speed));
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(GameController.isInGame) {
			rigidbody2D.velocity = velocity;
		}

	}

	void OnCollisionEnter2D(Collision2D other) {

	}
	
}
