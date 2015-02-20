using UnityEngine;
using System.Collections;

public class TranslationMovement : MonoBehaviour {

	public float speed = 2f;
	public Vector2 velocity;
	public bool triggered = false;

	// Use this for initialization
	void Start () {
		velocity = new Vector2 (Random.Range (-speed, speed), Random.Range (-speed, speed));
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (GameController.isInGame && !GameController.isGamePaused () && !GameController.isGameOver) {
			rigidbody2D.velocity = velocity;
		} 
		else {
			rigidbody2D.velocity = Vector2.zero;
		}

	}

	void OnCollisionEnter2D(Collision2D other) {

	}
	
}
