using UnityEngine;
using System.Collections;

public class AccelerometerInput : MonoBehaviour {

	public bool isInputActive = false;
	public float speed = 50f;
	public Camera camera;

	private Vector3 dir;
	private Animator anim;
	private Vector3 screenPos;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (isInputActive) {
			if (PlayerController.isFlying && !GameController.isGamePaused () && GameController.isInGame 
			    && !GameController.isGameOver && !GameController.isOverGUI()) {

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
				//TODO : vérifier si on est pas outOffScreen à droite ou à gauche
				if(!isGoingOutScreen()){
					transform.Translate (dir * speed);
					//rigidbody2D.velocity = dir * speed;
				}
			}	
		
		}


	}

	private bool isGoingOutScreen(){

		bool isGoingOut = false;
		/*****************   control out of Map	*********************/
		screenPos = camera.WorldToScreenPoint(transform.position);

		if (screenPos.x >= Screen.width) {
				//transform.position = new Vector3(transform.position.x - 1f,transform.position.y,0);
				//rigidbody2D.velocity = Vector3.zero;
				isGoingOut = true;
		} 
		else if (screenPos.x <= 0) {
				//transform.position = new Vector3(transform.position.x + 1f,transform.position.y,0);
				//rigidbody2D.velocity = Vector3.zero;
				isGoingOut = true;
		} 
		return isGoingOut;
	}
}
