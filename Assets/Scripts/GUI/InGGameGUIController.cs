using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class InGGameGUIController : MonoBehaviour {

	public Camera camera;

	Text nbrPiecesText,nbrDiamantsText,altitudeText,score;
	Image espaceIcone;
	Animator iconeEspaceAnimator;

	GameObject panelVitesse;
	Text vitessePlayer;

	GameObject endLevelPanel;
	Text endText;
	GameObject NextLevelButton;

	GameObject PhoenixButton;


	void Start () {
		nbrPiecesText = GameObject.Find ("nbrePiecesText").GetComponent<Text> ();
		nbrDiamantsText = GameObject.Find ("nbreDiamantsText").GetComponent<Text> ();
		altitudeText = GameObject.Find ("altitudeText").GetComponent<Text> ();
		espaceIcone = GameObject.Find ("espaceIcone").GetComponent<Image> ();
		iconeEspaceAnimator = GameObject.Find ("espaceIcone").GetComponent<Animator> ();
		score = GameObject.Find ("score").GetComponent<Text> ();
		
	
		panelVitesse = GameObject.Find ("PanelInfosVitesse");
		vitessePlayer = GameObject.Find ("infoVitesse").GetComponent<Text> ();
		panelVitesse.SetActive (false);

		PhoenixButton =  GameObject.Find ("PhoenixButton");
		PhoenixButton.SetActive (false);
		NextLevelButton = GameObject.Find ("NextLevelButton");
		NextLevelButton.SetActive (false);
		endLevelPanel = GameObject.Find ("endLevelPanel");
		endText = GameObject.Find ("endText").GetComponent<Text> ();
		endLevelPanel.SetActive (false);


	}
	

	void Update () {
		nbrPiecesText.text = "x "+GameController.nbrePieces;
		nbrDiamantsText.text = "x "+GameController.nbreDiamond;
		altitudeText.text = GameController.altitude + " m ";
		score.text = GameController.score.ToString();

		/**** gestion de l'icone indiquant si on est dans l'espace ****/
		if (GameController.isInSpace) {
			iconeEspaceAnimator.SetTrigger("beginSpace");
		} 

		/*** gestion du message du panel d'alerte de vitesse ***/
		if (PlayerController.vitesse.y >= -0.08f && PlayerController.vitesse.y < 0f && PlayerController.isFlying ) {
			if (GameController.isInSpace) {
				vitessePlayer.text = "Votre vitesse est très basse : \n Vous risquez le GameOver !!";
			} 
			else{
				vitessePlayer.text = "Votre vitesse est très basse : battez des ailes... !!";
			}
			panelVitesse.SetActive (true);
		}
		else if(PlayerController.vitesse.y >= 0f && PlayerController.isFlying ){
			if (GameController.isInSpace) {
				vitessePlayer.text = "Votre vitesse est très basse : \n Temps restant avt GameOver : "+(int)GameController.timeLeftToGameOver+" s";
			} 
			else{
				vitessePlayer.text = "Votre vitesse est très basse, battez des ailes... !!\n Temps restant avant GameOver : "+(int)GameController.timeLeftToGameOver+" s";
			}
			panelVitesse.SetActive (true);

		}
		else {
			panelVitesse.transform.position = camera.WorldToScreenPoint(PlayerController.actualPosition);
			panelVitesse.SetActive (false);			
		}



		/*if (PlayerController.vitesse.y >= -0.08f && PlayerController.isFlying && !GameController.isInSpace) {
			vitessePlayer.text = "Votre vitesse est très basse : battez des ailes... !!\n Temps restant avt GameOver : "+(int)GameController.timeLeftToGameOver+" s";
			panelVitesse.SetActive (true);
		}
		else if(PlayerController.vitesse.y >= -0.08f && PlayerController.isFlying && GameController.isInSpace){
			vitessePlayer.text = "Votre vitesse est très basse : \n Temps restant avt GameOver : "+(int)GameController.timeLeftToGameOver+" s";
			panelVitesse.SetActive (true);

		}
		else {
			panelVitesse.transform.position = camera.WorldToScreenPoint(PlayerController.actualPosition);
			panelVitesse.SetActive (false);			
		}*/

		/*** gestion du message du panel de fin de partie *****/
		if (!GameController.isInGame) {
			if (GameController.isGameOver) {
				endText.text = "Vous avez lamentablement perdu...houuuuuu !";
				/** si on a assez de diamants on peut faire effet Phoenix ***/
				if (GameController.nbreDiamond >= GameController.getDiamondForPhoenix ()) {
					PhoenixButton.SetActive (true);
					PhoenixButton.GetComponent<Button>().interactable = true;
				}
				else{
					PhoenixButton.SetActive (true);
					PhoenixButton.GetComponent<Button>().interactable = false;
				}

			} 
			else {
				endText.text = " Bravo vous etes un dieu de ce jeu ...trop fort !";
				NextLevelButton.SetActive (true);
			}
			endLevelPanel.SetActive (true);
		} 
		else {
			endLevelPanel.SetActive (false);
			PhoenixButton.SetActive (false);
		}
	}


	public void replayLevel(){
		//Application.LoadLevel (0);
        GameController.Save();
		Application.LoadLevel ("firstLevel");
	}

	public void changeLevel(string levelName){
		Application.LoadLevel ("FIRELEVEL2048");
	
	}

	public void pauseGame(){
		GameController.tooglePauseGame();
	}

}
