using UnityEngine;
using System.Collections;

public class AccelerometerInput : MonoBehaviour {

	public bool isInputActive = false;
	public float speed = 50f;

	private Vector3 dir;
	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (isInputActive) {
			if (PlayerController.isFlying && !GameController.isGamePaused () && GameController.isInGame && !GameController.isGameOver) {

				/**** récupère la valeur de l'accélération sur axe Ox ***/
				dir.x = Input.acceleration.x;
				dir *= Time.deltaTime;


				/*** met à jour l'animation de déplacement ****/
				if(dir.x <= -0.1f ){
					anim.SetTrigger ("toLeft");	
					//transform.Translate (dir * speed);
				}
				else if(dir.x >= 0.1f  ){
					anim.SetTrigger ("toRight");
					//transform.Translate (dir * speed);
				}
				transform.Translate (dir * speed);
				//rigidbody2D.velocity = dir * speed;
			}	
		
		}


	}
}
