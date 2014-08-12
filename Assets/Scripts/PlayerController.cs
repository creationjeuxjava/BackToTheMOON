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
			Vector3 touchPosition = Input.mousePosition;
			//touchPosition = AspectUtility.mousePosition;
			touchPosition = AspectUtility.mouseInvertPosition;
			Debug.Log("clic gauche en ("+Input.mousePosition.x+" , "+Input.mousePosition.y+")");
			Debug.Log("***clic gauche converti ("+touchPosition.x+" , "+touchPosition.y+")");
			Debug.Log("perso centre en ("+gameObject.collider2D.bounds.center.x+" , "+gameObject.collider2D.bounds.center.y+")");

			//affichage pour debug
			InGameGUI.setMessage("perso centre en ("+gameObject.collider2D.bounds.center.x+" , "+gameObject.collider2D.bounds.center.y+")",
			                     "***clic gauche converti ("+touchPosition.x+" , "+touchPosition.y+")");

			if(gameObject.collider2D.bounds.Contains(new Vector2(Input.mousePosition.x,Input.mousePosition.y))){

				Debug.Log("*************************------>   clic gauche sur le perso !!");
			}
		}
					
	}

	void OnMouseOver()
	{
		Debug.Log ("souris au dessus de : "+gameObject.name);
	}

	void OnMouseDown()
	{
		Debug.Log ("souris clic  dessus de : "+gameObject.name);
	}
}
