using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {
    int x = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(x++, 0, 0);
	}
}
