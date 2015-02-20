using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

   	public GameObject fumee;
	private Animator anim;

	/**** nvelle implémentation car le perso ne bouge pas ...c'est le niveau qui le fait ******/
	public static bool isFlying;
	public static Vector3 vitesse ;
	public Camera camera;
	public float gravityReductionfactor = 300000;

	public static Vector3 translation;
	private float speedPlayer = 0.3f;//0.7f;
	public static bool isWithCask = false;
	public static bool isWithShoe = false;
	public static bool isWithBeans = false;
	private bool isItemActivated = false;
	public static bool isFlyBegin = false;

	private float timeLeft = 5.0f;

	public static Vector3 actualPosition;

	private float gravityLevel;
	private float startFlyTime;
	private const float MAX_VITESSE = -0.3f;
	private float gravityEffect;
	private float timeSinceStart;

	public enum State : byte
	{
		naked,	//battement "'ailes"
		laser,   // donner des coups de sabre
		noAction  //item sans action
	}
	
	//The current mode the demo is in
	public  State currentState = State.noAction;
	public static State state;

	public void launchIntheAir(float gravity){
		gravityLevel = gravity;
		isFlying = true;
		isFlyBegin = true;
		vitesse = new Vector3(0,-speedPlayer,0);
		startFlyTime = Time.time;
		//GameObject particules = Instantiate(fumee, new Vector3(transform.position.x,transform.position.y-2, -2f), transform.rotation) as GameObject; 
		//particules.transform.parent = this.transform;
		anim.SetTrigger ("decollage");
	}


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		state = currentState;

		vitesse = Vector3.zero;
		isFlying = false;
		isFlyBegin = false;
		translation = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		state = currentState;
		//Debug.Log("*********************"+this+" Vtesse player : "+vitesse.y);

		if (isFlying && !GameController.isGamePaused() ) {

			/*** on met à jour la vitesse du joueur ***/
			timeSinceStart = Time.time - startFlyTime;
			gravityEffect = (float) 0.5f * gravityLevel * timeSinceStart  / gravityReductionfactor;//calcul "savant" de l'équation horaire !! * timeSinceStart
			
			if(!GameController.isInSpace){
				//on calcule le vecteur vitesse du player ajusté
				vitesse.y = vitesse.y + gravityEffect;
				//Debug.Log("*************Vitesse globale : "+vitesse.y+" et temps écoulé depuis le lancement : "+timeSinceStart+ " effet de gravity : "+ gravityEffect);
				//lastPlayerSpeed = new Vector3(playerSpeed.x,playerSpeed.y,playerSpeed.z);
				//Debug.Log("------------------------ isflybegin: "+isFlyBegin + " item activated ? "+isItemActivated);
				controlMaxVitessePlayer();
			}
			else{
				/*playerSpeed.x = PlayerController.vitesse.x;
			playerSpeed.y = PlayerController.vitesse.y;
			playerSpeed.z = PlayerController.vitesse.z;*/
				
				//Debug.Log("Vitesse globale avt control (inSpace): "+playerSpeed.y);
				controlMaxVitessePlayer();
			}

			if(!GameController.isOverGUI()){
				if(!audio.isPlaying)	audio.Play();
				
				/*****************   control out of Map	*********************/
				/*Vector3 screenPos = camera.WorldToScreenPoint(transform.position);
				if(screenPos.x >= Screen.width ) {
					translation.x = 0;
					transform.position = new Vector3(transform.position.x - 1f,transform.position.y,0);
					rigidbody2D.velocity = Vector3.zero;
				}
				else if(screenPos.x <= 0){
					translation.x = 0;
					transform.position = new Vector3(transform.position.x + 1f,transform.position.y,0);
					rigidbody2D.velocity = Vector3.zero;
				}*/
				//else{

					/*** correction si trop bas...suite aux collisions ****/
					//if(screenPos.y <= 100){//40
					if(transform.position.y <= -28){//voir ds le world (scene) directement
						
						transform.position = new Vector3(transform.position.x,-26,0);
						rigidbody2D.velocity = Vector3.zero;
					}
					else{
						transform.position = new Vector3(transform.position.x,transform.position.y,0);
						rigidbody2D.velocity = Vector3.zero;
						//Debug.Log("PlayerController : on update normalement !");
					}
					
				//}
				
				//transform.Translate(translation);
				actualPosition = transform.position;
				
				if(isItemActivated) this.checkTimeItemLeft();

			}

		}
		if (GameController.isGamePaused ())
						audio.Pause ();

		ActionButtonManager.updateIcon(currentState);

	}

	/** méthode statique appelée par Gamecontroller lors de l'entrée ds l'espace ****/
	public static void setVitesseEnterInSpace(float vitesseY){
		vitesse.y = vitesseY;	
		//Debug.Log ("***************  vitesse en entrant ds l'espace : "+vitesseY);
	}

	/**** détection des collisions avec les GO istrigger = false ****/
	void OnCollisionEnter2D(Collision2D other){
		
		if(other.gameObject.tag == "Meteorite" || other.gameObject.tag == "Colonne" 
		   || other.gameObject.tag == "Triangle" || other.gameObject.tag == "MiniMeteorite"){
			//Debug.Log ("***************  collision avec un météorite ");
			//on meurt ?
			//if(isWithCask) Destroy(other.gameObject);
			//else 

			updateVitesse(other.gameObject);
			
		}
		if(other.gameObject.tag == "Oiseau" ){
			//Debug.Log ("***************  collision avec un oiseau ");
			updateVitesse(other.gameObject);
			
		}




	}

	/**** détection des collisions avec les GO istrigger = true 
	 *****    permet de pouvoir passer au travers !!      ****/
	void OnTriggerEnter2D(Collider2D other) {

		if(other.gameObject.tag == "Cask"  && !isItemActivated && !isFlyBegin){
			//Debug.Log ("***************  collision avec un cask ");
			Destroy(other.gameObject);
			isWithCask = true;
			isWithShoe = false;
			isWithBeans = false;
			timeLeft = 5f;
			isItemActivated = true;
			anim.SetBool("withCask",true);
			
			//Sprite casqueSprite = Resources.Load("Sprites/persocasque", typeof(Sprite)) as Sprite;
			//GetComponent<SpriteRenderer>().sprite = casqueSprite;
			
			//GetComponent<Inventory>().addItem(new Item("casque",1,Item.ItemType.Timer));
			currentState = State.noAction;
			//ActionButtonManager.updateIcon(currentState);
		}

		if(other.gameObject.tag == "Shoe" && !isItemActivated && !isFlyBegin ){
			//Debug.Log ("***************  collision avec une shoe ");
			//Destroy(other.gameObject);
			other.gameObject.SetActive(false);
			isWithShoe = true;
			isWithCask = false;
			isWithBeans = false;
			timeLeft = 5f;
			isItemActivated = true;
			anim.SetBool("withShoes",true);
			
			//Sprite shoeSprite = Resources.Load("Sprites/persoshoes", typeof(Sprite)) as Sprite;
			//GetComponent<SpriteRenderer>().sprite = shoeSprite;
			
			//GetComponent<Inventory>().addItem(new Item("shoes",2,Item.ItemType.Timer));
			//boostVitesse(50/100);
			updateVitesse(other.gameObject);
			currentState = State.noAction;
			//ActionButtonManager.updateIcon(currentState);
		}
		if(other.gameObject.tag == "Piece" ){
			//Debug.Log ("***************  collision avec une piece ");
			GameController.addPiece();
			//Destroy(other.gameObject);
			other.gameObject.SetActive(false);
		}
		if(other.gameObject.tag == "Diamond" ){
			//Debug.Log ("***************  collision avec une piece ");
			GameController.addDiamond();
			//Destroy(other.gameObject);
			other.gameObject.SetActive(false);
		}

		if(other.gameObject.tag == "Beans" && !isItemActivated && !isFlyBegin){
			//Debug.Log ("***************  collision avec beans ");
			isWithShoe = false;
			isWithCask = false;
			isWithBeans = true;
			timeLeft = 1f;
			isItemActivated = true;
			anim.SetTrigger("goFlageollet");
			other.gameObject.SetActive(false);
			updateVitesse(other.gameObject);
			currentState = State.noAction;
			//ActionButtonManager.updateIcon(currentState);
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
		else if ( obj.tag == "Beans")
			vitesse.y += vitesse.y * 20 / 100;
		else {

			//if(vitesse.y + obj.GetComponent<InteractionEnnemy>().speedReducingFactor <= 0)
				vitesse.y += obj.GetComponent<InteractionEnnemy>().speedReducingFactor;
			//Debug.Log(obj.name+" : On réduit la vitesse"+vitesse.y + obj.GetComponent<InteractionEnnemy>().speedReducingFactor);
		}
		//Debug.Log("*********************"+this+" Vitesse player avt control : "+vitesse.y);
		//controlMaxVitessePlayer();
	}

	private void controlMaxVitessePlayer(){
		if( vitesse.y  < MAX_VITESSE){
			vitesse.y = MAX_VITESSE;
			//Debug.Log("control: on limite la vitesse à "+MAX_VITESSE);
		}
		//Debug.Log(" ************** CONTROLE --> Vitesse globale après control: "+vitesse.y);
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

			currentState = State.naked;
			//ActionButtonManager.updateIcon(currentState);
		}
	}

	private void resetPlayerState(){

		isWithShoe = false;
		isWithCask = false;
		isWithBeans = false;
		anim.SetBool("withCask",false);
		anim.SetBool("withShoes",false);
		currentState = State.naked;
		ActionButtonManager.updateIcon(currentState);
	}

	/*** permet de savoir si la phase de décollage est terminée en animation ...****/
	/*** fonction appelée par l'animator ****/
	private void stopStartFly(){
		//Debug.Log ("le fly enclenché !!");
		isFlyBegin = false;
		currentState = State.naked;
		//ActionButtonManager.updateIcon(currentState);
	}


	/*** méthode appelée par le script de controle de la GUI *****/
	public void makeAction(){

		switch (currentState) {

			case State.naked:
				
				if(vitesse.y >= -0.05f){
					anim.SetTrigger("battements");
					vitesse.y += vitesse.y * 50 / 100;//avt 10
				}
					
				//Debug.Log ("clic sur bouton action : battements");
				//TODO limiter le nbre de clics possibles par un timer !!--> pas forcément intéréssant ?
				break;

			case State.laser:
				//anim setTrigger -> sabre
				break;

			case State.noAction:
				//Debug.Log ("clic sur bouton action  : noAction");
				break;
		
		}
	}


}
