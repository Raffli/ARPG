using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class InventoryManager : MonoBehaviour {

	public static InventoryManager Instance { get; set; }

	public GameObject inventoryPanel;
	private Image playerModel;
	private Transform equippedGroup;
	[HideInInspector] public Button head, amulet, chest, gloves, primary, secondary, ring1, ring2, pants, shoes;
	private GameObject bagGroup;
	public Button bagSlot;
	private List <Button> bag;

	void Awake () {
		DontDestroyOnLoad (transform.gameObject);

		if (Instance != null && Instance != this) {
			Destroy (gameObject);
		} else {
			Instance = this;
		}

		playerModel = inventoryPanel.transform.Find ("Model").GetComponent<Image> ();

		equippedGroup = inventoryPanel.transform.Find ("Equipped");
		head = equippedGroup.Find ("Head").Find ("Item").GetComponent<Button> ();
		amulet = equippedGroup.Find ("Amulet").Find ("Item").GetComponent<Button> ();
		chest = equippedGroup.Find ("Chest").Find ("Item").GetComponent<Button> ();
		gloves = equippedGroup.Find ("Gloves").Find ("Item").GetComponent<Button> ();
		primary = equippedGroup.Find ("Primary").Find ("Item").GetComponent<Button> ();
		secondary = equippedGroup.Find ("Secondary").Find ("Item").GetComponent<Button> ();
		ring1 = equippedGroup.Find ("Ring1").Find ("Item").GetComponent<Button> ();
		ring2 = equippedGroup.Find ("Ring2").Find ("Item").GetComponent<Button> ();
		pants = equippedGroup.Find ("Pants").Find ("Item").GetComponent<Button> ();
		shoes = equippedGroup.Find ("Shoes").Find ("Item").GetComponent<Button> ();

		bag = new List<Button> ();
		bagGroup = inventoryPanel.transform.Find ("Bag").gameObject;

		InventoryEventHandler.OnItemEquipped += EquipItem;
	}

	void Update () {
		if (Input.GetButtonDown ("Inventory")) {
			inventoryPanel.SetActive (!inventoryPanel.activeSelf);
		}

		if (Input.GetKeyDown (KeyCode.KeypadPlus)) {
			Item draconianSword = new Item ("Draconian Sword", Resources.Load<Sprite> ("UI/Icons/Items/draconianSword"), "Primary");
			AddItemToBag (draconianSword);
		}

		if (Input.GetKeyDown (KeyCode.KeypadMinus)) {
			RemoveItemFromBag (bag.Count - 1);
		}
	}

	public bool GetInventoryActive () {
		return inventoryPanel.activeSelf;
	}

	public void SetPlayerModel (Sprite player) {
		playerModel.sprite = player;
	}

	public void AddItemToBag (Item item) {
		item.itemSlot = bag.Count;
		Button newItem = Instantiate (bagSlot, bagGroup.transform);
		newItem.image.sprite = item.sprite;
		newItem.image.enabled = true;
		newItem.GetComponent<BaggedItem> ().item = item;
		bag.Add (newItem);
	}

	public void RemoveItemFromBag (int index) {
		Button item = bag [index];
		bag.RemoveAt (index);
		Destroy (item.gameObject);
	}

	public void EquipItem (Item item) {
		Button buttonToEquip = null;
		switch (item.itemPosition) 
		{
		case "Head":
			buttonToEquip = head;
			break;
		case "Amulet":
			buttonToEquip = amulet;
			break;
		case "Chest":
			buttonToEquip = chest;
			break;
		case "Gloves":
			buttonToEquip = gloves;
			break;
		case "Primary":
			buttonToEquip = primary;
			break;
		case "Secondary":
			buttonToEquip = secondary;
			break;
		case "Ring1":
			buttonToEquip = ring1;
			break;
		case "Ring2":
			buttonToEquip = ring2;
			break;
		case "Pants":
			buttonToEquip = pants;
			break;
		case "Shoes":
			buttonToEquip = shoes;
			break;			
		}
		if (buttonToEquip != null) {
			Item swapItem = null;
			if (buttonToEquip.image.enabled == true) {
				swapItem = buttonToEquip.GetComponent<EquippedItem> ().item;
			} 
			buttonToEquip.image.sprite = item.sprite;
			buttonToEquip.image.enabled = true;
			buttonToEquip.GetComponent<EquippedItem> ().item = item;
			RemoveItemFromBag (item.itemSlot);
			if (swapItem != null) {
				AddItemToBag (swapItem);
			}
		}
	}

	public void UnequipShoes () {
		shoes.image.enabled = false;
	}
}
