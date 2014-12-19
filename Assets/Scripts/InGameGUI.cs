﻿using UnityEngine;
using System.Collections;

public class InGameGUI : MonoBehaviour {

	private static string message,message2;
	private int buttonSize = 40;
	private int coeffVitesse = 1000;
	private Rect pauseRect;
	public Texture2D iconCarte;
	public Texture2D iconPause;
	public Texture2D iconRejouer;
	public Texture2D barreVitesse;

	// Use this for initialization
	void Start () {
		message = "Player en : ";
		message2 = "Touch en : ";
		pauseRect = new Rect (Screen.width - 80, 20, buttonSize, buttonSize);
	}
	
	// Update is called once per frame
	void Update () {
		float realY =  Screen.height - Input.mousePosition.y;
		//Debug.Log ("Mouse en : " + Input.mousePosition+" et Sreen : "+Screen.height);
		//Debug.Log ("REct en : " + pauseRect.position + " et souris en "+realY);
		if (pauseRect.Contains (new Vector3(Input.mousePosition.x,realY,Input.mousePosition.z))) {
			GameController.OverGUI(true);
		
		}
		else GameController.OverGUI(false);
	}

	/**** Eléments GUI du jeu ****/
	void OnGUI () {
		GUI.skin.GetStyle ("Label").fontSize = 12;

		/*** juste utiles au debug ****/
		GUI.Label(new Rect(30,120,600,100),message);
		GUI.Label(new Rect(30,90,600,100),message2);
		GUI.Label(new Rect(30,150,600,100),"isWithCask : "+PlayerController.isWithCask +" || isWithShoe : "+PlayerController.isWithShoe);
		GUI.Label(new Rect(30,170,600,100),"isFlyBegin : "+PlayerController.isFlyBegin);

		//GUI.Label(AspectUtility.adaptRect(30,120,600,100),message);
		//GUI.Label(AspectUtility.adaptRect(30,90,600,100),message2);


		//GUI.Label (new Rect( 70 ,50,30,30),iconCarte);
		//GUI.Label (new Rect( 100 ,55,50,50),""+infosGameObject);

		/*** la vitesse du player ****/
		if (GameController.isInGame) {
			float vitesse = PlayerController.vitesse.y * -1 * coeffVitesse;
			GUI.DrawTexture(new Rect(100 ,20,vitesse,30), barreVitesse);
		
		}


		/***** retour à la carte  ****/
		if (GUI.Button (new Rect (40,Screen.height - 80,buttonSize,buttonSize), iconCarte)) {
			Application.LoadLevel (1);
		}
		/**** pause ****/
		if (GUI.Button (pauseRect, iconPause)) {
			GameController.tooglePauseGame();
		}
		/**** rejouer *****/
		if (GUI.Button (new Rect (40,20,buttonSize,buttonSize), iconRejouer)) {
			Application.LoadLevel (0);
		}


		if (!GameController.isInGame) {
			string msg ="";
			if(GameController.isGameOver) msg = "GameOver";
			else msg = " WIN";
			GUI.Box(new Rect(Screen.width/2-200/2, Screen.height/2-400/2, 200, 200), msg);
			if (GUI.Button (new Rect (Screen.width/2-150/2,Screen.height/2-200/2,100,50), "Quitter")) {
				Application.LoadLevel (0);
			}
		}
			
	}


	/*** mise à jour du message du Label  *****/
	public static void setMessage(string msg,string msg2){

		message = "Infos Player => "+msg2;
		message2 = "Altimètre => "+msg;
	}
}
