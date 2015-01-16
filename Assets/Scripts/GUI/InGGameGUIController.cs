using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class InGGameGUIController : MonoBehaviour {

	Text nbrPiecesText,nbrDiamantsText,altitudeText;
	//Image pieceIcone,diamantIcone;
	Image espaceIcone;

	// Use this for initialization
	void Start () {
		nbrPiecesText = GameObject.Find ("nbrePiecesText").GetComponent<Text> ();
		nbrDiamantsText = GameObject.Find ("nbreDiamantsText").GetComponent<Text> ();
		altitudeText = GameObject.Find ("altitudeText").GetComponent<Text> ();
		espaceIcone = GameObject.Find ("espaceIcone").GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		nbrPiecesText.text = "x "+GameController.nbrePieces;
		nbrDiamantsText.text = "x nonImpl";
		altitudeText.text = GameController.altitude + " m ";


		if (GameController.isInSpace) {
			espaceIcone.color = Color.green;		
		} 
		else {
				
			espaceIcone.color = Color.red;
		}
	}
}
