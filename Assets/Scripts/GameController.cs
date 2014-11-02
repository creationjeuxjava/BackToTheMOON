using UnityEngine;
using System.Collections;

/*
 * * gère la logique du jeu et la création du niveau
 * */

public class GameController : MonoBehaviour {

	public GameObject meteorite;
	private int nbreMeteor = 5;

	private GameObject world;//conteneur du world
	private static bool isWorldMoving = false;

	// Use this for initialization
	void Start () {
		Debug.Log ("on crée le monde !!");
		world = GameObject.FindGameObjectWithTag ("World");
		createWorld ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isWorldMoving == true) {
				
			world.transform.Translate (new Vector3 (0,-0.2f,0));
		}
	}


	//a compléter pour créer un niveau complet !!
	void createWorld(){

		for(int i = 0 ; i < nbreMeteor ; i++){
			Vector3 spawnPosition = new Vector3 (Random.Range(-45,56),Random.Range(0,30),0);
			Quaternion spawnRotation =  Quaternion.identity;
			GameObject sprite = Instantiate(meteorite, spawnPosition, spawnRotation) as GameObject;
			sprite.transform.parent = world.transform;
			
		}

	}


	//lancer le joueur et donc faire avancer le monde !!
	public static void LaunchPlayer (){
		isWorldMoving = true;
	}
}
