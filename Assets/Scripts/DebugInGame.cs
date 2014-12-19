using UnityEngine;
using System.Collections;

public class DebugInGame : MonoBehaviour {

	private static string message,message2;
	// Use this for initialization
	void Start () {
		message = "Infos Player =>";
		message2 = "Altimètre : ";
	}
	


	/**** Eléments GUI du jeu ****/
	void OnGUI () {
		GUI.skin.GetStyle ("Label").fontSize = 12;
		
		/*** juste utiles au debug ****/
		GUI.Label (new Rect (30, 120, 600, 100), message);
		GUI.Label (new Rect (30, 90, 600, 100), message2);
		GUI.Label (new Rect (30, 150, 600, 100), "isWithCask : " + PlayerController.isWithCask + " || isWithShoe : " + PlayerController.isWithShoe);
		GUI.Label (new Rect (30, 170, 600, 100), "isFlyBegin : " + PlayerController.isFlyBegin);
		GUI.Label (new Rect (30, 190, 600, 100), "Vector3 Vplayer ==> " + PlayerController.vitesse);
	}

	/*** mise à jour du message du Label  *****/
	public static void setMessage(string msg,string msg2){
		
		message = "Infos Player => "+msg2;
		message2 = "Altimètre => "+msg;
	}
}
