using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BaggedItem : MonoBehaviour, IPointerClickHandler {

	public Item item;

	public void OnPointerClick (PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left) {
			InventoryManager.Instance.ShowItemDescription (this.item);
		} else if (eventData.button == PointerEventData.InputButton.Right) {
			InventoryEventHandler.ItemEquipped (this.item);
		}
	}
}
