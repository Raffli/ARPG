using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaggedItem : MonoBehaviour, IPointerClickHandler {

	public Item item;

	public void OnPointerClick (PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left) {
			
			Debug.Log ("left button clicked");
		} else if (eventData.button == PointerEventData.InputButton.Right) {
			Debug.Log ("this item is " + this.item.name);
			InventoryEventHandler.ItemEquipped (this.item);
		}
	}
}
