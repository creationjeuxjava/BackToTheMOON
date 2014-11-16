using UnityEngine;
using System.Collections;

public class DestroyByOutOffBox : MonoBehaviour {

	/*void OnTriggerEnter2D(Collider other) {
		Destroy(other.gameObject);
		Debug.Log("Collision other est sorti !!");
	}*/

	//TODO améliorer ce point ...pour l'instant seul le canon est détecté !!
	void OnCollisionEnter2D(Collision2D other) {
		Debug.Log("Collision other est sorti : "+other.gameObject.name +" qui était en z "+other.transform.position.z);
		Destroy(other.gameObject);

	}
}
