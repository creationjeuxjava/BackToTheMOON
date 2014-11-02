using UnityEngine;
using System.Collections;

public class InGameGUI : MonoBehaviour {

	private static string message,message2;

	// Use this for initialization
	void Start () {
		message = "Player en : ";
		message2 = "Touch en : ";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/**** juste pour le debug actuellemtn, mais pourra contenir les éléments GUI du jeu ****/
	void OnGUI () {
		GUI.skin.GetStyle ("Label").fontSize = 12;
		GUI.Label(AspectUtility.adaptRect(300,120,500,100),message);
		GUI.Label(AspectUtility.adaptRect(300,50,500,100),message2);
	}


	/*** mise à jour du message du Label  *****/
	public static void setMessage(string msg,string msg2){

		message = "Infos Player : "+msg;
		message2 = "Touch en : "+msg2;
	}
}
