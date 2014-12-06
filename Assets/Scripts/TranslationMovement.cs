using UnityEngine;
using System.Collections;

public class TranslationMovement : MonoBehaviour {

	public float speed = 0.6f;
	public Vector3 velocity;
	public bool triggered = false;

	// Use this for initialization
	void Start () {
		velocity = new Vector3 (Random.Range (-speed, speed), Random.Range (-speed, speed), 0);
	}
	
	// Update is called once per frame
	void Update () {
		if(renderer.isVisible || triggered) {
			triggered = true;
			transform.Translate(velocity);
		}

	}

	void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Meteorite") {
			Vector3 tmpV = other.gameObject.GetComponent<TranslationMovement>().velocity;
			other.gameObject.GetComponent<TranslationMovement>().velocity = velocity;
			velocity = tmpV;
		}
	}
	
}
