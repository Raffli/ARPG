using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootManager : MonoBehaviour {

	public static LootManager Instance;

	public GameObject loot;
	public int playerLevel { get; set; }
	public string playerClass { get; set; }

	public Color normal;
	public Color magic;
	public Color epic;
	public Color legendary;

	void Start () {
		DontDestroyOnLoad (transform.gameObject);

		if (Instance != null && Instance != this) {
			Destroy (gameObject);
		} else {
			Instance = this;
		}
	}

	public void UpdateLevel (int newLevel) {
		playerLevel = newLevel;
	}

	public void SpawnLoot (Vector3 lootPosition) {
		GameObject lootObject = Instantiate (loot) as GameObject;
		lootObject.transform.position = new Vector3 (lootPosition.x, lootPosition.y + 2f, lootPosition.z);
		Item lootItem = ItemGenerator.Instance.GenerateRandomItem (playerLevel, playerClass);
		lootObject.transform.Find ("ItemBackground").GetComponent<Loot> ().item = lootItem;
		Text lootName = lootObject.transform.Find ("ItemName").GetComponent<Text> ();
		lootName.text = lootItem.name;
		if (lootItem.itemState == Item.ItemState.Normal) {
			lootName.color = normal;
		} else if (lootItem.itemState == Item.ItemState.Magic) {
			lootName.color = magic;
		} else if (lootItem.itemState == Item.ItemState.Epic) {
			lootName.color = epic;
		} else {
			lootName.color = legendary;
		}
	}

}
