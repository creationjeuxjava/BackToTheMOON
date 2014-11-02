using UnityEngine;
using System.Collections;

/*
 * * gère la logique du jeu et la création du niveau
 * */

public class GameController : MonoBehaviour {

	public GameObject meteorite;
	private int nbreMeteor = 5;

	// Use this for initialization
	void Start () {
		Debug.Log ("on crée le monde !!");

		createWorld ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	//a compléter pour créer un niveau complet !!
	void createWorld(){

		for(int i = 0 ; i < nbreMeteor ; i++){
			Vector3 spawnPosition = new Vector3 (Random.Range(-45,56),Random.Range(0,30),0);
			Quaternion spawnRotation =  Quaternion.identity;
			Instantiate(meteorite, spawnPosition, spawnRotation);
			
		}

	}
}
