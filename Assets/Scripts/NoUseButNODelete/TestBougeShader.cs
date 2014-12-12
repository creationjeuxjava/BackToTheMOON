using UnityEngine;
using System.Collections;

public class TestBougeShader : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
            transform.position = new Vector3(0, Mathf.Lerp(-5, 5, Time.time / 10), 0);

	}
}
