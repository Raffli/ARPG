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
	private Image head, amulet, chest, gloves, primary, secondary, ring1, ring2, pants, shoes;
	private GameObject bagGroup;
	public Image bagSlot;
	private List <Image> bag;
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
		head = equippedGroup.Find ("Head").Find ("Item").GetComponent<Image> ();
		amulet = equippedGroup.Find ("Amulet").Find ("Item").GetComponent<Image> ();
		chest = equippedGroup.Find ("Chest").Find ("Item").GetComponent<Image> ();
		gloves = equippedGroup.Find ("Gloves").Find ("Item").GetComponent<Image> ();
		primary = equippedGroup.Find ("Primary").Find ("Item").GetComponent<Image> ();
		secondary = equippedGroup.Find ("Secondary").Find ("Item").GetComponent<Image> ();
		ring1 = equippedGroup.Find ("Ring1").Find ("Item").GetComponent<Image> ();
		ring2 = equippedGroup.Find ("Ring2").Find ("Item").GetComponent<Image> ();
		pants = equippedGroup.Find ("Pants").Find ("Item").GetComponent<Image> ();
		shoes = equippedGroup.Find ("Shoes").Find ("Item").GetComponent<Image> ();

		bag = new List<Image> ();
		bagGroup = inventoryPanel.transform.Find ("Bag").gameObject;
		draconianSword = Resources.Load<Sprite> ("UI/Icons/Items/draconianSword");
	}

	void Update () {
		if (Input.GetButtonDown ("Inventory")) {
			inventoryPanel.SetActive (!inventoryPanel.activeSelf);
		}

		if (Input.GetKeyDown (KeyCode.KeypadPlus)) {
			EquipPrimary (draconianSword);
		}

		if (Input.GetKeyDown (KeyCode.KeypadMinus)) {
			UnequipPrimary ();
		}
	}

	public bool GetInventoryAcitve () {
		return inventoryPanel.activeSelf;
	}

	public void SetPlayerModel (Sprite player) {
		playerModel.sprite = player;
	}

	public void AddItemToBag (Sprite item) {
		Image newItem = Instantiate (bagSlot, bagGroup.transform);
		newItem.sprite = item;
		bag.Add (newItem);
	}

	public void RemoveItemFromBag (int index) {
		Image item = bag [index];
		bag.RemoveAt (index);
		Destroy (item.gameObject);
	}

	public void EquipHead (Sprite item) {
		head.sprite = item;
		head.gameObject.SetActive (true);
	}

	public void UnequipHead () {
		head.gameObject.SetActive (false);
	}

	public void EquipAmulet (Sprite item) {
		amulet.sprite = item;
		amulet.gameObject.SetActive (true);
	}

	public void UnequipAmulet () {
		amulet.gameObject.SetActive (false);
	}

	public void EquipChest (Sprite item) {
		chest.sprite = item;
		chest.gameObject.SetActive (true);
	}

	public void UnequipChest () {
		head.gameObject.SetActive (false);
	}

	public void EquipGloves (Sprite item) {
		gloves.sprite = item;
		gloves.gameObject.SetActive (true);
	}

	public void UnequipGloves () {
		gloves.gameObject.SetActive (false);
	}

	public void EquipPrimary (Sprite item) {
		primary.sprite = item;
		primary.gameObject.SetActive (true);
	}

	public void UnequipPrimary () {
		primary.gameObject.SetActive (false);
	}

	public void EquipSecondary (Sprite item) {
		secondary.sprite = item;
		secondary.gameObject.SetActive (true);
	}

	public void UnequipSecondary () {
		secondary.gameObject.SetActive (false);
	}

	public void EquipRing1 (Sprite item) {
		ring1.sprite = item;
		ring1.gameObject.SetActive (true);
	}

	public void UnequipRing1 () {
		ring1.gameObject.SetActive (false);
	}

	public void EquipRing2 (Sprite item) {
		ring2.sprite = item;
		ring2.gameObject.SetActive (true);
	}

	public void UnequipRing2 () {
		ring2.gameObject.SetActive (false);
	}

	public void EquipPants (Sprite item) {
		pants.sprite = item;
		pants.gameObject.SetActive (true);
	}

	public void UnequipPants () {
		pants.gameObject.SetActive (false);
	}

	public void EquipShoes (Sprite item) {
		shoes.sprite = item;
		shoes.gameObject.SetActive (true);
	}

	public void UnequipShoes () {
		shoes.gameObject.SetActive (false);
	}
}
