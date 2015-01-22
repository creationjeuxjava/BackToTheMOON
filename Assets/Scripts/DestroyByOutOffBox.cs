using UnityEngine;
using System.Collections;

public class DestroyByOutOffBox : MonoBehaviour {


	void OnCollisionEnter2D(Collision2D other) {

		manageDestruction (other.gameObject);
		//Debug.Log ("collision boundary et " + other.gameObject.name);
	}

	void OnTriggerEnter2D(Collider2D other) {
		//Debug.Log ("collision boundary et " + other.gameObject.name);
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
