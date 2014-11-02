using UnityEngine;
using System.Collections;

public class CanonController : MonoBehaviour {

	//public GameObject gameController;
	private bool isMoving = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown()
	{
		//Debug.Log ("***************  souris clic  sur : "+gameObject.name+" on va tirer");
		GameController.LaunchPlayer();
	}
}
