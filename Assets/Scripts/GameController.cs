using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor;

/*
 * * gère la logique du jeu et la création du niveau par le biais, à terme de LoadLevelController
 * */

public class GameController : MonoBehaviour {

	public GameObject meteorite;
	public GameObject nuage;
	public GameObject oiseau;
	private int nbreMeteor = 30;
	private int nbreNuages = 15;
	public GameObject player;

	public static GameObject world;//conteneur du world
	private static bool isWorldMoving = false;
	private static bool isGameInPause = false;
	private static bool isOverGUIPause = false;
	private GameObject backLayer, middleLayer, foreLayer;


	private float altitude;
	private int coeffAltitude = 5;
	private int coeffVitesse = 1 * 3600; 
	private int currentLevel = 1; //par défaut le premier niveau
	private List<GameObject> listeGameObjects = new List<GameObject>();

	// Use this for initialization
	void Start () {
		altitude = 0;
		world = GameObject.FindGameObjectWithTag ("World");
		backLayer = GameObject.FindGameObjectWithTag ("BackLayer");
		middleLayer = GameObject.FindGameObjectWithTag ("MiddleLayer");
		foreLayer = GameObject.FindGameObjectWithTag ("ForeLayer");
		createWorld ();

		//création aléatoire de bonus en l'air =>AssetDatabase non valable dans le build...remplacé par Resources.load !!
		//TODO à généraliser  !!
		//Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/world1/cask.prefab", typeof(GameObject));
		/*Object prefab = Resources.Load<Object>("Prefabs/cask");

		Vector3 pos = new Vector3(0,50,-4.6f);
		GameObject clone = Instantiate(prefab, pos, Quaternion.identity) as GameObject;
		clone.transform.parent = foreLayer.transform;


		Object prefab2 = Resources.Load<Object>("Prefabs/shoe");		
		Vector3 pos2 = new Vector3(0,100,-4.6f);
		GameObject clone2 = Instantiate(prefab2, pos2, Quaternion.identity) as GameObject;
		clone2.transform.parent = foreLayer.transform;*/
	}
	
	// Update is called once per frame
	void Update () {
		if (isWorldMoving == true && !isGameInPause) {
			//world.transform.Translate (PlayerController.vitesse);//on déplace tout le niveau en fonction de la vitesse du joueur !!

			//chaque partie avance à une vitesse différente == parallax
			backLayer.transform.Translate (PlayerController.vitesse / 3);
			middleLayer.transform.Translate (PlayerController.vitesse / 1.5f);
			foreLayer.transform.Translate (PlayerController.vitesse);

			altitude = foreLayer.transform.position.y * -1 * coeffAltitude;
			float vitesse = PlayerController.vitesse.y*-1 * coeffVitesse;
			InGameGUI.setMessage("Altitude :"+altitude,"Vitesse Player : "+vitesse+" km/h");



		}
	}



	//a compléter pour créer un niveau complet !!
	void createWorld(){
		this.GetComponent<LoadLevelcontroller> ().loadLevel (currentLevel);
		/*createSpriteWorld (meteorite, nbreMeteor,new Vector2(-45,56),new Vector2(300,500));
		createSpriteWorld (nuage, nbreNuages,new Vector2(-40,40),new Vector2(50,250));
		createSpriteWorld (oiseau, nbreNuages,new Vector2(-30,45),new Vector2(20,150));*/

	}

	public void addGameObjectInWorld(GameObject obj){
		obj.transform.parent = foreLayer.transform;
		listeGameObjects.Add (obj);
	}

	/*** méthode de création d'éléments du World *****/
	private void createSpriteWorld(GameObject objectToInstantiate,int number,Vector2 xRange,Vector2 yRange){

		for(int i = 0 ; i < number ; i++){
			Vector3 spawnPosition = new Vector3 (Random.Range(xRange.x,xRange.y),Random.Range(yRange.x,yRange.y),-4.6f);
			Quaternion spawnRotation =  Quaternion.identity;;//Quaternion.Euler(0,0, Random.Range(0, 360) ); //Quaternion.identity;
			GameObject sprite = Instantiate(objectToInstantiate, spawnPosition, spawnRotation) as GameObject;
			//sprite.rigidbody.angularVelocity = Random.insideUnitSphere * 2f;
			sprite.transform.parent = foreLayer.transform;
			//sprite.transform.parent = world.transform;
			
		}
	
	}


	//lancer le joueur et donc faire avancer le monde !!
	public void LaunchPlayer (){
		player.GetComponent <PlayerController>().launchIntheAir ();
		isWorldMoving = true;
	}

	//mettre le jeu en pause ou le remettre
	public static void tooglePauseGame(){

		isGameInPause = !isGameInPause;
	}

	//
	public static void OverGUI(bool value){		
		isOverGUIPause = value;
	}

	public static bool isGamePaused(){return isGameInPause;}
	public static bool isOverGUI(){return isOverGUIPause;}


}
