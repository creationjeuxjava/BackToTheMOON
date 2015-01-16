using UnityEngine;
using System.Collections;

public class CanonController : MonoBehaviour {

	public GameObject canonFire;
	public GameObject gameController;
	public Animator anim,animMoitie;

	// Use this for initialization
	void Start () {
		anim = GameObject.Find ("canondevant").GetComponent<Animator> ();
		animMoitie = GameObject.Find ("canonfond").GetComponent<Animator> ();
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
		Debug.Log ("on touche le canon");
		anim.SetTrigger ("fire");//audio.Play();
		animMoitie.SetTrigger ("fire");

	}

	/*** permet de savoir si la phase de chauffe du canon est terminée en animation ...****/
	/*** fonction appelée par l'animator ****/
	//[RequireComponent(typeof(AudioSource))]
	void launchIt(){
		if(!audio.isPlaying) 
			audio.Play();
			//audio.PlayOneShot ("canon2", 0.7F);
		gameController.GetComponent<GameController>().LaunchPlayer();
		GameObject particules = Instantiate(canonFire, new Vector3(transform.position.x,transform.position.y+5f, -20f), transform.rotation) as GameObject; 
		//particules.transform.parent = GameController.world.transform;


		Debug.Log ("on launchIt");

	}
}
