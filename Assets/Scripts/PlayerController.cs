using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    bool isMoving;
	// Use this for initialization
	void Start () {
		Debug.Log("PlayerController-->création");
	}
	
	// Update is called once per frame
	void Update () {

		/*** android ****/
		if (Input.touchCount == 1) {
			Vector3 touchPosition = Input.GetTouch (0).position;
			Debug.Log ("PlayerController: on touche écran en  (" + touchPosition.x + " , " + touchPosition.y + ")");
		}
		/**** version pc *****/
		if (Input.GetMouseButton (0)) {
			Debug.Log("clic gauche en ("+Input.mousePosition.x+" , "+Input.mousePosition.y+")");
			if(gameObject.rigidbody2D.collider.bounds.Contains(new Vector2(Input.mousePosition.x,Input.mousePosition.y))){

				Debug.Log("clic gauche sur le perso !!");
			}
		}
					
	}
}
