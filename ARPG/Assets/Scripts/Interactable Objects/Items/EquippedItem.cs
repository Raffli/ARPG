using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquippedItem : MonoBehaviour, IPointerClickHandler {

	public Item item;
	
	public void OnPointerClick (PointerEventData eventData)
	{
		Debug.Log ("on pointer click");
	}

}
