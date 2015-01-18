using UnityEngine;
using System.Collections;

public class PlayButtonManager : MonoBehaviour {

	// Use this for initialization
	public void StartGame () {
		Application.LoadLevel ("firstLevel");
	}
}
