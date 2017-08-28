using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AI;


public class Player : NetworkBehaviour {

	[SyncVar]
	public int level;
	public int xp;
	public int xpToLevel;

	[HideInInspector] public Stat vitality, dexterity, strength, intelligence; // Base Stats
	[HideInInspector] public Stat armor; // Defense - depends on items and skills
	[HideInInspector] public Stat damage; // Offense - depends on items and skills
	[HideInInspector] public Stat critChance, cooldownReduction; // Offense - depends on items

	[HideInInspector] public Stat health, healthPerSecond; // Defense - calculated from vitality and items
	[HideInInspector] public Stat mana, manaPerSecond; // Resource - calculated from intelligence and items

	public float critDamage { get; set; }
	public bool invisible { get; set; }

	public List <Item> bag;
	public int maximumBagSlots { get; set; }

	[HideInInspector] [SyncVar] public int currentMana;
	[HideInInspector] [SyncVar] public int maximumMana;
	[HideInInspector] [SyncVar] public int currentHealth;
	[HideInInspector] [SyncVar] public int maximumHealth;

	private NetworkStartPosition[] spawnPoints;


	void Awake(){
		DontDestroyOnLoad(transform.gameObject);    
	}

	void Start() {
		if (!isLocalPlayer)
		{
			Destroy(transform.Find("Main Camera").gameObject);
		}
		else {
			transform.Find("Main Camera").gameObject.SetActive(true);
		}
		critDamage = 1.5f;
		invisible = false;
		bag = new List<Item> ();
		maximumBagSlots = 40;
		InventoryManager.Instance.maximumBagSlots = maximumBagSlots;

		vitality = new Stat (10, "Vitality", "Measures how sturdy your character is.");
		dexterity = new Stat (10, "Dexterity", "Measures how agile your character is.");
		strength = new Stat (10, "Strength", "Measures how physically strong your character is.");
		intelligence = new Stat (10, "Intelligence", "Measures how intelligent your character is.");

		Sprite playerModel;
		if (tag.Equals ("Mage")) {
			intelligence.baseValue += 15;
			playerModel = Resources.Load<Sprite> ("UI/mage");
		} else if (tag.Equals ("Rouge")) {
			vitality.baseValue += 5;
			dexterity.baseValue += 10;
			playerModel = Resources.Load<Sprite> ("UI/rogue");
		} else {
			vitality.baseValue += 10;
			strength.baseValue += 5;
			playerModel = Resources.Load<Sprite> ("UI/warrior");
		}
		InventoryManager.Instance.SetPlayerModel (playerModel);

		damage = new Stat (0, "Damage", "Measures the extra damage you deal on your attacks.");
		armor = new Stat (0, "Armor", "Measures how much damage you can absorb.");
		critChance = new Stat (5, "Crit Chance", "Measures the chance to strike an enemy critical.");
		cooldownReduction = new Stat (0, "Cooldown Reduction", "Reduces the cooldowns of your skills.");

		health = new Stat (vitality.GetValue() * 10, "Health", "Your health. If it is at 0 you die!");
		float hps = vitality.GetValue () * 0.1f;
		healthPerSecond = new Stat (Mathf.RoundToInt (hps), "Health per Second", "How much health you regenerate every second.");
		mana = new Stat (intelligence.GetValue() * 10, "Mana", "Spiritual energy used for spells.");
		Debug.Log ("new mana is " + mana.GetValue ());
		float mps = intelligence.GetValue () * 0.1f;
		manaPerSecond = new Stat (Mathf.RoundToInt (mps), "Mana per Second", "How much mana you regenerate every second.");
		maximumHealth = health.GetValue();
		maximumMana = mana.GetValue();

		currentHealth = maximumHealth; 
		currentMana = maximumMana;

		HUDManager.Instance.UpdateHP (currentHealth, health.GetValue());
		HUDManager.Instance.UpdateMana (currentMana, mana.GetValue());
		HUDManager.Instance.UpdateXPBar (xp, xpToLevel);

		InventoryEventHandler.OnItemEquipped += EquipItem;
		InventoryEventHandler.OnItemUnequipped += UnequipItem;
		InventoryEventHandler.OnItemBagged += AddItemToBag;
		InventoryEventHandler.OnItemUnbagged += RemoveItemFromBag;
		InventoryEventHandler.OnItemDestroyed += DestroyItem;
	}

	public void UpdateDynamicStats () {
		health.baseValue = vitality.GetValue () * 10;
		Debug.Log ("new health is " + health.GetValue ());
		float hps = vitality.GetValue () * 0.1f;
		healthPerSecond.baseValue = Mathf.RoundToInt (hps);
		mana.baseValue = intelligence.GetValue () * 10;
		Debug.Log ("new mana is " + mana.GetValue ());
		float mps = intelligence.GetValue () * 0.1f;
		manaPerSecond.baseValue = Mathf.RoundToInt (mps);
		maximumHealth = health.GetValue();
		maximumMana = mana.GetValue();
	}

