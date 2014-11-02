using UnityEngine;
using System.Collections;

/*
 * * gère la logique du jeu et la création du niveau
 * */

public class GameController : MonoBehaviour {

	public GameObject meteorite;
	public GameObject nuage;
	public GameObject oiseau;
	private int nbreMeteor = 20;
	private int nbreNuages = 5;

	public static GameObject world;//conteneur du world
	private static bool isWorldMoving = false;

	private float altitude;
	private int coeffAltitude = 5;

	// Use this for initialization
	void Start () {
		altitude = 0;
		world = GameObject.FindGameObjectWithTag ("World");
		createWorld ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isWorldMoving == true) {
			world.transform.Translate (PlayerController.vitesse);//on déplace tout le niveau en fonction de la vitesse du joueur !!
			altitude = world.transform.position.y * -1 * coeffAltitude;
			InGameGUI.setMessage("Altitude :"+altitude,"pas de message");
		}
	}



	//a compléter pour créer un niveau complet !!
	void createWorld(){

		createSpriteWorld (meteorite, nbreMeteor,new Vector2(-45,56),new Vector2(100,400));
		createSpriteWorld (nuage, nbreNuages,new Vector2(-55,66),new Vector2(30,70));
		createSpriteWorld (oiseau, nbreNuages,new Vector2(-40,50),new Vector2(30,70));

	}

	/*** méthode de création d'éléments du World *****/
	private void createSpriteWorld(GameObject objectToInstantiate,int number,Vector2 xRange,Vector2 yRange){

		for(int i = 0 ; i < number ; i++){
			Vector3 spawnPosition = new Vector3 (Random.Range(xRange.x,xRange.y),Random.Range(yRange.x,yRange.y),-30f);
			Quaternion spawnRotation =  Quaternion.identity;
			GameObject sprite = Instantiate(objectToInstantiate, spawnPosition, spawnRotation) as GameObject;
			sprite.transform.parent = world.transform;
			
		}
	
	}


	//lancer le joueur et donc faire avancer le monde !!
	public static void LaunchPlayer (){
		PlayerController.launchIntheAir ();
		isWorldMoving = true;
	}
}
