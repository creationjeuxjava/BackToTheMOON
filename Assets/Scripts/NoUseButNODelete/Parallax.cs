using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {
    int y = 0;
    public GameObject[] objToParallax;
    public bool playerMoving = false;
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //Time.timeScale =  0.2f;
       if(Input.GetKey(KeyCode.Space))
            
       transform.Translate(0, y-0.03f, 0);
	}
}
