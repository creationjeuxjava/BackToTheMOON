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
	private Vector3 screenPos,futurScreenPos;
	private float lastClickTime = 0;
	private float catchTime = 0.25f;

	private const float DOUBLE_LATERAL = 4f;
	private const float SIMPLE_LATERAL = 2f;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		translation = Vector3.zero;
		futurScreenPos = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		if (isInputActive && !SystemInfo.supportsAccelerometer) {
			if (PlayerController.isFlying && !GameController.isGamePaused () && GameController.isInGame 
			    && !GameController.isGameOver && !GameController.isOverGUI() && !PlayerController.isFlyBegin) {

				if (Input.GetMouseButtonDown (0)){//fonctionne aussi sur Android !!
					touchPos = camera.ScreenToWorldPoint(Input.mousePosition );
					Debug.Log("*************   Clic en  : "+touchPos+" et player en : "+transform.position.x);

					/*** est-ce un double-clic ?****/
					//double click
					if(Time.time - lastClickTime < catchTime ){
						//Debug.Log ("double click");
						lateralDelta = DOUBLE_LATERAL;
					}
					//simple click
					else{
						//Debug.Log ("single click");
						lateralDelta = SIMPLE_LATERAL;
					}

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
					lastClickTime = Time.time;
				}//fin input
				//transform.Translate(translation);
				if(!isGoingOutScreen()){

					//if (  (translation.x < 0 && transform.position.x > touchPos.x) ||
					    //(translation.x > 0 && transform.position.x < touchPos.x) ){

							rigidbody2D.velocity = translation * Time.smoothDeltaTime * speed;
					Debug.Log("*************   on deplace le player  translation : "+rigidbody2D.velocity.x);
					//}


				}

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
			translation = Vector3.zero;
			isGoingOut = true;
		} 
		else if (screenPos.x <= 0) {
			transform.position = new Vector3(transform.position.x + 1f,transform.position.y,0);
			rigidbody2D.velocity = Vector3.zero;
			translation = Vector3.zero;
			isGoingOut = true;
		} 
		return isGoingOut;
	}
}
