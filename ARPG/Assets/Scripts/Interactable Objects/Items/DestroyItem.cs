using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DestroyItem : MonoBehaviour, IPointerClickHandler {

	public Item item { get; set; }

	public void OnPointerClick (PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left) {
			if (item != null) {
				InventoryEventHandler.ItemDestroyed (this.item);
			}
		} 
	}

}
