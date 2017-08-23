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
	[HideInInspector] public Stat armor; // Defense - depends on items
	[HideInInspector] public Stat damage; // Offense - calculated from dex, str and int
	[HideInInspector] public Stat critChance, cooldownReduction; // Offense - depends on items

	[HideInInspector] public Stat health, healthPerSecond, healthPerHit; // Defense - calculated from vitality and hph from item
	[HideInInspector] public Stat mana, manaPerSecond, manaPerHit; // Resource - calculated from intelligence and mph from item

	[HideInInspector] [SyncVar] public int currentMana;
	[HideInInspector] [SyncVar] public int maximumMana;
	[HideInInspector] [SyncVar] public int currentHealth;
	[HideInInspector] [SyncVar] public int maximumHealth;

	private float dexScale;
	private float strScale;
	private float intScale;

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

		vitality = new Stat (10, "Vitality", "Measures how sturdy your character is.");
		dexterity = new Stat (10, "Dexterity", "Measures how agile your character is.");
		strength = new Stat (10, "Strength", "Measures how physically strong your character is.");
		intelligence = new Stat (10, "Intelligence", "Measures how intelligent your character is.");

		if (tag.Equals ("Mage")) {
			intelligence.baseValue += 15;
			intScale = 0.5f;
			dexScale = 0.1f;
			strScale = 0.1f;
		} else if (tag.Equals ("Rouge")) {
			vitality.baseValue += 5;
			dexterity.baseValue += 10;
			intScale = 0.1f;
			dexScale = 0.5f;
			strScale = 0.1f;
		} else if (tag.Equals ("Warrior")) {
			vitality.baseValue += 10;
			strength.baseValue += 5;
			intScale = 0.1f;
			dexScale = 0.1f;
			strScale = 0.5f;
		}

		armor = new Stat (0, "Armor", "Measures how much damage you can absorb.");
		damage = new Stat (calculateDamage(), "Damage", "Measures how much damage you do with your attacks.");
		critChance = new Stat (5, "Crit Chance", "Measures the chance to strike an enemy critical.");
		cooldownReduction = new Stat (0, "Cooldown Reduction", "Reduces the cooldowns of your skills.");
		health = new Stat (vitality.GetValue() * 10, "Health", "Your health. If it is at 0 you die!");
		float hps = vitality.GetValue () * 0.1f;
		healthPerSecond = new Stat (Mathf.RoundToInt (hps), "Health per Second", "How much health you regenerate every second.");
		healthPerHit = new Stat (0, "Health per Hit", "How much health you regenerate when hitting an enemy.");
		mana = new Stat (100, "Mana", "Spiritual energy used for spells.");
		float mps = intelligence.GetValue () * 0.1f;
		manaPerSecond = new Stat (Mathf.RoundToInt (mps), "Mana per Second", "How much mana you regenerate every second.");
		manaPerHit = new Stat (0, "Mana per Hit", "How much mana you regenerate when hitting an enemy.");

		maximumHealth = health.GetValue();
		currentHealth = maximumHealth; 
		maximumMana = mana.GetValue();
		currentMana = maximumMana;

		HUDManager.Instance.UpdateHP (currentHealth, health.GetValue());
		HUDManager.Instance.UpdateMana (currentMana, mana.GetValue());
		HUDManager.Instance.UpdateXPBar (xp, xpToLevel);
	}

	private int calculateDamage () {
		int dmg = 10;
		dmg += Mathf.RoundToInt ((dexterity.GetValue() * dexScale));
		dmg += Mathf.RoundToInt ((strength.GetValue() * strScale));
		dmg += Mathf.RoundToInt ((intelligence.GetValue() * intScale));
		return dmg;
	}

	public void TakeDamage (int amount) {
		amount -= armor.GetValue ();
		currentHealth -= amount;
		HUDManager.Instance.UpdateHP (currentHealth, health.GetValue());
		if (currentHealth <= 0) {
			Die();
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
		HUDManager.Instance.UpdateHP (currentHealth, health.GetValue());
	}

	public void IncreaseMana (int amount) {
		currentMana += amount;
		if (currentMana > mana.GetValue()) {
			currentMana = mana.GetValue();
		}
		HUDManager.Instance.UpdateMana (currentMana, mana.GetValue());
	}

	public void ReduceMana (int amount) {
		currentMana -= amount;
		HUDManager.Instance.UpdateMana (currentMana, mana.GetValue());
	}

	void Die() {
		currentHealth = health.GetValue();
	}
}
