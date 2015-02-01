using UnityEngine;
using System.Collections;

public class ControlOutOffScreen : MonoBehaviour {

	Vector3 pos;	
	Camera camera;
	Vector3 screenPos;
	TranslationMovement translationScript;

	void Awake(){
		camera = GameObject.FindWithTag ("Camera").camera;
		translationScript = GetComponent<TranslationMovement> ();
	
	}
	// Update is called once per frame
	void Update () {
		pos = transform.position;
		screenPos = camera.WorldToScreenPoint(pos);

		if(screenPos.x >= Screen.width ) {
			//transform.position = new Vector3(transform.position.x - 1.5f,transform.position.y,0);
			translationScript.velocity.x = (-1) * translationScript.velocity.x;
		}
		else if(screenPos.x <= 0){
			//transform.position = new Vector3(transform.position.x + 1.5f,transform.position.y,0);
			translationScript.velocity.x = (-1) * translationScript.velocity.x;
		}

	}
}
