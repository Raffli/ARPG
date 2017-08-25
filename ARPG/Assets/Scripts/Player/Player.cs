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

	[HideInInspector] public Stat health, healthPerSecond, healthPerHit; // Defense - calculated from vitality and hph from item
	[HideInInspector] public Stat mana, manaPerSecond, manaPerHit; // Resource - calculated from intelligence and mph from item

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
		maximumBagSlots = 64;

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

		healthPerHit = new Stat (0, "Health per Hit", "How much health you regenerate when hitting an enemy.");
		manaPerHit = new Stat (0, "Mana per Hit", "How much mana you regenerate when hitting an enemy.");

		UpdateDynamicStats ();

		currentHealth = maximumHealth; 
		currentMana = maximumMana;

		HUDManager.Instance.UpdateHP (currentHealth, health.GetValue());
		HUDManager.Instance.UpdateMana (currentMana, mana.GetValue());
		HUDManager.Instance.UpdateXPBar (xp, xpToLevel);
	}

	public void UpdateDynamicStats () {
		health = new Stat (vitality.GetValue() * 10, "Health", "Your health. If it is at 0 you die!");
		float hps = vitality.GetValue () * 0.1f;
		healthPerSecond = new Stat (Mathf.RoundToInt (hps), "Health per Second", "How much health you regenerate every second.");
		mana = new Stat (intelligence.GetValue() * 10, "Mana", "Spiritual energy used for spells.");
		float mps = intelligence.GetValue () * 0.1f;
		manaPerSecond = new Stat (Mathf.RoundToInt (mps), "Mana per Second", "How much mana you regenerate every second.");
		maximumHealth = health.GetValue();
		maximumMana = mana.GetValue();
	}

	public void AddItemToBag (Item item) {
		if (bag.Count < maximumBagSlots) {
			// give item slot. bag.Count
			bag.Add (item);
		}
	}

	public void RemoveItemFromBag (Item item) {
		bag.Remove (item);
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
