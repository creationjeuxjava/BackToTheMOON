using UnityEngine;
using System.Collections;

public class DebugInGame : MonoBehaviour {

	private static string message,message2;
	private int startY;
	// Use this for initialization
	void Start () {
		startY = 80;
		message = "Infos Player =>";
		message2 = "Altimètre : ";
	}
	


	/**** Eléments GUI du jeu ****/
	void OnGUI () {
		GUI.skin.GetStyle ("Label").fontSize = 12;
		
		/*** juste utiles au debug ****/

		GUI.Label (new Rect (30, startY, 600, 100), message2);
		GUI.Label (new Rect (30, getNextY(1), 600, 100), message);
		GUI.Label (new Rect (30, getNextY(2), 600, 100), "isWithCask : " + PlayerController.isWithCask + " || isWithShoe : " + PlayerController.isWithShoe);
		GUI.Label (new Rect (30, getNextY(3), 600, 100), "isFlyBegin : " + PlayerController.isFlyBegin);
		GUI.Label (new Rect (30, getNextY(4), 600, 100), "Vector3 Vplayer ==> " + PlayerController.vitesse);
		GUI.Label (new Rect (30, getNextY(5), 600, 100), "Vector3 Translationplayer ==> " + PlayerController.translation);
	}

	/*** mise à jour du message du Label  *****/
	public static void setMessage(string msg,string msg2){
		
		message = "Infos Player => "+msg2;
		message2 = "Altimètre => "+msg;
	}

	private int getNextY(int indice){
		int nextY = startY + 20 * indice;
		return nextY;
	}
}
