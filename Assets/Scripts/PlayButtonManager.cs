using UnityEngine;
using System.Collections;

public class PlayButtonManager : MonoBehaviour {

	private string selectedLevel;

	// Use this for initialization
	public void StartGame () {

	}

	public void loadLevel(string refLevel) {
		if (refLevel == selectedLevel) {
			loadSelectedLevel();
		} else {
			selectedLevel = refLevel;
		}
	}

	public void loadSelectedLevel() {
		if(selectedLevel != null)
			Application.LoadLevel (selectedLevel);
	}

	public void unsetSelectedLevel() {
		selectedLevel = null;
	}
}
