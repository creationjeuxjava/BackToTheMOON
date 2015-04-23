using UnityEngine;
using System.Collections;

public class PlayButtonManager : MonoBehaviour {

	private GameObject barSelection;
	private GameObject levelScreen = null;
	private int selectedLevel = -1;
	
	// Use this for initialization
	public void Start () {

		barSelection = GameObject.Find("/Canvas/HandBackground/LevelScreen/BarSelection");
		barSelection.SetActive(false);
		levelScreen = GameObject.Find ("/Canvas/HandBackground/LevelScreen");
		levelScreen.SetActive(false);
	}

	public void LoadLevel(int refLevel) {
		LoadingScreenController.setLevel (refLevel);
		if (refLevel == selectedLevel) {
			LoadSelectedLevel();
		} else {
			SelectLevel(refLevel);
		}
	}

	public void LoadSelectedLevel() {
		if(selectedLevel > 0) 
			Application.LoadLevel (1);
	}

	public void SelectLevel(int level) {
		selectedLevel = level;
		barSelection.SetActive (true);
	}

	public void UnsetSelectedLevel() {
		selectedLevel = -1;
		barSelection.SetActive (false);
	}

	public void FadeInLevelScreen() {
		levelScreen.SetActive (true);
	}
}
