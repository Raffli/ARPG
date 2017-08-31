using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Loot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

	public Item item;

	public void OnPointerExit (PointerEventData eventData)
	{
		HUDManager.Instance.SetLootCursor (false);
	}

	public void OnPointerEnter (PointerEventData eventData)
	{
		HUDManager.Instance.SetLootCursor (true);
	}

	public void OnPointerClick (PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left && !InventoryManager.Instance.bagFull) {
			if (item != null) {
				HUDManager.Instance.SetLootCursor (false);
				InventoryEventHandler.ItemBagged (item);
				Destroy (this.transform.parent.gameObject);
			}
		}
	}
		
}
