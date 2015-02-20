using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActionButtonManager : MonoBehaviour {

	/*** pour le bouton action ****/
	public static GameObject buttonObject;
	static Image  iconeBoutonAction;
	private static Animator anim;
	//PlayerController controller;

	// Use this for initialization
	void Start () {
		/*** pour le bouton action ****/
		//iconeBoutonAction = GameObject.Find ("actionButton").GetComponent<Image> ();
		//iconeBoutonAction = buttonObject.GetComponent<Image> ();
		//iconeBoutonAction.sprite = (Sprite)Resources.Load ("Sprites/ui/boutonaction",typeof(Sprite));
		//iconeBoutonAction.sprite = (Sprite)Resources.Load ("None",typeof(Sprite));

		//controller = this.GetComponent<PlayerController> ();
		anim = GameObject.Find ("actionButton").GetComponent<Animator> ();
	}
	


	public static void updateIcon(PlayerController.State currentstate){
		//Debug.Log ("ActionButton : vitesse joueur "+GameController.lastPlayerSpeed.y);
		//Debug.Log ("ActionButton : etat du joueur :"+currentstate);

		PlayerController.State state = currentstate; 
		if (state == PlayerController.State.noAction) {
			anim.SetTrigger("close");
		} 
		//else if (state == PlayerController.State.naked && GameController.lastPlayerSpeed.y >= -0.05f) {
		else if (state == PlayerController.State.naked && PlayerController.vitesse.y < -0.08f &&  !GameController.isInSpace) {
			anim.SetTrigger("openFly");
		} 
		else if (state == PlayerController.State.naked &&  PlayerController.vitesse.y >= -0.08f &&  !GameController.isInSpace) {
			anim.SetTrigger("enableFlyAction");
		} 
		else if (state == PlayerController.State.naked &&  GameController.isInSpace) {
			anim.SetTrigger("close");
		}
		else if (state == PlayerController.State.laser) {
			//	buttonObject.SetActive (true);
			//iconeBoutonAction.sprite = (Sprite)Resources.Load ("Sprites/ui/sabrelaser", typeof(Sprite));
		} 
		else {
			anim.SetTrigger("close");
		}
	}


}
