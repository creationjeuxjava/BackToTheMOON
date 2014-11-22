using UnityEngine;
using System.Collections;

public class MenuGUI : MonoBehaviour {

	public GUIStyle menuStyle ;

	// Use this for initialization
	void Start () {
		// on adapte la font
		menuStyle.font = Resources.Load ("Fonts/J-airplane-swash-font") as Font;
		menuStyle.fontSize = AspectUtility.adaptFont(10);
		Debug.Log("MenuGUI: taille adaptée de la font est  :"+menuStyle.fontSize);

		//initialisation du facebook sdk
		enabled = false;
		FB.Init(SetInit, OnHideUnity);
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


		/*** gestion des input **********/
		/*******************************/
		// Groupe centré à l'écran
		float widthBox = AspectUtility.adaptCoordX(300);
		float heightBox = AspectUtility.adaptCoordX(300);
		GUI.BeginGroup (new Rect (Screen.width / 2 - widthBox/2, Screen.height * 0.6f - heightBox / 2, widthBox, heightBox));


		Rect boxRect = AspectUtility.adaptRect(0,0, 300, 300);
		Rect playRect = AspectUtility.adaptRect (300/2-200/2, 100, 200, 70);
		Rect facebookRect = AspectUtility.adaptRect (300/2-200/2, 200, 200, 70);
		Rect quitRect = AspectUtility.adaptRect (300/2-200/2, 300, 200, 70);

		//GUI.Box (boxRect, "Menu");
		/*** lancer le premier niveau ***/
		if (GUI.Button(playRect,"Jouer",menuStyle)) {
			Application.LoadLevel(1);
		}
		/*** quitter le jeu ***/
		if (GUI.Button (quitRect,"Quitter",menuStyle)) {
			Application.Quit();
		}

		if (!FB.IsLoggedIn)                                                                                              
		{                                                                                                               
			if (GUI.Button(facebookRect, "Facebook", menuStyle))                                      
			{                                                                                                            
				FB.Login("email,publish_actions", LoginCallback);                                                        
			}                                                                                                            
		}   

		GUI.EndGroup ();
		/*** quitter le jeu (icone)***/
		/*if (GUI.Button (quitRect, icon)) {
			Application.Quit();
		}*/



	}

	private void SetInit()                                                                       
	{                                                                                            
		Debug.Log("SetInit");                                                                  
		enabled = true; // "enabled" is a property inherited from MonoBehaviour                  
		if (FB.IsLoggedIn)                                                                       
		{                                                                                        
			Debug.Log("Already logged in");                                                    
			OnLoggedIn();                                                                        
		}                                                                                        
	}                                                                                            
	
	private void OnHideUnity(bool isGameShown)                                                   
	{                                                                                            
		Debug.Log("OnHideUnity");                                                              
		if (!isGameShown)                                                                        
		{                                                                                        
			// pause the game - we will need to hide                                             
			Time.timeScale = 0;                                                                  
		}                                                                                        
		else                                                                                     
		{                                                                                        
			// start the game back up - we're getting focus again                                
			Time.timeScale = 1;                                                                  
		}                                                                                        
	}   

	void LoginCallback(FBResult result)                                                        
	{                                                                                          
		Debug.Log("LoginCallback");                                                          
		
		if (FB.IsLoggedIn)                                                                     
		{                                                                                      
			OnLoggedIn();                                                                      
		}                                                                                      
	}                                                                                          
	
	void OnLoggedIn()                                                                          
	{                                                                                          
		Debug.Log("Logged in. ID: " + FB.UserId);                                            
	}       
}
