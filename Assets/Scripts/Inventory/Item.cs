using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public string itemName;
	int itemId;
	Sprite itemIcon;
	ItemType itemType;

	public enum ItemType{
		Consumable,
		Permanent

	}
	public Item(string name,int id,ItemType type){

		itemId = id;
		//itemIcon = icon;//Resources.Load<Sprite>(""+name);
		itemType = type;
		itemName = name;
	}
}
