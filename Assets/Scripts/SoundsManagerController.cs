using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundsManagerController : MonoBehaviour {

	public AudioClip phoenix,alertGameOver;

	private Dictionary<string,AudioClip> dicoSounds = new Dictionary<string,AudioClip>();
	// Use this for initialization
	void Start () {
		dicoSounds.Add ("phoenix",phoenix);
		dicoSounds.Add ("alertGameOver",alertGameOver);
	}
	

	public void playSound(string name,float volume){
		if (dicoSounds.ContainsKey (name)) {
			AudioClip clip = dicoSounds [name];
			audio.PlayOneShot (clip, volume);
		} 
		else {
				
			Debug.Log("le clip "+name+" n'existe pas dans le dico !!");
		}

	}
}
