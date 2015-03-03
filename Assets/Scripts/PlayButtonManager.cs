using UnityEngine;
using System.Collections;

public class PlayButtonManager : MonoBehaviour {

	private string selectedLevel;
	private GameObject barSelection;
	
	// Use this for initialization
	public void Start () {
		barSelection = GameObject.Find("/Canvas/HandBackground/LevelScreen/BarSelection");
		barSelection.SetActive(false);
	}

	public void LoadLevel(string refLevel) {
		if (refLevel == selectedLevel) {
			LoadSelectedLevel();
		} else {
			SelectLevel(refLevel);
		}
	}

	public void LoadSelectedLevel() {
		if(selectedLevel != null)
			Application.LoadLevel (selectedLevel);
	}

	public void SelectLevel(string levelName) {
		selectedLevel = levelName;
		barSelection.SetActive (true);
	}

	public void UnsetSelectedLevel() {
		selectedLevel = null;
		barSelection.SetActive (false);
	}
}
