﻿using UnityEngine;
using System.Collections;

public class DestroyByOutOffBox : MonoBehaviour {


	void update(){

		//transform.Translate (PlayerController.vitesse * (-1));
	}

	//TODO améliorer ce point ...pour l'instant seul le canon est détecté !!
	void OnCollisionEnter2D(Collision2D other) {
		Debug.Log("Collision other est sorti : "+other.gameObject.name +" qui était en z "+other.transform.position.z);
		Destroy(other.gameObject);

	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("TRIGGER ==>Collision other est sorti : "+other.gameObject.name +" qui était en z "+other.transform.position.z);
		Destroy(other.gameObject);
		
	}
}
