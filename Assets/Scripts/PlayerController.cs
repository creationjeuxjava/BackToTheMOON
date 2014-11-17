using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public bool isMoving;
	public float speed = 0.75F;
	public float amplificationMove = 10f;
	public Vector2 touchDeltaPosition;
	public Vector3 lastMouseCoordinate = Vector3.zero;
	public bool isDragging = false;
	public Vector3 initialDraggingPos;
	public Camera camera;

	public GameObject fumee;

	/**** nvelle implémentation car le perso ne bouge pas ...c'est le niveau qui le fait ******/
	private static bool isFlying;
	public static Vector3 vitesse ;
	private Vector3 translation;
	private float speedPlayer = 0.3f;
	private bool isWithCask = false;

	public void launchIntheAir(){
		isFlying = true;
		vitesse = new Vector3(0,-speedPlayer,0);
		GameObject particules = Instantiate(fumee, new Vector3(transform.position.x,transform.position.y-1, -16f), transform.rotation) as GameObject; 
		particules.transform.parent = this.transform;
	}


	// Use this for initialization
	void Start () {
		touchDeltaPosition = Vector2.zero;
		vitesse = Vector3.zero;
		isFlying = false;
		translation = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		if (isFlying && !GameController.isGamePaused() && !GameController.isOverGUI()) {

			if(!audio.isPlaying)	audio.Play();
			/*****************   control out of Map	*********************/
			Vector3 screenPos = camera.WorldToScreenPoint(transform.position);
			if(screenPos.x >= Screen.width ) {
				translation.x = 0;
				transform.position = new Vector3(transform.position.x - 0.7f,transform.position.y,0);
			}
			else if(screenPos.x <= 0){
				translation.x = 0;
				transform.position = new Vector3(transform.position.x + 0.7f,transform.position.y,0);
				
			}
			else{
				transform.position = new Vector3(transform.position.x,transform.position.y,0);
			}


			/******************  déplacement droite/gauche du player  *************/
			if (Input.GetMouseButtonDown (0)){//fonctionne aussi sur Android !!

				Vector2 touchPos = camera.ScreenToWorldPoint(Input.mousePosition );
				Debug.Log("*************   Clic en  : "+touchPos+" et player en : "+transform.position.x);

				if(gameObject.collider2D.bounds.Contains (touchPos)){
					translation.x = 0;

				}
				else{

					if(touchPos.x < transform.position.x ){
						translation.x = -0.2f;
						
					}
					else if(touchPos.x > (transform.position.x + collider2D.bounds.max.x )  ){
						translation.x = 0.2f;
						
					}
				}
			} 			

			transform.Translate(translation);
				
		}
		if (GameController.isGamePaused ())
						audio.Pause ();

		/*** android ****/
		if (Input.touchCount == 1) {
			Vector3 touchPosition = Input.GetTouch (0).position;
			touchPosition = camera.ScreenToWorldPoint(touchPosition);

			// est- on sur le player ?
			if(gameObject.collider2D.bounds.Contains(new Vector2(touchPosition.x,touchPosition.y))){
				
				//Debug.Log("*************************------>   touche android sur le perso !!");
				if (Input.GetTouch(0).phase == TouchPhase.Moved) {					
					//touchDeltaPosition = Input.GetTouch(0).deltaPosition;
				}

			}
			//on relache la touche... le player s'envole !!
			if (Input.GetTouch(0).phase == TouchPhase.Ended) {

				//transform.Translate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);
			}

			//affichage pour debug
			InGameGUI.setMessage("centré en ("+gameObject.collider2D.bounds.center.x+" , "+gameObject.collider2D.bounds.center.y+") ",
			                     "PlayerController: on touche écran en  (" + touchPosition.x + " , " + touchPosition.y + ")");
		}


		/**** version pc *****/
		//appui
		if (Input.GetMouseButtonDown (0) && Input.mousePresent && Application.platform != RuntimePlatform.Android 
						&& Application.platform != RuntimePlatform.IPhonePlayer) {

			Vector3 touchPosition = Input.mousePosition;
			touchPosition = camera.ScreenToWorldPoint (touchPosition);
			//affichage pour debug
			InGameGUI.setMessage("centré en ("+gameObject.collider2D.bounds.center.x+" , "+gameObject.collider2D.bounds.center.y+") ",
			                     "PlayerController: on touche écran en  (" + touchPosition.x + " , " + touchPosition.y + ")");

			//Debug.Log("ameObject.collider2D.bounds "+gameObject.collider2D.bounds);
			if (gameObject.collider2D.bounds.Contains (new Vector2 (touchPosition.x, touchPosition.y))) {
				//Debug.Log("iclic perso");
					//si on n'est pas encore en mode dragging, on se met en mode dragging et on note 
					// la position initialle de la souris
					if (!isDragging) {
						isDragging = true;	
						initialDraggingPos = Input.mousePosition;


						//Debug.Log("initialDraggingPos:"+initialDraggingPos.x);
					} 
					// On regarde de combien la souris a bougé 
					//Vector3 mouseDelta = Input.mousePosition - lastMouseCoordinate;
					// on save la dernière position
					//lastMouseCoordinate = Input.mousePosition;
			}

		} 

		//relache...le player s'envole mais toujours de la meme quantité...TODO...à améliorer!!
		if(Input.GetMouseButtonUp(0)&& Application.platform != RuntimePlatform.Android 
		   && Application.platform != RuntimePlatform.IPhonePlayer){
			//si on était en mode dragging lorsque la souris a été relachée, on note l'écart de position
			// et on notifie le fait qu'on est plus en mode dragging
			if(isDragging) {
				isDragging = false;
				touchDeltaPosition = Input.mousePosition - initialDraggingPos;
				//Debug.Log("touchDeltaPosition:"+touchDeltaPosition.x);
			}
			//Debug.Log("touch delta :"+touchDeltaPosition);
			//transform.Translate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);


			//rigidbody2D.AddForce(new Vector2(touchDeltaPosition.x* amplificationMove,touchDeltaPosition.y* amplificationMove));
		}
		//renderer.	
	}

	void OnCollisionEnter2D(Collision2D other){
		
		if(other.gameObject.tag == "Meteorite" ){
			Debug.Log ("***************  collision avec un météorite ");
			//on meurt ?
			if(isWithCask) Destroy(other.gameObject);
			else updateVitesse(other.gameObject);
			
		}
		if(other.gameObject.tag == "Oiseau" ){
			Debug.Log ("***************  collision avec un oiseau ");
			//on meurt ?
			updateVitesse(other.gameObject);
			
		}
		if(other.gameObject.tag == "Cask" ){
			Debug.Log ("***************  collision avec un cask ");
			Destroy(other.gameObject);
			isWithCask = true;

			Sprite casqueSprite = Resources.Load("Sprites/persocasque", typeof(Sprite)) as Sprite;
			GetComponent<SpriteRenderer>().sprite = casqueSprite;

			GetComponent<Inventory>().addItem(new Item("casque",0,Item.ItemType.Permanent));
			
		}
	}

	private void updateVitesse(GameObject obj){

		vitesse.y += obj.GetComponent<InteractionEnnemy>().speedReducingFactor;
	
	}

	/*void OnMouseOver()
	{
		Debug.Log ("souris au dessus de : "+gameObject.name);
	}*/

	void OnMouseDown()
	{
		//Debug.Log ("souris clic  dessus de : "+gameObject.name);
		//translation.x = 0;
	}
}
