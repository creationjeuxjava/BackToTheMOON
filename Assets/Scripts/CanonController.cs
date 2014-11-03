using UnityEngine;
using System.Collections;

public class CanonController : MonoBehaviour {

	public GameObject canonFire;
	public GameObject gameController;
	private bool isMoving = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown()
	{
		//Debug.Log ("***************  souris clic  sur : "+gameObject.name+" on va tirer");
		gameController.GetComponent<GameController>().LaunchPlayer();
		GameObject particules = Instantiate(canonFire, new Vector3(transform.position.x,transform.position.y+5f, -20f), transform.rotation) as GameObject; 
		particules.transform.parent = GameController.world.transform;
	}
}
