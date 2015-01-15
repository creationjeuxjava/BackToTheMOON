using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DebugInGame : MonoBehaviour {

	private int startY;
	Text isFly,isCask,isShoe,translation,vitesse;

	void Start () {
		startY = 80;
		isFly = GameObject.Find ("isFlyBegin").GetComponent<Text> ();
		isCask = GameObject.Find ("isWithCask").GetComponent<Text> ();
		isShoe = GameObject.Find ("isWithShoe").GetComponent<Text> ();
		translation = GameObject.Find ("translation").GetComponent<Text> ();
		vitesse = GameObject.Find ("vitesse").GetComponent<Text> ();
	}

	void Update(){
		isFly.text = "isBeginFly : "+PlayerController.isFlyBegin;
		isFly.text = "isWithCask : "+PlayerController.isWithCask;
		isFly.text = "isWithShoe : "+PlayerController.isWithShoe;
		translation.text = "translation :" + PlayerController.translation;
		vitesse.text = "vitesse : " + PlayerController.vitesse;

	}

	private int getNextY(int indice){
		int nextY = startY + 20 * indice;
		return nextY;
	}
}
