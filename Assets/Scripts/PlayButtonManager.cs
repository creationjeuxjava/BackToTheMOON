using UnityEngine;
using System.Collections;

public class PlayButtonManager : MonoBehaviour {

	public Animator zoomAnimator;
	// Use this for initialization
	public void StartGame () {
		zoomAnimator = GameObject.Find ("Canvas/HandBackground").GetComponent<Animator>();
		zoomAnimator.SetBool ("zoom", false);
		Invoke ("loadFirstLevel",1);
	}

	public void loadFirstLevel() {
		Application.LoadLevel ("firstLevel");
	}

	public void loadSecondLevel() {
		Application.LoadLevel ("secondLevel");
	}
}