	public void AddItemToBag (Item item) {
		item.itemEquipped = false;
		if (bag.Count < maximumBagSlots) {
			item.itemSlot = bag.Count;
			bag.Add (item);
		}
	}

	public void RemoveItemFromBag (Item item) {
		bag.Remove (item);
		UpdateItemSlotInBag ();
	}

	public void UpdateItemSlotInBag () {
		for (int i = 0; i < bag.Count; i++) {
			bag [i].itemSlot = i;
		}
	}

	public void DestroyItem (Item item) {
		if (item.itemEquipped) {
			UnequipItem (item);
		} else {
			RemoveItemFromBag (item);
		}
	}

	public void EquipItem (Item item) {
		item.itemEquipped = true;
		foreach (StatBonus bonus in item.itemStats) {
			switch (bonus.statType) 
			{
			case "Damage": 
				damage.AddBonus (bonus.value);
				break;
			case "Vitality": 
				vitality.AddBonus (bonus.value);
				UpdateDynamicStats ();
				break;
			case "Dexterity": 
				dexterity.AddBonus (bonus.value);
				break;
			case "Intelligence": 
				intelligence.AddBonus (bonus.value);
				UpdateDynamicStats ();
				break;
			case "Strength": 
				strength.AddBonus (bonus.value);
				break;
			case "Armor": 
				armor.AddBonus (bonus.value);
				break;
			case "Crit Chance": 
				critChance.AddBonus (bonus.value);
				break;
			case "Cooldown Reduction": 
				cooldownReduction.AddBonus (bonus.value);
				break;
			case "Health": 
				health.AddBonus (bonus.value);
				break;
			case "Health per Second": 
				healthPerSecond.AddBonus (bonus.value);
				break;
			case "Mana": 
				mana.AddBonus (bonus.value);
				break;
			case "Mana per Second": 
				manaPerSecond.AddBonus (bonus.value);
				break;
			}
		}

	}

	public void UnequipItem (Item item) {
		if (bag.Count < maximumBagSlots) {
			foreach (StatBonus bonus in item.itemStats) {
				switch (bonus.statType) 
				{
				case "Damage": 
					damage.RemoveBonus (bonus.value);
					break;
				case "Vitality": 
					vitality.RemoveBonus (bonus.value);
					UpdateDynamicStats ();
					break;
				case "Dexterity": 
					dexterity.RemoveBonus (bonus.value);
					break;
				case "Intelligence": 
					intelligence.RemoveBonus (bonus.value);
					UpdateDynamicStats ();
					break;
				case "Strength": 
					strength.RemoveBonus (bonus.value);
					break;
				case "Armor": 
					armor.RemoveBonus (bonus.value);
					break;
				case "Crit Chance": 
					critChance.RemoveBonus (bonus.value);
					break;
				case "Cooldown Reduction": 
					cooldownReduction.RemoveBonus (bonus.value);
					break;
				case "Health": 
					health.RemoveBonus (bonus.value);
					break;
				case "Health per Second": 
					healthPerSecond.RemoveBonus (bonus.value);
					break;
				case "Mana": 
					mana.RemoveBonus (bonus.value);
					break;
				case "Mana per Second": 
					manaPerSecond.RemoveBonus (bonus.value);
					break;
				}
			}
			if (!item.toDestroy) {
				AddItemToBag (item);
			}
		}
	}

	public bool GetCritted () {
		if (Random.Range (0, 100) <= critChance.GetValue ()) {
			return true;
		}
		return false;
	}

	public void TakeDamage (int amount) {
		amount -= armor.GetValue ();
		if (amount > 0) {
			ReduceHealth (amount);
		}
	}

	private void OnLevelWasLoaded(int level)
	{
		if (level > 1) {

			GetComponent<NavMeshAgent>().enabled = false;
			spawnPoints = FindObjectsOfType<NetworkStartPosition>();

			Vector3 spawnPoint = Vector3.zero;

			if (spawnPoints != null && spawnPoints.Length > 0)
			{
				spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
			}
			transform.position = spawnPoint;
			GetComponent<NavMeshAgent>().enabled = true;

		}
	}

	public void Heal (int amount) {
		currentHealth += amount;
		if (currentHealth > health.GetValue()) {
			currentHealth = health.GetValue();
		}
		HUDManager.Instance.UpdateHP (currentHealth, maximumHealth);
	}

	public void ReduceHealth (int amount) {
		if (currentHealth > health.GetValue ()) {
			currentHealth = health.GetValue ();
		}
		currentHealth -= amount;
		HUDManager.Instance.UpdateHP (currentHealth, maximumHealth);
		if (currentHealth <= 0) {
			Die ();
		}
	}

	public void IncreaseMana (int amount) {
		currentMana += amount;
		if (currentMana > mana.GetValue()) {
			currentMana = mana.GetValue();
		}
		HUDManager.Instance.UpdateMana (currentMana, maximumMana);
	}

	public void ReduceMana (int amount) {
		currentMana -= amount;
		HUDManager.Instance.UpdateMana (currentMana, maximumMana);
	}

	void Die() {
		currentHealth = maximumHealth;
	}
}
