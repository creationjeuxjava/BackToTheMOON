﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActionButtonManager : MonoBehaviour {

	/*** pour le bouton action ****/
	public GameObject buttonObject;
	Image iconeBoutonAction;

	// Use this for initialization
	void Start () {
		/*** pour le bouton action ****/
		//iconeBoutonAction = GameObject.Find ("actionButton").GetComponent<Image> ();
		iconeBoutonAction = buttonObject.GetComponent<Image> ();
		iconeBoutonAction.sprite = (Sprite)Resources.Load ("Sprites/ui/boutonaction",typeof(Sprite));
		//iconeBoutonAction.sprite = (Sprite)Resources.Load ("None",typeof(Sprite));
	}
	
	// Update is called once per frame
	void Update () {
		if (this.GetComponent<PlayerController>().currentState == PlayerController.State.noAction) {
			//buttonObject.SetActive (false);
			iconeBoutonAction.sprite = (Sprite)Resources.Load ("Sprites/ui/boutonaction",typeof(Sprite));
		} 
		else if (this.GetComponent<PlayerController> ().currentState == PlayerController.State.naked) {
			buttonObject.SetActive (true);
			iconeBoutonAction.sprite = (Sprite)Resources.Load ("Sprites/ui/helico", typeof(Sprite));
		} 
		else if (this.GetComponent<PlayerController> ().currentState == PlayerController.State.laser) {
			buttonObject.SetActive (true);
			iconeBoutonAction.sprite = (Sprite)Resources.Load ("Sprites/ui/sabrelaser", typeof(Sprite));
		} 
		else {
			Debug.Log("pas de state adapté !!!");
		}
	}


}