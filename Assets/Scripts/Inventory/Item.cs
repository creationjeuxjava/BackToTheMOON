using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public string itemName;
	int itemId;
	public Sprite itemIcon;
	ItemType itemType;

	public enum ItemType{
		Consumable,
		Permanent,
		Timer

	}
	public Item(string name,int id,ItemType type){

		itemId = id;
		if(!name.Equals("flageollets")) itemIcon = Resources.Load<Sprite>("GUI/bouton"+name);
		itemType = type;
		itemName = name;
	}
}
