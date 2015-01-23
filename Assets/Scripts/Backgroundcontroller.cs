using UnityEngine;
using System.Collections;

public class Backgroundcontroller : MonoBehaviour {
	
	Color color = new Color(1,1,1,1);
	Material planMat;

	void Start(){

		planMat = GetComponent<MeshRenderer> ().materials [0];

	}

	void Update () {

		float bleu = 1 - GameController.altitude / 50000;
		float vert = 0.6f - GameController.altitude / 40000;
		color.r = 0;
		color.g = vert;
		color.b = bleu;
		color.a = 1;
		//Color color = new Color(0,vert,bleu,1);
		planMat.color = color;
	}
}
