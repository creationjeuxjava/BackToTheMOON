using UnityEngine;
using System.Collections;

public class AccelerometerInput : MonoBehaviour {

	public bool isInputActive = false;
	public float speed = 50f;

	Vector3 dir;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (isInputActive) {
			if (PlayerController.isFlying && !GameController.isGamePaused ()) {
				dir.x = Input.acceleration.x;
				dir *= Time.deltaTime;
				transform.Translate (dir * speed);
			}	
		
		}


	}
}
