using UnityEngine;
using System.Collections;

public class Backgroundcontroller : MonoBehaviour {


	void Update () {

		float bleu = 1 - GameController.altitude / 3000;
		float vert = 0.6f - GameController.altitude / 3000;
		Color color = new Color(0,vert,bleu,1);
		GetComponent<MeshRenderer>().materials[0].color = color;
	}
}
