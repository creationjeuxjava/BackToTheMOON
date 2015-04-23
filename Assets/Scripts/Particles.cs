using UnityEngine;
using System.Collections;

public class Particles : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = "particles";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
