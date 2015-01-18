using UnityEngine;
using System.Collections;
using SmartLocalization;

public class Internalization : MonoBehaviour {

	string testLoc;
	// Use this for initialization
	void Start () {
		LanguageManager languageManager = LanguageManager.Instance;
		languageManager.OnChangeLanguage += OnChangeLanguage;
		languageManager.ChangeLanguage ("en");

	}


	void OnDestroy(){
		if (LanguageManager.HasInstance)
						LanguageManager.Instance.OnChangeLanguage -= OnChangeLanguage;
	}

	void OnChangeLanguage(LanguageManager thisLanguageManager)
	{

		testLoc = thisLanguageManager.GetTextValue ("MyFirst.Key");
	}

	// Update is called once per frame
	void Update () {
		Debug.Log ("azeazeazeaze");
	}


	public void ChangeLangue(){
		LanguageManager.Instance.ChangeLanguage("en");
		Debug.Log (testLoc);
	}

	void OnGui(){
		Debug.Log (testLoc);
		if (GUI.Button (new Rect (100, 100, 100, 100), "english"))
		    LanguageManager.Instance.ChangeLanguage("en");
		if(GUILayout.Button("ENGLISH"))
			LanguageManager.Instance.ChangeLanguage("en");
}
}