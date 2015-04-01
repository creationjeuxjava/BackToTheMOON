using UnityEngine;
using System.Collections;

public class PlayButtonManager : MonoBehaviour {

	private int selectedLevel;
	private GameObject barSelection;
	private GameObject levelScreen;
	
	// Use this for initialization
	public void Start () {

		barSelection = GameObject.Find("/Canvas/HandBackground/LevelScreen/BarSelection");
		barSelection.SetActive(false);
		levelScreen = GameObject.Find ("/Canvas/HandBackground/LevelScreen");
		levelScreen.SetActive(false);
	}

	public void LoadLevel(int refLevel) {
		LoadingScreenController.setNumLevel (refLevel);
		if (refLevel == selectedLevel) {
			LoadSelectedLevel();
		} else {
			SelectLevel(refLevel);
		}
	}

	public void LoadSelectedLevel() {
		if(selectedLevel != -1)
			Application.LoadLevel ("LoadingScreen");
	}

	public void SelectLevel(int levelName) {
		selectedLevel = levelName;
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
