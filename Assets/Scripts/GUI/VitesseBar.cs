using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VitesseBar : MonoBehaviour {

	public float maxVitesse;
	public Color alert,middle,ok;

	public Color contentColor; //pour debug !!

	float maxHeight;
	Vector2 currentBarRectSize;
	Image vitesseContent;
	RectTransform vitesseContentTransform,parentBar;

	// Use this for initialization
	void Start () {
		vitesseContent = GameObject.Find ("vitesseContent").GetComponent<Image> ();
		vitesseContentTransform = GameObject.Find ("vitesseContent").GetComponent<RectTransform> ();
		parentBar = GameObject.Find ("barreVitesse").GetComponent<RectTransform> ();

		currentBarRectSize = new Vector2(0.05f,0f);
		vitesseContentTransform.sizeDelta = currentBarRectSize;
		maxHeight = parentBar.rect.height;

		contentColor = vitesseContent.color;//pour debug

	}
	
	// Update is called once per frame
	void Update () {
		convertVitesseForIHM();
		vitesseContentTransform.sizeDelta = currentBarRectSize;

		//Debug.Log ("*********************"+this + "vitesse player " + GameController.playerSpeed.y);
		//Debug.Log ("--> valeur transform " + vitesseContentTransform.offsetMax.y);

		if(GameController.playerSpeed.y > -0.1f){
			vitesseContent.color = alert;
		}
		else if(GameController.playerSpeed.y <= -0.1f && GameController.playerSpeed.y > -0.2f){
			vitesseContent.color = middle;
		}
		else{
			vitesseContent.color = ok;
		}
		contentColor = vitesseContent.color;
	}

	private void convertVitesseForIHM(){
		currentBarRectSize.y = GameController.playerSpeed.y * maxHeight / maxVitesse;

	}
}
