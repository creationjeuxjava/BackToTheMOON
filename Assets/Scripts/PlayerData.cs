using UnityEngine;
using System.Collections;

[System.Serializable]

public class PlayerData {

	public int coins;
	public int diamonds;
	public int maxAltitude;
	public bool hasSavedGame;

	public bool[] doneLevels = {false,false,false,false,false};

	public static int LEVEL_EARTH = 1, LEVEL_MARS = 2, LEVEL_DABOGA = 3, LEVEL_CITY = 4, LEVEL_ROBOT = 5;
}
