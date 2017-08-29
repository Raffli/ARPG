using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {

	public enum ItemState { Normal, Magic, Epic, Legendary }
	public enum ItemPosition { Head, Amulet, Chest, Gloves, Primary, Secondary, Ring1, Ring2, Pants, Shoes }

	public string name { get; set; }
	public Sprite sprite { get; set; }
	public ItemPosition itemPosition { get; set; }

	public int itemSlot { get; set; }
	public ItemState itemState { get; set; }
	public string itemDescription { get; set; }
	public List <StatBonus> itemStats { get; set; }
	public bool itemEquipped { get; set; }
	public bool toDestroy { get; set; }

	public Item (string name, Sprite sprite, ItemPosition itemPosition) {
		itemStats = new List<StatBonus> ();
		this.name = name;
		this.sprite = sprite;
		this.itemPosition = itemPosition;
	}
}
