using UnityEngine;
using System.Collections;

public class MenuGUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {
		// Groupe centré à l'écran
		// All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.
		/*GUI.BeginGroup (new Rect (Screen.width / 2 - 50, Screen.height / 2 - 50, 100, 150));		
		GUI.Box (new Rect (0,0,100,150), "Menu");
		GUI.Button (new Rect (10,40,80,30), "Jouer");
		GUI.Button (new Rect (10,80,80,30), "Quitter");		
		GUI.EndGroup ();*/


		/**** tentative de scaler les élemts GUI *****/
		Vector3 scale = new Vector3();
		
		scale.x = Screen.width/800.0f; // calculate hor scale
		scale.y = Screen.height/1280.0f; // calculate vert scale
		scale.z = 1;
		Matrix4x4 svMat = GUI.matrix; // save current matrix
		
		
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);


		/*** gestion des input **********/
		/*******************************/
		// Groupe centré à l'écran
		GUI.BeginGroup (new Rect (Screen.width / 2 - Screen.width * 0.4f/2, Screen.height / 2 - 50, Screen.width * 0.4f, Screen.height *0.2f));

		/*Rect boxRect = new Rect (0, 0, AspectUtility.adaptCoordX(100),  AspectUtility.adaptCoordY(150));
		Rect boxRect = new Rect (0, 0, Screen.width * 0.4f, Screen.height *0.2f);
		Rect playRect = new Rect (10, 40, 80, 30);
		Rect quitRect = new Rect (10, 80, 80, 30);*/

		Rect boxRect = new Rect (0, 0, 100, 150);
		Rect playRect = new Rect (10, 40, 80, 30);
		Rect quitRect = new Rect (10, 80, 80, 30);



		GUI.Box (boxRect, "Menu");
		/*** lancer le premier niveau ***/
		if (GUI.Button(playRect,"Jouer")) {
			Application.LoadLevel(0);
		}
		/*** quitter le jeu ***/
		if (GUI.Button (quitRect,"Quitter")) {
			Application.Quit();
		}

		GUI.EndGroup ();
		/*** quitter le jeu (icone)***/
		/*if (GUI.Button (quitRect, icon)) {
			Application.Quit();
		}*/
	}
}
