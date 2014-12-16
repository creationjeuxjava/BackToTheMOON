using UnityEngine;
using System.Collections;

public class DestroyByOutOffBox : MonoBehaviour {


	void OnCollisionEnter2D(Collision2D other) {

		manageDestruction (other.gameObject);
	}

	void OnTriggerEnter2D(Collider2D other) {

		manageDestruction (other.gameObject);
	}


	private void manageDestruction(GameObject obj){
		PooledObject poolObjectComponent = obj.GetComponent<PooledObject>();
		if (poolObjectComponent == null) {
			Destroy (obj.gameObject);
			//Debug.Log ("on supprime le GameObject type trigger:" + obj.name);
		} 
		else {
			obj.SetActive (false);
			//Debug.Log("on desactive "+obj.name);	
			
		}

	}
}
