 using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;
//using UnityEditor;

/*
 * * gère la logique du jeu et la création du niveau par le biais de LoadLevelController, mais aussi d'objets
 *    de pooling !!
 * */

public class GameController : MonoBehaviour {

	public GameObject player;

	public static GameObject world;//conteneur du world
	private static bool isWorldMoving = false;
	private static bool isGameInPause = false;
	private static bool isOverGUIPause = false;
	public static bool isInGame,isGameOver;
	public static bool isInSpace;
	private GameObject backLayer, middleLayer, foreLayer,backBackLayer;



	public static float altitude = 0;
	public static int nbrePieces = 0;
	private int coeffAltitude = 100;
	private float altMaxForWinLevel = 150000f;//80000f ou 150000f ;
	private float altBeginOfSpace = 41000f; //10000f; test   //41000f;  realité
	private const float MAX_VITESSE = -0.3f;

	private int coeffVitesse = 1 * 3600; 
	public int currentLevel = 1; //par défaut le premier niveau
	private float gravityLevel;
	private float startFlyTime;
	public static Vector3 lastPlayerSpeed,playerSpeed;


	private const float TIME_TO_GAME_OVER = 3f;
	public static float timeLeftToGameOver = TIME_TO_GAME_OVER;

	//private List<GameObject> listeObjectPoolers = new List<GameObject>();
	private Dictionary<GameObject,string> dicoObjectPoolers = new Dictionary<GameObject,string>();

	// création initiale
	void Start () {
		//Application.targetFrameRate = 60;
		resetGame ();
		//Debug.Log (this.name + " méthode strat");
	}
	
	// Update is called once per frame
	void Update () {

		/** a-t-on besoin d'activer des GO depuis les ObjectPooler ****/
		foreach (KeyValuePair<GameObject, string> entry in dicoObjectPoolers)
		{
			string layerName = entry.Value; 

			//Vector2 minMax = listeObjectPoolers[i].GetComponent<ObjectPooler>().getMinMax();
			Vector2 minMax = entry.Key.GetComponent<ObjectPooler>().getMinMax();
			//Debug.Log("Position du ForeLayer : "+foreLayer.transform.position.y*(-1)+" et min "+minMax.x+" et max "+minMax.y +" pour GO "+listeObjectPoolers[i].name);
			//if(foreLayer.transform.position.y * (-1) > minMax.x && foreLayer.transform.position.y * (-1 )< minMax.y){

			GameObject obj = null;
			if(altitude > minMax.x && altitude < minMax.y){
				obj = entry.Key.GetComponent<ObjectPooler>().GetPooledObject();
				if(obj != null){
					//Debug.Log(listeObjectPoolers[i].name +" crée !");
					
					//obj.transform.position = new Vector3(Random.Range(-3,3),Random.Range(20,200),-4.6f);
					obj.transform.position = new Vector3(Random.Range(-3,3),Random.Range(0,100),-4.6f);
					obj.SetActive(true);
					if(layerName == "foreGround")
						obj.transform.parent = foreLayer.transform;
					else if(layerName == "middleLayer")
						obj.transform.parent = middleLayer.transform;
					else if(layerName == "backGround")
						obj.transform.parent = backLayer.transform;
					else if(layerName == "backBackGround")
						obj.transform.parent = backBackLayer.transform;
				}
				
			}
		}


		/*for (int i = 0; i < listeObjectPoolers.Count; i++) {

			Vector2 minMax = listeObjectPoolers[i].GetComponent<ObjectPooler>().getMinMax();
			if(foreLayer.transform.position.y * (-1) > minMax.x && foreLayer.transform.position.y * (-1 )< minMax.y){
				GameObject obj = listeObjectPoolers[i].GetComponent<ObjectPooler>().GetPooledObject();
				if(obj != null){
					obj.transform.position = new Vector3(Random.Range(-14,14),Random.Range(20,200),-4.6f);
					obj.SetActive(true);
					obj.transform.parent = foreLayer.transform;
				}

			}
		}*/

		if (isWorldMoving == true && !isGameInPause && isInGame) {

			//temps écoulé depuis le début du lancement
			float timeSinceStart = Time.time - startFlyTime;
			float gravityEffect = (float) 0.5f * gravityLevel * timeSinceStart * timeSinceStart /3000;//calcul savant de l'équation horaire !!
		


			if(!isInSpace){
				//on calcule le vecteur vitesse du player ajusté
				playerSpeed = new Vector3(PlayerController.vitesse.x,PlayerController.vitesse.y + gravityEffect, PlayerController.vitesse.z);
				//Debug.Log("Vitesse globale : "+playerSpeed+" et temps écoulé depuis le lancement : "+timeSinceStart+ " effet de gravity : "+ gravityEffect);
				lastPlayerSpeed = new Vector3(playerSpeed.x,playerSpeed.y,playerSpeed.z);
				controlMaxVitessePlayer();
			}
			else{
				playerSpeed.x = PlayerController.vitesse.x;
				playerSpeed.y = PlayerController.vitesse.y;
				playerSpeed.z = PlayerController.vitesse.z;
				controlMaxVitessePlayer();
			}



			//chaque partie avance à une vitesse différente == parallax
			backBackLayer.transform.Translate (playerSpeed / 3f);
			backLayer.transform.Translate (playerSpeed / 2);
			middleLayer.transform.Translate (playerSpeed / 1.5f);
			foreLayer.transform.Translate (playerSpeed);

			//calcul de l'altitude
			altitude = foreLayer.transform.position.y * -1 * coeffAltitude;
			float vitesse = PlayerController.vitesse.y*-1 * coeffVitesse;
			//Debug.Log("ISINSpace :"+isInSpace +" Altitude :"+altitude+"Vitesse Player : "+vitesse+" km/h et nbre pièces : "+nbrePieces);
			Debug.Log (this+" vitesse joueur "+playerSpeed.y);


			//if(PlayerController.vitesse.y > 0){
			//if(lastPlayerSpeed.y > 0.2f){
			/*if((!isInSpace && playerSpeed.y > 0.2f) || (isInSpace && playerSpeed.y >= 0)){
				checkTimeToGameOverLeft();
			}*/
			if(playerSpeed.y >= 0){
				checkTimeToGameOverLeft();
			}
			else{
				timeLeftToGameOver = TIME_TO_GAME_OVER;
			}
			if(altitude >= altMaxForWinLevel){
				isInGame = false;
			}
			if(altitude >= altBeginOfSpace && !isInSpace) { 
				isInSpace = true;
				PlayerController.setVitesseEnterInSpace(lastPlayerSpeed.y);
			}

		}
	}

