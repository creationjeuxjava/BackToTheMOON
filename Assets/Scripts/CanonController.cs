using UnityEngine;
using System.Collections;

public class CanonController : MonoBehaviour {

	public GameObject canonFire;
	public GameObject gameController;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		/**** version android et iphone ***/
		int fingerCount = 0;
		foreach (Touch touch in Input.touches) {
			if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
				fingerCount++;
			
		}
		if (fingerCount > 0)
			print("User has " + fingerCount + " finger(s) touching the screen");
		
	}
	
	/*** au clic sur le canon on fait "décoller" le pplayer *****/
	void OnMouseDown()//fonctionne aussi sur android !!
	{
		gameController.GetComponent<GameController>().LaunchPlayer();
		GameObject particules = Instantiate(canonFire, new Vector3(transform.position.x,transform.position.y+5f, -20f), transform.rotation) as GameObject; 
		particules.transform.parent = GameController.world.transform;
	}
}
