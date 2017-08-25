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
		head.image.sprite = item;
		head.image.enabled = true;
	}

	public void UnequipHead () {
		head.gameObject.SetActive (false);
	}

	public void EquipAmulet (Sprite item) {
		amulet.image.sprite = item;
		amulet.gameObject.SetActive (true);
	}

	public void UnequipAmulet () {
		amulet.gameObject.SetActive (false);
	}

	public void EquipChest (Sprite item) {
		chest.image.sprite = item;
		chest.gameObject.SetActive (true);
	}

	public void UnequipChest () {
		head.gameObject.SetActive (false);
	}

	public void EquipGloves (Sprite item) {
		gloves.image.sprite = item;
		gloves.gameObject.SetActive (true);
	}

	public void UnequipGloves () {
		gloves.gameObject.SetActive (false);
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
		secondary.gameObject.SetActive (true);
	}

	public void UnequipSecondary () {
		secondary.image.gameObject.SetActive (false);
	}

	public void EquipRing1 (Sprite item) {
		ring1.image.sprite = item;
		ring1.gameObject.SetActive (true);
	}

	public void UnequipRing1 () {
		ring1.gameObject.SetActive (false);
	}

	public void EquipRing2 (Sprite item) {
		ring2.image.sprite = item;
		ring2.gameObject.SetActive (true);
	}

	public void UnequipRing2 () {
		ring2.gameObject.SetActive (false);
	}

	public void EquipPants (Sprite item) {
		pants.image.sprite = item;
		pants.gameObject.SetActive (true);
	}

	public void UnequipPants () {
		pants.gameObject.SetActive (false);
	}

	public void EquipShoes (Sprite item) {
		shoes.image.sprite = item;
		shoes.gameObject.SetActive (true);
	}

	public void UnequipShoes () {
		shoes.gameObject.SetActive (false);
	}
}
