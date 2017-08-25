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
	private GameObject bagGroup;
	public Button bagSlot;
	private List <Button> bag;
	private Sprite draconianSword;

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
		draconianSword = Resources.Load<Sprite> ("UI/Icons/Items/draconianSword");
	}

	void Update () {
		if (Input.GetButtonDown ("Inventory")) {
			inventoryPanel.SetActive (!inventoryPanel.activeSelf);
		}

		if (Input.GetKeyDown (KeyCode.KeypadPlus)) {
			AddItemToBag (draconianSword);
		}

		if (Input.GetKeyDown (KeyCode.KeypadMinus)) {
			RemoveItemFromBag (bag.Count - 1);
		}
	}

	public bool GetInventoryAcitve () {
		return inventoryPanel.activeSelf;
	}

	public void SetPlayerModel (Sprite player) {
		playerModel.sprite = player;
	}

	public void AddItemToBag (Sprite item) {
		Button newItem = Instantiate (bagSlot, bagGroup.transform);
		newItem.image.sprite = item;
		newItem.image.enabled = true;
		bag.Add (newItem);
	}

	public void RemoveItemFromBag (int index) {
		Button item = bag [index];
		bag.RemoveAt (index);
		Destroy (item.gameObject);
	}

	public void EquipHead (Sprite item) {
		head.image.sprite = item;
		head.image.enabled = true;
	}

	public void UnequipHead () {
		head.image.enabled = false;
	}

	public void EquipAmulet (Sprite item) {
		amulet.image.sprite = item;
		amulet.image.enabled = true;
	}

	public void UnequipAmulet () {
		amulet.image.enabled = false;
	}

	public void EquipChest (Sprite item) {
		chest.image.sprite = item;
		chest.image.enabled = true;
	}

	public void UnequipChest () {
		head.image.enabled = false;
	}

	public void EquipGloves (Sprite item) {
		gloves.image.sprite = item;
		gloves.image.enabled = true;
	}

	public void UnequipGloves () {
		gloves.image.enabled = false;
	}

	public void EquipPrimary (Sprite item) {
		primary.image.sprite = item;
		primary.image.enabled = true;
	}

	public void UnequipPrimary () {
		primary.image.enabled = false;
	}

	public void EquipSecondary (Sprite item) {
		secondary.image.sprite = item;
		secondary.image.enabled = true;
	}

	public void UnequipSecondary () {
		secondary.image.enabled = false;
	}

	public void EquipRing1 (Sprite item) {
		ring1.image.sprite = item;
		ring1.image.enabled = true;
	}

	public void UnequipRing1 () {
		ring1.image.enabled = false;
	}

	public void EquipRing2 (Sprite item) {
		ring2.image.sprite = item;
		ring2.image.enabled = true;
	}

	public void UnequipRing2 () {
		ring2.image.enabled = false;
	}

	public void EquipPants (Sprite item) {
		pants.image.sprite = item;
		pants.image.enabled = true;
	}

	public void UnequipPants () {
		pants.image.enabled = false;
	}

	public void EquipShoes (Sprite item) {
		shoes.image.sprite = item;
		shoes.image.enabled = true;
	}

	public void UnequipShoes () {
		shoes.image.enabled = false;
	}
}
