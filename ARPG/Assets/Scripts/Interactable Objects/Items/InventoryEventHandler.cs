using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryEventHandler : MonoBehaviour {

	public delegate void EquipItemHandler (Item item);
	public static event EquipItemHandler OnItemEquipped;

	public static void ItemEquipped (Item item) {
		Debug.Log ("call item equipped im event handler");
		OnItemEquipped (item);
	}
		
}
