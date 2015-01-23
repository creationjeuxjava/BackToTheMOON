using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class InGGameGUIController : MonoBehaviour {

	Text nbrPiecesText,nbrDiamantsText,altitudeText;
	Image espaceIcone;
	Animator iconeEspaceAnimator;
	
	GameObject panelVitesse;
	Text vitessePlayer;


	void Start () {
		nbrPiecesText = GameObject.Find ("nbrePiecesText").GetComponent<Text> ();
		nbrDiamantsText = GameObject.Find ("nbreDiamantsText").GetComponent<Text> ();
		altitudeText = GameObject.Find ("altitudeText").GetComponent<Text> ();
		espaceIcone = GameObject.Find ("espaceIcone").GetComponent<Image> ();
		iconeEspaceAnimator = GameObject.Find ("espaceIcone").GetComponent<Animator> ();
		
	
		panelVitesse = GameObject.Find ("PanelInfosVitesse");
		vitessePlayer = GameObject.Find ("infoVitesse").GetComponent<Text> ();
		panelVitesse.SetActive (false);

	}
	

	void Update () {
		nbrPiecesText.text = "x "+GameController.nbrePieces;
		nbrDiamantsText.text = "nonImpl";
		altitudeText.text = GameController.altitude + " m ";


		if (GameController.isInSpace) {
			iconeEspaceAnimator.SetTrigger("beginSpace");
		} 

		if(GameController.lastPlayerSpeed.y >= -0.05f && PlayerController.isFlying){
			vitessePlayer.text = "Votre vitesse est très basse : battez des ailes... !!";
			panelVitesse.SetActive (true);
		}


	}

}
