using UnityEngine;
using System.Collections;

public class DestroyByOutOffBox : MonoBehaviour {


	void OnCollisionEnter2D(Collision2D other) {
		//Debug.Log("Collision other est sorti : "+other.gameObject.name +" qui était en z "+other.transform.position.z);

		PooledObject poolObjectComponent = other.gameObject.GetComponent<PooledObject>();
		if (poolObjectComponent == null) {
				Destroy (other.gameObject);
				Debug.Log ("on supprime le GameObject type collision:" + other.gameObject.name);
		} 
		else {
			other.gameObject.SetActive (false);
			Debug.Log("on desactive "+gameObject.name);	
		
		}

	}

	void OnTriggerEnter2D(Collider2D other) {
		//Debug.Log("TRIGGER ==>Collision other est sorti : "+other.gameObject.name +" qui était en z "+other.transform.position.z);
		PooledObject poolObjectComponent = other.gameObject.GetComponent<PooledObject>();
		if (poolObjectComponent == null) {
				Destroy (other.gameObject);
				Debug.Log ("on supprime le GameObject type trigger:" + other.gameObject.name);
		} 
		else {
			other.gameObject.SetActive (false);
			Debug.Log("on desactive "+gameObject.name);	
		
		}
		
	}
}
