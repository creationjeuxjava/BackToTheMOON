using UnityEngine;
using System.Collections;

public class MenuGUI : MonoBehaviour {

	public GUIStyle menuStyle ;

	// Use this for initialization
	void Start () {
		menuStyle.font = Resources.Load ("Fonts/J-airplane-swash-font") as Font;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {

		/**** tentative de scaler les élemts GUI *****/
		/*Vector3 scale = new Vector3();		
		scale.x = Screen.width/800.0f; // calculate hor scale
		scale.y = Screen.height/1280.0f; // calculate vert scale
		scale.z = 1;
		Matrix4x4 svMat = GUI.matrix; // save current matrix		
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);
		   */
		/***** le résultat est criticable ... ***/


		//GUIStyle style = GUI.skin.GetStyle("Label");
		//style.fontSize = 30;
		menuStyle.fontSize = AspectUtility.adaptFont(30);
		Debug.Log("MenuGUI: taille adaptée de la font est  :"+menuStyle.fontSize);


		/*** gestion des input **********/
		/*******************************/
		// Groupe centré à l'écran
		float widthBox = AspectUtility.adaptCoordX(300);
		float heightBox = AspectUtility.adaptCoordX(300);
		GUI.BeginGroup (new Rect (Screen.width / 2 - widthBox/2, Screen.height * 0.6f - heightBox / 2, widthBox, heightBox));


		Rect boxRect = AspectUtility.adaptRect(0,0, 300, 300);
		Rect playRect = AspectUtility.adaptRect (300/2-200/2, 100, 200, 70);
		Rect quitRect = AspectUtility.adaptRect (300/2-200/2, 200, 200, 70);

		//GUI.Box (boxRect, "Menu");
		/*** lancer le premier niveau ***/
		if (GUI.Button(playRect,"Jouer",menuStyle)) {
			Application.LoadLevel(1);
		}
		/*** quitter le jeu ***/
		if (GUI.Button (quitRect,"Quitter",menuStyle)) {
			Application.Quit();
		}

		GUI.EndGroup ();
		/*** quitter le jeu (icone)***/
		/*if (GUI.Button (quitRect, icon)) {
			Application.Quit();
		}*/



	}
}
