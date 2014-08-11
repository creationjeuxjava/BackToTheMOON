using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    bool isMoving;
	// Use this for initialization
	void Start () {
		Debug.Log("PlayerController-->création");
	}
	
	// Update is called once per frame
	void Update () {
				if (Input.touchCount == 1) {
						Vector3 touchPosition = Input.GetTouch (0).position;
						Debug.Log ("PlayerController: on touche écran en  (" + touchPosition.x + " , " + touchPosition.y + ")");
				}
		}
}
