using UnityEngine;
using System.Collections;

public class LoadingScreenController : MonoBehaviour {

	AsyncOperation async;
	public float waitTime = 2f;
	public bool isPreloadingRequire = true;


	public static int numLevel = 1;
	public static LoadingScreenController control;// c'est la clé pour passer des infos aux autres scènes !!


	void awake(){
		if (control == null) {
			DontDestroyOnLoad (gameObject);
			control = this;
		} 
		else if(control != this){
			
			Destroy(gameObject);//un seul possible sinon static ne fonctionnera pas !!
		}
		

	}
	// Use this for initialization
	void Start () {
		Debug.Log("lancement de la coroutine");
		if(isPreloadingRequire){
			StartCoroutine(waitSeconds (waitTime));

		}
	}
	
	// Update is called once per frame
	void Update () {
		//TODO : passage en aléatoire des infos : descriptif d'items ou autre du jeu (tutoriel en attente !!)
	}

	IEnumerator waitSeconds (float waitTime)
	{
		yield return new WaitForSeconds (waitTime);
		StartCoroutine(LoadLevel (numLevel));//TODO : à remplacer par la variable "numLevel"
	}

	IEnumerator LoadLevel(int numLevel) {
		//AsyncOperation async = Application.LoadLevelAsync("firstLevel");
		async = Application.LoadLevelAsync(numLevel);

		//async = Application.LoadLevelAsync("firstLevel");//TODO à changer  par le numéro du bon monde à charger
		yield return async;
		Debug.Log("Loading complete");
	}

	/*** appel static depuis le screen des Level, juste avant le chargement de la scène "loadingScreen" ****/
	public static void setNumLevel(int numeroLevel){

		numLevel = numeroLevel;
	}
}
