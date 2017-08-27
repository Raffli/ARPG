using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquippedItem : MonoBehaviour, IPointerClickHandler {

	public Item item;
	
	public void OnPointerClick (PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left) {
			InventoryManager.Instance.ShowItemDescription (this.item);
		} else if (eventData.button == PointerEventData.InputButton.Right) {
			InventoryEventHandler.ItemUnequipped (this.item);
		}
	}

}
