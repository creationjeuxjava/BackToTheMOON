using UnityEngine;
using System.Collections;

public class controlCollision2 : MonoBehaviour {

	public GameObject explosionPrefab;
	Animator anim;
	bool isHurted = false;
	bool isAlive = true;

	void Update(){

		if (isHurted && isAlive) {
			anim.SetTrigger("explode");	
			GameObject explosion = (GameObject)Instantiate(explosionPrefab);
			GameObject explosion2 = (GameObject)Instantiate(explosionPrefab);
			explosion.transform.position = transform.position;
			explosion2.transform.position = transform.position;
			//particules
			Debug.Log (" HURTED !!");
			isAlive = false;
		}

	}

	public void regenerate(){
		Debug.Log (" regenerate !!");
		isAlive = true;
		isHurted = false;
	}
	void Start(){
		anim = GetComponent<Animator> ();
		Debug.Log (" sart !!");
		if(anim == null)Debug.Log (" pas d'anim !!");
	}
	/*** test des collisions ***/
	void OnCollisionEnter2D(Collision2D other){
		
		
		//Debug.Log (" Ennemi, collision On avec : " + other.gameObject.name);
	}
	
	void OnTriggerEnter2D(Collider2D other){

		isHurted = true;
		//GetComponent<SpriteRenderer> ().color = Color.red;
		//this.renderer.material.color = Color.red;
		//this.renderer.material.color = Color.green;
		Debug.Log (" Ennemi, collision avec Trigger: " + other.gameObject.name);
		//Destroy ();
	}

	void OnTriggerExit2D(Collider2D other){
		//isHurted = false;
		//GetComponent<SpriteRenderer> ().color = Color.white;
		//this.renderer.material.color = Color.red;
		//this.renderer.material.color = Color.green;
		//Debug.Log (" Ennemi, sort avec Trigger: " + other.gameObject.name);
	}
}
