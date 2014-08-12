using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    bool isMoving;
	float speed = 0.5F;
	float amplificationMove = 10f;
	Vector2 touchDeltaPosition;
	Vector3 lastMouseCoordinate = Vector3.zero;

	public Camera camera;

	// Use this for initialization
	void Start () {
		touchDeltaPosition = Vector2.zero;
		//Debug.Log("PlayerController-->création");
	}
	
	// Update is called once per frame
	void Update () {

		/*** android ****/
		if (Input.touchCount == 1) {
			Vector3 touchPosition = Input.GetTouch (0).position;
			touchPosition = camera.ScreenToWorldPoint(touchPosition);

			// est- on sur le player ?
			if(gameObject.collider2D.bounds.Contains(new Vector2(touchPosition.x,touchPosition.y))){
				
				Debug.Log("*************************------>   touche android sur le perso !!");
				if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {					
					touchDeltaPosition = Input.GetTouch(0).deltaPosition;
				}

			}
			//on relache la touche... le player s'envole !!
			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) {

				transform.Translate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);
			}

			//affichage pour debug
			InGameGUI.setMessage("perso centre en ("+gameObject.collider2D.bounds.center.x+" , "+gameObject.collider2D.bounds.center.y+")",
			                     "PlayerController: on touche écran en  (" + touchPosition.x + " , " + touchPosition.y + ")");
		}


		/**** version pc *****/
		//appui
		if (Input.GetMouseButtonDown (0)&& Input.mousePresent && Application.platform != RuntimePlatform.Android 
		    	&& Application.platform != RuntimePlatform.IPhonePlayer) {

			Vector3 touchPosition = Input.mousePosition;
			touchPosition = camera.ScreenToWorldPoint(touchPosition);
			//affichage pour debug
			InGameGUI.setMessage("perso centre en ("+gameObject.collider2D.bounds.center.x+" , "+gameObject.collider2D.bounds.center.y+")",
			                     "***clic gauche converti ("+touchPosition.x+" , "+touchPosition.y+")");

			if(gameObject.collider2D.bounds.Contains(new Vector2(touchPosition.x,touchPosition.y))){

				Debug.Log("*************************------>   clic gauche sur le perso !!");
				// On regarde de combien la souris a bougé 
				Vector3 mouseDelta = Input.mousePosition - lastMouseCoordinate;
				touchDeltaPosition = camera.ScreenToWorldPoint(mouseDelta);
				
				// on save la dernière position
				lastMouseCoordinate = Input.mousePosition;
			}
		}
		//relache...le player s'envole mais toujours de la meme quantité...TODO...à améliorer!!
		if(Input.GetMouseButtonUp(0)&& Application.platform != RuntimePlatform.Android 
		   && Application.platform != RuntimePlatform.IPhonePlayer){

			//transform.Translate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);
			rigidbody2D.AddForce(new Vector2(-touchDeltaPosition.x* amplificationMove,-touchDeltaPosition.y* amplificationMove));
		}
					
	}

	/*void OnMouseOver()
	{
		Debug.Log ("souris au dessus de : "+gameObject.name);
	}

	void OnMouseDown()
	{
		Debug.Log ("souris clic  dessus de : "+gameObject.name);
	}*/
}
