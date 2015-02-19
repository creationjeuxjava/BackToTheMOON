using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class InGGameGUIController : MonoBehaviour {

	public Camera camera;

	Text nbrPiecesText,nbrDiamantsText,altitudeText;
	Image espaceIcone;
	Animator iconeEspaceAnimator;

	GameObject panelVitesse;
	Text vitessePlayer;

	GameObject endLevelPanel;
	Text endText;
	GameObject NextLevelButton;


	void Start () {
		nbrPiecesText = GameObject.Find ("nbrePiecesText").GetComponent<Text> ();
		nbrDiamantsText = GameObject.Find ("nbreDiamantsText").GetComponent<Text> ();
		altitudeText = GameObject.Find ("altitudeText").GetComponent<Text> ();
		espaceIcone = GameObject.Find ("espaceIcone").GetComponent<Image> ();
		iconeEspaceAnimator = GameObject.Find ("espaceIcone").GetComponent<Animator> ();
		
	
		panelVitesse = GameObject.Find ("PanelInfosVitesse");
		vitessePlayer = GameObject.Find ("infoVitesse").GetComponent<Text> ();
		panelVitesse.SetActive (false);

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


		if (GameController.isInSpace) {
			iconeEspaceAnimator.SetTrigger("beginSpace");
		} 

		if (PlayerController.vitesse.y >= -0.05f && PlayerController.isFlying) {
			vitessePlayer.text = "Votre vitesse est très basse : battez des ailes... !!\n Temps restant avt GameOver : "+(int)GameController.timeLeftToGameOver;
			panelVitesse.SetActive (true);
		}
		else {
			panelVitesse.transform.position = camera.WorldToScreenPoint(PlayerController.actualPosition);
			panelVitesse.SetActive (false);			
		}

		if (!GameController.isInGame) {
			if(GameController.isGameOver) endText.text = "Vous avez lamentablement perdu...houuuuuu !";
			else {
				endText.text = " Bravo vous etes un dieu de ce jeu ...trop fort !";
				NextLevelButton.SetActive (true);
			}
			endLevelPanel.SetActive (true);
		}
	}

	public void replayLevel(){
		//Application.LoadLevel (0);
		Application.LoadLevel ("firstLevel");
	}

	public void changeLevel(string levelName){
		Application.LoadLevel ("FIRELEVEL2048");
	
	}

	public void pauseGame(){
		GameController.tooglePauseGame();
	}

}
