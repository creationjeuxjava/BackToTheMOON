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
	private GameObject backLayer, middleLayer, foreLayer,backBackLayer;


	private float altitude;
	private static int nbrePieces = 0;
	private float altMaxForWinLevel = 10000f;
	private int coeffAltitude = 5;
	private int coeffVitesse = 1 * 3600; 
	private int currentLevel = 1; //par défaut le premier niveau
	private List<GameObject> listeObjectPoolers = new List<GameObject>();

	// Use this for initialization
	void Start () {
		isInGame = true;
		isGameOver = false;
		altitude = 0;
		world = GameObject.FindGameObjectWithTag ("World");
		backLayer = GameObject.FindGameObjectWithTag ("BackLayer");
		middleLayer = GameObject.FindGameObjectWithTag ("MiddleLayer");
		foreLayer = GameObject.FindGameObjectWithTag ("ForeLayer");
		backBackLayer = GameObject.FindGameObjectWithTag ("BackBackLayer");
		createWorld ();

		//création aléatoire de bonus en l'air =>AssetDatabase non valable dans le build...remplacé par Resources.load !!
		//TODO à généraliser  !!
		//Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/world1/cask.prefab", typeof(GameObject));
		//Object prefab = Resources.Load<Object>("Prefabs/cask");


	}
	
	// Update is called once per frame
	void Update () {

		/** a-t-on besoin d'activer des GO depuis les ObjectPooler ****/
		for (int i = 0; i < listeObjectPoolers.Count; i++) {

			Vector2 minMax = listeObjectPoolers[i].GetComponent<ObjectPooler>().getMinMax();
			//Debug.Log("Position du ForeLayer : "+foreLayer.transform.position.y*(-1)+" et min "+minMax.x+" et max "+minMax.y +" pour GO "+listeObjectPoolers[i].name);
			if(foreLayer.transform.position.y * (-1) > minMax.x && foreLayer.transform.position.y * (-1 )< minMax.y){
				GameObject obj = listeObjectPoolers[i].GetComponent<ObjectPooler>().GetPooledObject();
				if(obj != null){
					//Debug.Log(listeObjectPoolers[i].name +" crée !");

					obj.transform.position = new Vector3(Random.Range(-14,14),Random.Range(20,60),-4.6f);
					obj.SetActive(true);
					obj.transform.parent = foreLayer.transform;
				}

			}
			else{

				//Debug.Log(listeObjectPoolers[i].name +" : On ne peut pas créer le prefab => alt non correcte !");
			}
		}



		if (isWorldMoving == true && !isGameInPause && isInGame) {

			//chaque partie avance à une vitesse différente == parallax
			backBackLayer.transform.Translate (PlayerController.vitesse / 3.7f);
			backLayer.transform.Translate (PlayerController.vitesse / 3);
			middleLayer.transform.Translate (PlayerController.vitesse / 1.5f);
			foreLayer.transform.Translate (PlayerController.vitesse);

			altitude = foreLayer.transform.position.y * -1 * coeffAltitude;
			float vitesse = PlayerController.vitesse.y*-1 * coeffVitesse;
			InGameGUI.setMessage("Altitude :"+altitude,"Vitesse Player : "+vitesse+" km/h et nbre pièces : "+nbrePieces);

			if(PlayerController.vitesse.y > 0){
				isInGame = false;
				isGameOver = true;
			}
			if(altitude >= altMaxForWinLevel){
				isInGame = false;
			}

		}
	}



	/** méthode d'apel por la création d'un niveau *****/
	void createWorld(){
		this.GetComponent<LoadLevelcontroller> ().loadLevel (currentLevel);
	}

	public void addGameObjectInWorld(GameObject obj){
		obj.transform.parent = foreLayer.transform;
	}

	public void addObjectPoolerInWorld(GameObject obj){
		listeObjectPoolers.Add (obj);
	}


	//lancer le joueur et donc faire avancer le monde !!
	public void LaunchPlayer (){
		player.GetComponent <PlayerController>().launchIntheAir ();
		isWorldMoving = true;
	}

	public static void addPiece(){

		nbrePieces++;
	}

	//mettre le jeu en pause ou le remettre
	public static void tooglePauseGame(){

		isGameInPause = !isGameInPause;
	}

	//permetr de savoir si a cliqué sur un élément GUI
	public static void OverGUI(bool value){		
		isOverGUIPause = value;
	}

	public static bool isGamePaused(){return isGameInPause;}
	public static bool isOverGUI(){return isOverGUIPause;}


}
