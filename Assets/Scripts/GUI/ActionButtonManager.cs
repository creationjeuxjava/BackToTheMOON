using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActionButtonManager : MonoBehaviour {

	/*** pour le bouton action ****/
	public static GameObject buttonObject;
	static Image  iconeBoutonAction;
	//PlayerController controller;

	// Use this for initialization
	void Start () {
		/*** pour le bouton action ****/
		iconeBoutonAction = GameObject.Find ("actionButton").GetComponent<Image> ();
		//iconeBoutonAction = buttonObject.GetComponent<Image> ();
		iconeBoutonAction.sprite = (Sprite)Resources.Load ("Sprites/ui/boutonaction",typeof(Sprite));
		//iconeBoutonAction.sprite = (Sprite)Resources.Load ("None",typeof(Sprite));

		//controller = this.GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		/*if (this.GetComponent<PlayerController>().currentState == PlayerController.State.noAction) {
			//buttonObject.SetActive (false);
			iconeBoutonAction.sprite = (Sprite)Resources.Load ("Sprites/ui/boutonaction",typeof(Sprite));
		} 
		else if (this.GetComponent<PlayerController> ().currentState == PlayerController.State.naked) {


			buttonObject.SetActive (true);
			iconeBoutonAction.sprite = (Sprite)Resources.Load ("Sprites/ui/helico", typeof(Sprite));
		} 
		else if (this.GetComponent<PlayerController> ().currentState == PlayerController.State.laser) {
			buttonObject.SetActive (true);
			iconeBoutonAction.sprite = (Sprite)Resources.Load ("Sprites/ui/sabrelaser", typeof(Sprite));
		} 
		else {
			Debug.Log("pas de state adapté !!!");
		}*/

		/*if (controller.currentState == PlayerController.State.noAction) {
			//buttonObject.SetActive (false);
			iconeBoutonAction.sprite = (Sprite)Resources.Load ("Sprites/ui/boutonaction",typeof(Sprite));
		} 
		else if (controller.currentState == PlayerController.State.naked) {
			
			
			buttonObject.SetActive (true);
			iconeBoutonAction.sprite = (Sprite)Resources.Load ("Sprites/ui/helico", typeof(Sprite));
		} 
		else if (controller.currentState == PlayerController.State.laser) {
			buttonObject.SetActive (true);
			iconeBoutonAction.sprite = (Sprite)Resources.Load ("Sprites/ui/sabrelaser", typeof(Sprite));
		} 
		else {
			Debug.Log("pas de state adapté !!!");
		}*/
	}

	public static void updateIcon(PlayerController.State currentstate){
		Debug.Log ("ActionButton : on update l'icone !!");

		PlayerController.State state = currentstate; 
		if (state == PlayerController.State.noAction) {
			//buttonObject.SetActive (false);
			iconeBoutonAction.sprite = (Sprite)Resources.Load ("Sprites/ui/boutonaction",typeof(Sprite));
		} 
		else if (state == PlayerController.State.naked) {			
		//	buttonObject.SetActive (true);
			iconeBoutonAction.sprite = (Sprite)Resources.Load ("Sprites/ui/helico", typeof(Sprite));
		} 
		else if (state == PlayerController.State.laser) {
		//	buttonObject.SetActive (true);
			iconeBoutonAction.sprite = (Sprite)Resources.Load ("Sprites/ui/sabrelaser", typeof(Sprite));
		} 
		else {
			Debug.Log("pas de state adapté !!!");
		}
	}


}
