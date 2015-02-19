using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VitesseBar : MonoBehaviour {

	public float maxVitesse;
	public float maxHeight = 200;
	public Color alert,middle,ok;

	public Color contentColor; //pour debug !!

	Vector3 offSetMaxValue;
	Image vitesseContent;
	RectTransform vitesseContentTransform;

	// Use this for initialization
	void Start () {
		vitesseContent = GameObject.Find ("vitesseContent").GetComponent<Image> ();
		vitesseContentTransform = GameObject.Find ("vitesseContent").GetComponent<RectTransform> ();

		//offSetMaxValue = new Vector3(2.8f,200,0);
		offSetMaxValue = new Vector3(0,0,0);
		vitesseContentTransform.offsetMax = Vector3.zero;

		//vitesseContentTransform.rect.Set (0,0,5,10);

		contentColor = vitesseContent.color;
	}
	
	// Update is called once per frame
	void Update () {
		convertVitesseForIHM();
		vitesseContentTransform.offsetMax = offSetMaxValue;

		Debug.Log ("*********************"+this + "vitesse player " + GameController.playerSpeed.y);
		Debug.Log ("--> valeur offsetMAx " + offSetMaxValue.y);
		Debug.Log ("--> valeur transform " + vitesseContentTransform.offsetMax.y);

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
		//offSetMaxValue.y = -1 * GameController.playerSpeed.y * maxHeight / maxVitesse;

		offSetMaxValue.y =  GameController.playerSpeed.y * maxHeight / maxVitesse;


	}
}
