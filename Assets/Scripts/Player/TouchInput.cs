using UnityEngine;
using System.Collections;

public class TouchInput : MonoBehaviour {

	public bool isInputActive = false;
	public float speed = 50f;
	public Camera camera;
	
	private Vector2 touchPos;
	private Vector3 translation;
	private Animator anim;
	private float lateralDelta = 2f;//0.15f

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		translation = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		if (isInputActive) {
			if (PlayerController.isFlying && !GameController.isGamePaused () && !GameController.isOverGUI()) {

				if (Input.GetMouseButtonDown (0)){//fonctionne aussi sur Android !!
					touchPos = camera.ScreenToWorldPoint(Input.mousePosition );
					//Debug.Log("*************   Clic en  : "+touchPos+" et player en : "+transform.position.x);
					
					if(gameObject.collider2D.bounds.Contains (touchPos)){
						translation = Vector3.zero;
					}
					else{
						if(!PlayerController.isFlyBegin){
							if(touchPos.x < transform.position.x ){
								translation.x = -lateralDelta;
								anim.SetTrigger ("toLeft");
								
							}
							else if(touchPos.x > (transform.position.x + collider2D.bounds.max.x )  ){
								translation.x = lateralDelta;
								anim.SetTrigger ("toRight");
							}
							
						}
						
					}
				}//fin input
				//transform.Translate(translation);
				rigidbody2D.velocity = translation * Time.smoothDeltaTime * speed;

			}	
			
		}
	}
}
