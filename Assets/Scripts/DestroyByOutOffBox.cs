using UnityEngine;
using System.Collections;

public class DestroyByOutOffBox : MonoBehaviour {


	void OnCollisionEnter2D(Collision2D other) {
		//Debug.Log("Collision other est sorti : "+other.gameObject.name +" qui était en z "+other.transform.position.z);

		PooledObject poolObjectComponent = other.gameObject.GetComponent<PooledObject>();
		if (poolObjectComponent == null) {
			Destroy (other.gameObject);
		} 

	}

	void OnTriggerEnter2D(Collider2D other) {
		//Debug.Log("TRIGGER ==>Collision other est sorti : "+other.gameObject.name +" qui était en z "+other.transform.position.z);
		PooledObject poolObjectComponent = other.gameObject.GetComponent<PooledObject>();
		if (poolObjectComponent == null) {
			Destroy (other.gameObject);
		} 
		
	}
}
