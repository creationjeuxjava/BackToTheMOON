using UnityEngine;
using System.Collections;

public class LauncherController : MonoBehaviour {

	public GameObject gameController;
	private Animator anim;

	// Use this for initialization
	void Start () {
		//anim = GameObject.Find ("Launcher").GetComponent<Animator> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/*** au clic sur le canon on fait "décoller" le pplayer *****/
	void OnMouseDown()//fonctionne aussi sur android !!
	{
		Debug.Log ("on touche le launcher");
		anim.SetTrigger ("fire");
		
	}

	/*** permet de savoir si la phase de chauffe du canon est terminée en animation ...****/
	/*** fonction appelée par l'animator ****/
	//[RequireComponent(typeof(AudioSource))]
	void launchIt(){
		/*if(!audio.isPlaying) 
			audio.Play();*/
		gameController.GetComponent<GameController>().LaunchPlayer();
		//GameObject particules = Instantiate(canonFire, new Vector3(transform.position.x,transform.position.y+5f, -20f), transform.rotation) as GameObject; 
		//particules.transform.parent = GameController.world.transform;
		
		
		Debug.Log ("on launchIt");
		
	}
}
