using UnityEngine;
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
		Debug.Log ("Mouse en : " + Input.mousePosition+" et Sreen : "+Screen.height);
		Debug.Log ("REct en : " + pauseRect.position + " et souris en "+realY);
		if (pauseRect.Contains (new Vector3(Input.mousePosition.x,realY,Input.mousePosition.z))) {
			GameController.OverGUI(true);
		
		}
		else GameController.OverGUI(false);
	}

	/**** Eléments GUI du jeu ****/
	void OnGUI () {
		GUI.skin.GetStyle ("Label").fontSize = 12;
		GUI.Label(AspectUtility.adaptRect(30,120,500,100),message);
		GUI.Label(AspectUtility.adaptRect(30,90,500,100),message2);

		//GUI.Label (new Rect( 70 ,50,30,30),iconCarte);
		//GUI.Label (new Rect( 100 ,55,50,50),""+infosGameObject);

		/*** la vitesse du player ****/
		//GUI.DrawTexture(new Rect(150 ,20,100,30), barreVitesse, ScaleMode.StretchToFill, true, 0f);//ScaleMode.ScaleToFit	
		float vitesse = PlayerController.vitesse.y * -1 * coeffVitesse;
		GUI.DrawTexture(new Rect(100 ,20,vitesse,30), barreVitesse);

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
	}


	/*** mise à jour du message du Label  *****/
	public static void setMessage(string msg,string msg2){

		message = "Infos Player => "+msg2;
		message2 = "Altimètre => "+msg;
	}
}
