using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryEventHandler : MonoBehaviour {

	public delegate void EquipItemHandler (Item item);
	public static event EquipItemHandler OnItemEquipped;

	public static void ItemEquipped (Item item) {
		OnItemEquipped (item);
	}

	public delegate void UnequipItemHandler (Item item);
	public static event UnequipItemHandler OnItemUnequipped;

	public static void ItemUnequipped (Item item) {
		OnItemUnequipped (item);
	}

	public delegate void BagItemHandler (Item item);
	public static event BagItemHandler OnItemBagged;

	public static void ItemBagged (Item item) {
		OnItemBagged (item);
	}

	public delegate void UnbagItemHandler (Item item);
	public static event UnbagItemHandler OnItemUnbagged;

	public static void ItemUnbagged (Item item) {
		OnItemUnbagged (item);
	}

	public delegate void DestroyItemHandler (Item item);
	public static event DestroyItemHandler OnItemDestroyed;

	public static void ItemDestroyed (Item item) {
		OnItemDestroyed (item);
	}
		
}
