using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

	private List<Item> items = new List<Item>();

	// Use this for initialization
	void Start () {
		items.Add (new Item("flageollets",0,Item.ItemType.Consumable));
		items.Add (new Item("flageollets",0,Item.ItemType.Consumable));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public List<Item> getInventory(){
		return items;
	}

	public void addItem(Item item){
		items.Add (item);
	}
}
