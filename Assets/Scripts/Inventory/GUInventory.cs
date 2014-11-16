using UnityEngine;
using System.Collections;
using System.Collections;
using System.Collections.Generic;

public class GUInventory : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/**** Eléments GUI du jeu ****/
	void OnGUI () {

		GUI.skin.GetStyle ("Label").fontSize = 12;
		GUI.Label(new Rect(Screen.width - 80,Screen.height - 100 ,100,30),"Inventaire");

		/*** le contenu de l'inventaire ***/
		List<Item> items = player.GetComponent<Inventory> ().getInventory ();
		Debug.Log (" contenu size : " + items.Count);
		string message = "";
		string message2 = "";
		int nbre = 0;
		foreach(Item item in items){
			if(item.itemName.Equals("flageollets")){

				nbre++;
				message = item.itemName+" x "+nbre;
			}
			else{

				message2 = item.itemName;
			}


		}
		GUI.Label(new Rect(Screen.width - 80,Screen.height - 50 ,100,30),message);
		GUI.Label(new Rect(Screen.width - 80,Screen.height - 30 ,100,30),message2);

	
	}
}
