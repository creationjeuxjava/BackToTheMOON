using UnityEngine;
using System.Collections;

public class AccelerometerInput : MonoBehaviour {

	public bool isInputActive = false;
	public float speed = 50f;
	public Camera camera;

	private Vector3 dir;
	private Animator anim;
	private Vector3 screenPos;
	//private bool isOutOffScreen = false;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (isInputActive) {
			if (PlayerController.isFlying && !GameController.isGamePaused () && GameController.isInGame 
			    && !GameController.isGameOver && !GameController.isOverGUI() && !PlayerController.isFlyBegin) {

				/*** récupère la position à l'écran ***/
				screenPos = camera.WorldToScreenPoint(transform.position);
				Debug.Log(" pos ecran : "+screenPos.x+ " largeur ecran "+Screen.width);
				/**** récupère la valeur de l'accélération sur axe Ox ***/
				dir.x = Input.acceleration.x;
				dir *= Time.deltaTime;


				/*** met à jour l'animation de déplacement ****/
				if(dir.x <= -0.1f ){
					if(screenPos.x > 10){

						anim.SetTrigger ("toLeft");
					}	
					else{
						dir.x = 0f;
					}
				}
				else if(dir.x >= 0.1f ){

					if(screenPos.x < Screen.width-10 ){
						
						anim.SetTrigger ("toRight");
					}	
					else{
						dir.x = 0f;
					}
				}

				//vérifie si on est pas outOffScreen à droite ou à gauche
				//if(!isOutOffScreen){
					transform.Translate (dir * speed);
					//rigidbody2D.velocity = dir * speed;
				//}
				//Debug.Log(" pos ecran : "+screenPos.x+ " largeur ecran "+Screen.width+" outScrree ?"+this.isOutOffScreen);
			}	
		
		}


	}

	private bool isGoingOutScreen(){

		bool isGoingOut = false;
		/*****************   control out of Map	*********************/
		screenPos = camera.WorldToScreenPoint(transform.position);

		if (screenPos.x >= Screen.width) {
				transform.position = new Vector3(transform.position.x - 1f,transform.position.y,0);
				rigidbody2D.velocity = Vector3.zero;
				dir.x = 0f;
				isGoingOut = true;
		} 
		else if (screenPos.x <= 0) {
				transform.position = new Vector3(transform.position.x + 1f,transform.position.y,0);
				rigidbody2D.velocity = Vector3.zero;
				dir.x = 0f;
				isGoingOut = true;
		} 
		return isGoingOut;
	}
}
