using UnityEngine;
using System.Collections;

public class ControlOutOffScreen : MonoBehaviour {

	//public Camera camera;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;

		Camera camera = GameObject.FindWithTag ("Camera").camera;
		Vector3 screenPos = camera.WorldToScreenPoint(pos);

		if(screenPos.x >= Screen.width ) {
			transform.position = new Vector3(transform.position.x - 1.5f,transform.position.y,0);
			GetComponent<TranslationMovement>().velocity.x = (-1) * GetComponent<TranslationMovement>().velocity.x;
		}
		else if(screenPos.x <= 0){
			transform.position = new Vector3(transform.position.x + 1.5f,transform.position.y,0);
			GetComponent<TranslationMovement>().velocity.x = (-1) * GetComponent<TranslationMovement>().velocity.x;
		}
	}
}
