using UnityEngine;
using System.Collections;

/*
 * * gère la logique du jeu et la création du niveau
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
	private int coeffVitesse= 1* 3600;

	// Use this for initialization
	void Start () {
		altitude = 0;
		world = GameObject.FindGameObjectWithTag ("World");
		backLayer = GameObject.FindGameObjectWithTag ("BackLayer");
		middleLayer = GameObject.FindGameObjectWithTag ("MiddleLayer");
		foreLayer = GameObject.FindGameObjectWithTag ("ForeLayer");
		createWorld ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isWorldMoving == true && !isGameInPause) {
			//world.transform.Translate (PlayerController.vitesse);//on déplace tout le niveau en fonction de la vitesse du joueur !!

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

		createSpriteWorld (meteorite, nbreMeteor,new Vector2(-45,56),new Vector2(300,500));
		createSpriteWorld (nuage, nbreNuages,new Vector2(-40,40),new Vector2(50,250));
		createSpriteWorld (oiseau, nbreNuages,new Vector2(-30,45),new Vector2(20,150));

	}

	/*** méthode de création d'éléments du World *****/
	private void createSpriteWorld(GameObject objectToInstantiate,int number,Vector2 xRange,Vector2 yRange){

		for(int i = 0 ; i < number ; i++){
			Vector3 spawnPosition = new Vector3 (Random.Range(xRange.x,xRange.y),Random.Range(yRange.x,yRange.y),-4.6f);
			Quaternion spawnRotation =  Quaternion.identity;;//Quaternion.Euler(0,0, Random.Range(0, 360) ); //Quaternion.identity;
			GameObject sprite = Instantiate(objectToInstantiate, spawnPosition, spawnRotation) as GameObject;
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

		if(isGameInPause) isGameInPause = false;
		else isGameInPause = true;
	}

	public static bool isGamePaused(){return isGameInPause;}
	public static bool isOverGUI(){return isOverGUIPause;}

	public static void OverGUI(bool value){
		
		isOverGUIPause = value;
	}
}
