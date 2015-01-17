using UnityEngine;
using System.Collections;

public class Backgroundcontroller : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		/*if (GameController.altitude < 100) {
			GetComponent<MeshRenderer>().materials[0].color = Color.blue;



		}
		else if(GameController.altitude > 100){
			GetComponent<MeshRenderer>().materials[0].color = Color.red;

		}*/
		float bleu = 1 - GameController.altitude / 3000;
		float vert = 1 - GameController.altitude / 3000;
		Color color = new Color(0,vert,bleu,1);
		GetComponent<MeshRenderer>().materials[0].color = color;
	}
}
