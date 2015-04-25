using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataRecorder : MonoBehaviour {

	private static PlayerData playerData;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public  static void  Save(PlayerData datas){
		
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.bttm");

		//playerData.hasSavedGame = hasSaved;
		bf.Serialize (file, datas);
		Debug.Log ("Saving game into " + Application.persistentDataPath);
		file.Close();
		GameController.hasSaved = true;
	}
	
	
	
	public static void Load(){
		if(File.Exists(Application.persistentDataPath + "/playerInfo.bttm"));
		{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.bttm", FileMode.Open);
			playerData = (PlayerData) bf.Deserialize(file);

			//hasSaved = playerData.hasSavedGame;
			file.Close ();
			Debug.Log ("Loading game from " + Application.persistentDataPath);
			
		}
	}

	public static PlayerData GetPlayerData() {
		return playerData;
	}
}
