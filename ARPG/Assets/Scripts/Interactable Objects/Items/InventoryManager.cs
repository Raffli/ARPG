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
	private Button head, amulet, chest, gloves, primary, secondary, ring1, ring2, pants, shoes;
	private GameObject descriptionGroup;
	private Text itemName, itemDescription, itemStats;
	public Button destroyButton;
	private GameObject bagGroup;
	public Button bagSlot;
	private List <Button> bag;
	public int maximumBagSlots { get; set; }

	public Color normal;
	public Color magic;
	public Color epic;
	public Color legendary;

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

		descriptionGroup = inventoryPanel.transform.Find ("Description").gameObject;
		itemName = descriptionGroup.transform.Find ("Name").GetComponent<Text> ();
		itemDescription = descriptionGroup.transform.Find ("MainDescription").GetComponent<Text> ();
		itemStats = descriptionGroup.transform.Find ("StatsBoost").GetComponent<Text> ();

		bag = new List<Button> ();
		bagGroup = inventoryPanel.transform.Find ("Bag").gameObject;

		InventoryEventHandler.OnItemEquipped += EquipItem;
		InventoryEventHandler.OnItemUnequipped += UnequipItem;
		InventoryEventHandler.OnItemBagged += AddItemToBag;
		InventoryEventHandler.OnItemUnbagged += RemoveItemFromBag;
		InventoryEventHandler.OnItemDestroyed += DestroyItem;
	}

	void Update () {
		if (Input.GetButtonDown ("Inventory")) {
			inventoryPanel.SetActive (!inventoryPanel.activeSelf);
		}

		if (Input.GetKeyDown (KeyCode.KeypadPlus)) {
			Item draconianSword = new Item ("Draconian Sword", Resources.Load<Sprite> ("UI/Icons/Items/draconianSword"), Item.ItemPosition.Primary);
			draconianSword.itemDescription = "One Handed Sword.";
			draconianSword.itemState = Item.ItemState.Magic;
			draconianSword.itemStats.Add (new StatBonus (23, "Damage"));
			draconianSword.itemStats.Add (new StatBonus (14, "Strength"));
			draconianSword.itemStats.Add (new StatBonus (17, "Vitality"));
			InventoryEventHandler.ItemBagged (draconianSword);
		}
	}

	public bool GetInventoryActive () {
		return inventoryPanel.activeSelf;
	}

	public void SetPlayerModel (Sprite player) {
		playerModel.sprite = player;
	}

	public void DestroyItem (Item item) {
		descriptionGroup.SetActive (false);
		if (item.itemEquipped) {
			item.toDestroy = true;
			UnequipItem (item);
		} else {
			RemoveItemFromBag (item);
		}
	}

	public void ShowItemDescription (Item item) {
		destroyButton.GetComponent<DestroyItem> ().item = item;
		descriptionGroup.SetActive (true);
		itemName.text = item.name;
		itemDescription.text = item.itemDescription;
		itemStats.text = "";
		foreach (StatBonus bonus in item.itemStats) {
			itemStats.text += "+ " + bonus.value + " " + bonus.statType + "\n";
		}
		if (item.itemState == Item.ItemState.Normal) {
			itemName.color = normal;
			itemStats.color = normal;
		} else if (item.itemState == Item.ItemState.Magic) {
			itemName.color = magic;
			itemStats.color = magic;
		} else if (item.itemState == Item.ItemState.Epic) {
			itemName.color = epic;
			itemStats.color = epic;
		} else if (item.itemState == Item.ItemState.Legendary) {
			itemName.color = legendary;
			itemStats.color = legendary;
		}
	}

	public void AddItemToBag (Item item) {
		if (bag.Count < maximumBagSlots) {
			Button newItem = Instantiate (bagSlot, bagGroup.transform);
			newItem.image.sprite = item.sprite;
			newItem.image.enabled = true;
			newItem.GetComponent<BaggedItem> ().item = item;
			bag.Add (newItem);
		}
	}

	public void RemoveItemFromBag (Item item) {
		Button button = bag [item.itemSlot];
		bag.RemoveAt (item.itemSlot);
		Destroy (button.gameObject);
	}

	public void EquipItem (Item item) {
		Button buttonToEquip = null;

		switch (item.itemPosition) 
		{
		case Item.ItemPosition.Head:
			buttonToEquip = head;
			break;
		case Item.ItemPosition.Amulet:
			buttonToEquip = amulet;
			break;
		case Item.ItemPosition.Chest:
			buttonToEquip = chest;
			break;
		case Item.ItemPosition.Gloves:
			buttonToEquip = gloves;
			break;
		case Item.ItemPosition.Primary:
			buttonToEquip = primary;
			break;
		case Item.ItemPosition.Secondary:
			buttonToEquip = secondary;
			break;
		case Item.ItemPosition.Ring1:
			buttonToEquip = ring1;
			break;
		case Item.ItemPosition.Ring2:
			buttonToEquip = ring2;
			break;
		case Item.ItemPosition.Pants:
			buttonToEquip = pants;
			break;
		case Item.ItemPosition.Shoes:
			buttonToEquip = shoes;
			break;			
		}
			
		if (buttonToEquip != null) {
			Item swapItem = null;
			if (buttonToEquip.image.enabled == true) {
				InventoryEventHandler.ItemUnequipped (buttonToEquip.GetComponent<EquippedItem> ().item);
			} 
			buttonToEquip.image.sprite = item.sprite;
			buttonToEquip.image.enabled = true;
			buttonToEquip.GetComponent<EquippedItem> ().item = item;
			InventoryEventHandler.ItemUnbagged (item);
			if (swapItem != null) {
				AddItemToBag (swapItem);
			}
		}
	}

	public void UnequipItem (Item item) {
		if (bag.Count < maximumBagSlots) {
			Button buttonToUnequip = null;

			switch (item.itemPosition) 
			{
			case Item.ItemPosition.Head:
				buttonToUnequip = head;
				break;
			case Item.ItemPosition.Amulet:
				buttonToUnequip = amulet;
				break;
			case Item.ItemPosition.Chest:
				buttonToUnequip = chest;
				break;
			case Item.ItemPosition.Gloves:
				buttonToUnequip = gloves;
				break;
			case Item.ItemPosition.Primary:
				buttonToUnequip = primary;
				break;
			case Item.ItemPosition.Secondary:
				buttonToUnequip = secondary;
				break;
			case Item.ItemPosition.Ring1:
				buttonToUnequip = ring1;
				break;
			case Item.ItemPosition.Ring2:
				buttonToUnequip = ring2;
				break;
			case Item.ItemPosition.Pants:
				buttonToUnequip = pants;
				break;
			case Item.ItemPosition.Shoes:
				buttonToUnequip = shoes;
				break;			
			}

			buttonToUnequip.image.enabled = false;
			buttonToUnequip.GetComponent<EquippedItem> ().item = null;
			if (!item.toDestroy) {
				AddItemToBag (item);
			}
		}
	
	}

	public void UnequipShoes () {
		shoes.image.enabled = false;
	}
}
