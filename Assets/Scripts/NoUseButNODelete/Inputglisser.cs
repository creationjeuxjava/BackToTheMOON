﻿using UnityEngine;
using System.Collections;

public class Inputglisser : MonoBehaviour {

	private bool isDragging = false;
	private Vector3 initialDraggingPos;
	private Vector2 touchDeltaPosition;
	private Vector3 lastMouseCoordinate = Vector3.zero;

	// Use this for initialization
	void Start () {
		touchDeltaPosition = Vector2.zero;
	}
	
	// Update is called once per frame
	void Update () {
		/*** android ****/
		if (Input.touchCount == 1) {
			Vector3 touchPosition = Input.GetTouch (0).position;
			touchPosition = camera.ScreenToWorldPoint(touchPosition);
			
			// est- on sur le player ?
			if(gameObject.collider2D.bounds.Contains(new Vector2(touchPosition.x,touchPosition.y))){
				
				//Debug.Log("*************************------>   touche android sur le perso !!");
				if (Input.GetTouch(0).phase == TouchPhase.Moved) {					
					//touchDeltaPosition = Input.GetTouch(0).deltaPosition;
				}
				
			}
			//on relache la touche... le player s'envole !!
			if (Input.GetTouch(0).phase == TouchPhase.Ended) {
				
				//transform.Translate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);
			}
			
			//affichage pour debug
			//DebugInGame.setMessage("centré en ("+gameObject.collider2D.bounds.center.x+" , "+gameObject.collider2D.bounds.center.y+") ",
			                  //   "PlayerController: on touche écran en  (" + touchPosition.x + " , " + touchPosition.y + ")");
		}
		
		
		/**** version pc *****/
		//appui
		if (Input.GetMouseButtonDown (0) && Input.mousePresent && Application.platform != RuntimePlatform.Android 
		    && Application.platform != RuntimePlatform.IPhonePlayer) {
			
			Vector3 touchPosition = Input.mousePosition;
			touchPosition = camera.ScreenToWorldPoint (touchPosition);
			//affichage pour debug
			//DebugInGame.setMessage("centré en ("+gameObject.collider2D.bounds.center.x+" , "+gameObject.collider2D.bounds.center.y+") ",
			                     //"PlayerController: on touche écran en  (" + touchPosition.x + " , " + touchPosition.y + ")");
			
			//Debug.Log("ameObject.collider2D.bounds "+gameObject.collider2D.bounds);
			if (gameObject.collider2D.bounds.Contains (new Vector2 (touchPosition.x, touchPosition.y))) {
				//Debug.Log("iclic perso");
				//si on n'est pas encore en mode dragging, on se met en mode dragging et on note 
				// la position initialle de la souris
				if (!isDragging) {
					isDragging = true;	
					initialDraggingPos = Input.mousePosition;
					
					
					//Debug.Log("initialDraggingPos:"+initialDraggingPos.x);
				} 
				// On regarde de combien la souris a bougé 
				//Vector3 mouseDelta = Input.mousePosition - lastMouseCoordinate;
				// on save la dernière position
				//lastMouseCoordinate = Input.mousePosition;
			}
			
		} 
		
		//relache...le player s'envole mais toujours de la meme quantité...TODO...à améliorer!!
		if(Input.GetMouseButtonUp(0)&& Application.platform != RuntimePlatform.Android 
		   && Application.platform != RuntimePlatform.IPhonePlayer){
			//si on était en mode dragging lorsque la souris a été relachée, on note l'écart de position
			// et on notifie le fait qu'on est plus en mode dragging
			if(isDragging) {
				isDragging = false;
				touchDeltaPosition = Input.mousePosition - initialDraggingPos;
				//Debug.Log("touchDeltaPosition:"+touchDeltaPosition.x);
			}
			//Debug.Log("touch delta :"+touchDeltaPosition);
			//transform.Translate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);
			
			
			//rigidbody2D.AddForce(new Vector2(touchDeltaPosition.x* amplificationMove,touchDeltaPosition.y* amplificationMove));
		}
	}

	/*void OnMouseOver()
	{
		Debug.Log ("souris au dessus de : "+gameObject.name);
	}*/
	
	void OnMouseDown()
	{
		//Debug.Log ("souris clic  dessus de : "+gameObject.name);
		//translation.x = 0;
	}
}
