using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBasis {

	public string itemName;
	public Sprite itemSprite;
	public string itemDescription;
	public int multiplier;

	public ItemBasis (string itemName, Sprite itemSprite, string itemDescription, int multiplier) {
		this.itemName = itemName;
		this.itemSprite = itemSprite;
		this.itemDescription = itemDescription;
		this.multiplier = multiplier;
	}
}
