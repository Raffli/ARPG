using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {

	public string name { get; set; }
	public Sprite sprite { get; set; }
	public int itemSlot { get; set; }
	public string itemPosition { get; set; }
	//public string itemName { get; set; }
	//public List <Stat> itemStats { get; set; }

	public Item (string name, Sprite sprite, string itemPosition) {
		this.name = name;
		this.sprite = sprite;
		this.itemPosition = itemPosition;
	}
}