	private void controlMaxVitessePlayer(){
		if( playerSpeed.y  < MAX_VITESSE){
			playerSpeed.y = MAX_VITESSE;
		}
	
	}
	/*** vérifie s'il reste du temps avant de lancer le gameOVER ...utile qd le perso redescend ****/
	private void checkTimeToGameOverLeft(){
		
		timeLeftToGameOver -= Time.deltaTime;
		if(timeLeftToGameOver < 0)
		{
			isInGame = false;
			isGameOver = true;
			//Debug.Log (this.name + " on aperdu !!!!!!!!!!!!!!!!!!!!!!!!!!");
		}
	}
	/** méthode d'apel por la création d'un niveau *****/
	void createWorld(){
		Debug.Log ("on charge le niveau :" + currentLevel);
		this.GetComponent<LoadLevelcontroller> ().loadLevel (currentLevel);
	}

	public void addGameObjectInWorld(GameObject obj,string layerName){

		if(layerName == "foreGround")
			obj.transform.parent = foreLayer.transform;
		else 
			obj.transform.parent = middleLayer.transform;


		/*if(obj.name == "nuage(clone)")
			obj.transform.parent = middleLayer.transform;
		else obj.transform.parent = foreLayer.transform;*/
	}
	public void addObjectPoolerInWorld(GameObject obj,string layerName){
		//listeObjectPoolers.Add (obj);
		dicoObjectPoolers.Add (obj, layerName);
	}

	public void setLevelGravity(float gravity){
		gravityLevel = gravity;	
		//Debug.Log("Gravité du Level  : "+ gravityLevel);
	}

	/*** méthode de remise à zéro de toutes les variables pour pouvoir rejouer sur une meme session ****/
	// à ce stade le nbre de pièces n'est pas remis à jour
	public void resetGame(){
		//altMaxForWinLevel = 8000f * coeffAltitude;
		//altBeginOfSpace = 50f*coeffAltitude;//4000f à remettre

		isWorldMoving = false;
		isGameInPause = false;
		isOverGUIPause = false;
		isInGame = true;
		isGameOver = false;
		isInSpace = false;

		altitude = 0;
		world = GameObject.FindGameObjectWithTag ("World");
		backLayer = GameObject.FindGameObjectWithTag ("BackLayer");
		middleLayer = GameObject.FindGameObjectWithTag ("MiddleLayer");
		foreLayer = GameObject.FindGameObjectWithTag ("ForeLayer");
		backBackLayer = GameObject.FindGameObjectWithTag ("BackBackLayer");

		timeLeftToGameOver = TIME_TO_GAME_OVER;
		createWorld ();
	
	}



	//lancer le joueur et donc faire avancer le monde !!
	public void LaunchPlayer (){
		player.GetComponent <PlayerController>().launchIntheAir ();
		isWorldMoving = true;
		startFlyTime = Time.time;//on note le temps du démarrage du vol !!
		//Debug.Log("au démarre le vol au temps : "+ startFlyTime);
	}

	public static void addPiece(){
		nbrePieces++;
		//Debug.Log("au ajoute une pièce  "+ nbrePieces);
	}

	//mettre le jeu en pause ou le remettre
	public static void tooglePauseGame(){
		isGameInPause = !isGameInPause;
	}

	//permetr de savoir si a cliqué sur un élément GUI
	public void OverGUI(bool value){		
		isOverGUIPause = value;
		Debug.Log("on est sur un element UI ? "+isOverGUIPause);
	}


	public static bool isGamePaused(){return isGameInPause;}
	public static bool isOverGUI(){return isOverGUIPause;}

	void Awake() {
		/*if (Advertisement.isSupported) {
			Advertisement.allowPrecache = true;
			Advertisement.Initialize ("22375");
		} else {
			Debug.Log("Platform not supported");
		}*/
	}
	void OnGUI() {
				if (!isInGame) {
						//if (GUI.Button (new Rect (140, 40, 150, 50), Advertisement.isReady () ? "Montrer ADS" : "En chargement ADS...")) {
								// Show with default zone, pause engine and print result to debug log
				/*Advertisement.Show (null, new ShowOptions {
				pause = true,
				resultCallback = result => {
					Debug.Log(result.ToString());
				}
			});*/
						//}
				}
		}


}
