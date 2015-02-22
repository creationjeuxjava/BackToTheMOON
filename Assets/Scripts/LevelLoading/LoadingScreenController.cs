using UnityEngine;
using System.Collections;

public class LoadingScreenController : MonoBehaviour {

	AsyncOperation async;
	public float waitTime = 2f;

	// Use this for initialization
	void Start () {
		Debug.Log("lancement de la coroutine");
		//async = 
		StartCoroutine(waitSeconds (waitTime));
	}
	
	// Update is called once per frame
	void Update () {
		/*if (!Application.isLoadingLevel) {
				
			Debug.Log("level en cours de chargement !!");
		}*/

		/*if (async.isDone) {
				
			Debug.Log("level chargé mon pote !! !!");
		}*/
	}

	IEnumerator waitSeconds (float waitTime)
	{
		yield return new WaitForSeconds (waitTime);
		StartCoroutine(LoadLevel (0));
	}

	IEnumerator LoadLevel(int numLevel) {
		//AsyncOperation async = Application.LoadLevelAsync("firstLevel");
		//async = Application.LoadLevelAsync(numLevel);
		async = Application.LoadLevelAsync("firstLevel");
		yield return async;
		Debug.Log("Loading complete");
	}
}
