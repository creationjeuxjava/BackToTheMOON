﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	/*private bool isMoving;
	private float speed = 0.75F;*/

	public GameObject fumee;
	public Animator anim;

	/**** nvelle implémentation car le perso ne bouge pas ...c'est le niveau qui le fait ******/
	private static bool isFlying;
	public static Vector3 vitesse ;
	public Camera camera;
	public static Vector3 translation;
	private float speedPlayer = 0.7f;
	public static bool isWithCask = false;
	public static bool isWithShoe = false;
	private bool isItemActivated = false;
	public static bool isFlyBegin = false;

	private float timeLeft = 5.0f;

	public void launchIntheAir(){
		isFlying = true;
		isFlyBegin = true;
		vitesse = new Vector3(0,-speedPlayer,0);
		GameObject particules = Instantiate(fumee, new Vector3(transform.position.x,transform.position.y-1, -16f), transform.rotation) as GameObject; 
		particules.transform.parent = this.transform;
		anim.SetTrigger ("decollage");
	}


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();

		vitesse = Vector3.zero;
		isFlying = false;
		isFlyBegin = false;
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
				transform.position = new Vector3(transform.position.x - 1.5f,transform.position.y,0);
			}
			else if(screenPos.x <= 0){
				translation.x = 0;
				transform.position = new Vector3(transform.position.x + 1.5f,transform.position.y,0);
				
			}
			else{
				/*** correction si trop bas...suite aux collisions ****/
				if(screenPos.y <= 80){//40
					
					transform.position = new Vector3(transform.position.x,-15,0);
				}
				else{
					transform.position = new Vector3(transform.position.x,transform.position.y,0);
				}

			}




			/******************  déplacement droite/gauche du player  *************/
			if (Input.GetMouseButtonDown (0)){//fonctionne aussi sur Android !!

				Vector2 touchPos = camera.ScreenToWorldPoint(Input.mousePosition );
				//Debug.Log("*************   Clic en  : "+touchPos+" et player en : "+transform.position.x);

				if(gameObject.collider2D.bounds.Contains (touchPos)){
					//translation.x = 0;
					translation = Vector3.zero;

				}
				else{
					if(!isFlyBegin){
						if(touchPos.x < transform.position.x ){
							translation.x = -0.4f;
							anim.SetTrigger ("toLeft");
							
						}
						else if(touchPos.x > (transform.position.x + collider2D.bounds.max.x )  ){
							translation.x = 0.4f;
							anim.SetTrigger ("toRight");
						}

					}

				}
			} 			

			transform.Translate(translation);

			if(isItemActivated) this.checkTimeItemLeft();
		}
		if (GameController.isGamePaused ())
						audio.Pause ();



	}

	/**** détection des collisions avec les GO istrigger = false ****/
	void OnCollisionEnter2D(Collision2D other){
		
		if(other.gameObject.tag == "Meteorite" || other.gameObject.tag == "Colonne"){
			//Debug.Log ("***************  collision avec un météorite ");
			//on meurt ?
			//if(isWithCask) Destroy(other.gameObject);
			//else 

			updateVitesse(other.gameObject);
			
		}
		if(other.gameObject.tag == "Oiseau" ){
			//Debug.Log ("***************  collision avec un oiseau ");
			//on meurt ?
			updateVitesse(other.gameObject);
			
		}




	}

	/**** détection des collisions avec les GO istrigger = true 
	 *****    permet de pouvoir passer au travers !!      ****/
	void OnTriggerEnter2D(Collider2D other) {

		if(other.gameObject.tag == "Cask"  && !isItemActivated ){
			//Debug.Log ("***************  collision avec un cask ");
			Destroy(other.gameObject);
			isWithCask = true;
			isWithShoe = false;
			isItemActivated = true;
			anim.SetBool("withCask",true);
			
			Sprite casqueSprite = Resources.Load("Sprites/persocasque", typeof(Sprite)) as Sprite;
			GetComponent<SpriteRenderer>().sprite = casqueSprite;
			
			GetComponent<Inventory>().addItem(new Item("casque",1,Item.ItemType.Timer));
			
		}

		if(other.gameObject.tag == "Shoe" && !isItemActivated ){
			//Debug.Log ("***************  collision avec une shoe ");
			Destroy(other.gameObject);
			isWithShoe = true;
			isWithCask = false;
			isItemActivated = true;
			anim.SetBool("withShoes",true);
			
			Sprite shoeSprite = Resources.Load("Sprites/persoshoes", typeof(Sprite)) as Sprite;
			GetComponent<SpriteRenderer>().sprite = shoeSprite;
			
			GetComponent<Inventory>().addItem(new Item("shoes",2,Item.ItemType.Timer));
			//boostVitesse(50/100);
			updateVitesse(other.gameObject);
			
		}
		if(other.gameObject.tag == "Piece" ){
			//Debug.Log ("***************  collision avec une piece ");
			GameController.addPiece();
			//Destroy(other.gameObject);
			other.gameObject.SetActive(false);
		}

	}


	private void boostVitesse(float boostValue){
		vitesse.y += vitesse.y * boostValue;
		//Debug.Log ("***************  collision avec une shoe -----------> boost de vitesse 50 /100 ");
	
	}
	private void updateVitesse(GameObject obj){

		if (isWithCask) {
				
			Vector3 pos = obj.transform.position;
			PooledObject poolObjectComponent = obj.GetComponent<PooledObject> ();
			if (poolObjectComponent == null) {
					Destroy (obj);
			} 
			//explose le météorite ==> methode spawnAsteroid !!
			GameObject gameControlller = GameObject.FindGameObjectWithTag ("GameController");
			gameControlller.GetComponent<LoadLevelcontroller> ().spawnAsteroid (obj);

			vitesse.y += obj.GetComponent<InteractionEnnemy> ().speedReducingFactor * 50 / 100;
		} 
		else if (isWithShoe && obj.tag == "Shoe")
						vitesse.y += vitesse.y * 50 / 100;
		else {
			vitesse.y += obj.GetComponent<InteractionEnnemy>().speedReducingFactor;
			//Debug.Log(obj.name+" : On réduit la vitesse");
		}
	
	}

	/*** vérifie si l'item est encore valide en fonction du temps restant ****/
	private void checkTimeItemLeft(){

		timeLeft -= Time.deltaTime;
		if(timeLeft < 0)
		{
			isItemActivated = false;
			Sprite normalSprite = Resources.Load("Sprites/perso", typeof(Sprite)) as Sprite;
			GetComponent<SpriteRenderer>().sprite = normalSprite;
			timeLeft = 5.0f;
			resetPlayerState();
			//TODO : supprimer l'item de l'inventaire !!
		}
	}

	private void resetPlayerState(){

		isWithShoe = false;
		isWithCask = false;
		anim.SetBool("withCask",false);
		anim.SetBool("withShoes",false);
	}

	/*** permet de savoir si la phase de décollage est terminée en animation ...****/
	/*** fonction appelée par l'animator ****/
	private void stopStartFly(){

		isFlyBegin = false;
	}


}
