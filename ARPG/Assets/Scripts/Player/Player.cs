using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AI;


public class Player : NetworkBehaviour {

    [SyncVar] public int level;
    [SyncVar] private int xp;
    private int xpToLevel;

    private GameObject levelUp;

    [HideInInspector] public Stat vitality, dexterity, strength, intelligence; // Base Stats
    [HideInInspector] public Stat armor; // Defense - depends on items and skills
    [HideInInspector] public Stat damage; // Offense - depends on items and skills
    [HideInInspector] public Stat critChance, cooldownReduction; // Offense - depends on items

    [HideInInspector] public Stat health, healthPerSecond; // Defense - calculated from vitality and items
    [HideInInspector] public Stat mana, manaPerSecond; // Resource - calculated from intelligence and items

    public float critDamage { get; set; }
    public bool invisible { get; set; }
    public string className { get; set; }
    public string playerName { get; set; }
    private Attack attack;

    public List<Item> bag;
    public int maximumBagSlots { get; set; }
    public bool playerIsOnServer { get; set; }

    [HideInInspector] [SyncVar] public int currentMana;
    [HideInInspector] [SyncVar] public int maximumMana;
    [HideInInspector] [SyncVar] public int currentHealth;
    [HideInInspector] [SyncVar] public int maximumHealth;

    private NetworkStartPosition[] spawnPoints;

    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }

    void Start() {
        if (!isLocalPlayer) {
            Destroy(transform.Find("Main Camera").gameObject);
        } else {
            transform.Find("Main Camera").gameObject.SetActive(true);
        }

        critDamage = 1.5f;
        invisible = false;

        bag = new List<Item>();
        maximumBagSlots = 40;
        InventoryManager.Instance.maximumBagSlots = maximumBagSlots;

        level = 1;
        xp = 0;
        xpToLevel = 100;
        playerName = "Flo";

        attack = transform.GetComponent<Attack>();

        vitality = new Stat(10, "Vitality", "Measures how sturdy your character is.");
        dexterity = new Stat(10, "Dexterity", "Measures how agile your character is.");
        strength = new Stat(10, "Strength", "Measures how physically strong your character is.");
        intelligence = new Stat(10, "Intelligence", "Measures how intelligent your character is.");

        Sprite playerModel;
        if (tag.Equals("Mage")) {
            className = "Mage";
            intelligence.baseValue += 15;
            playerModel = Resources.Load<Sprite>("UI/mage");
        } else if (tag.Equals("Rouge")) {
            className = "Rouge";
            vitality.baseValue += 5;
            dexterity.baseValue += 10;
            playerModel = Resources.Load<Sprite>("UI/rogue");
        } else {
            className = "Warrior";
            vitality.baseValue += 10;
            strength.baseValue += 5;
            playerModel = Resources.Load<Sprite>("UI/warrior");
        }

        damage = new Stat(0, "Damage", "Measures the extra damage you deal on your attacks.");
        armor = new Stat(0, "Armor", "Measures how much damage you can absorb.");
        critChance = new Stat(5, "Crit Chance", "Measures the chance to strike an enemy critical.");
        cooldownReduction = new Stat(0, "Cooldown Reduction", "Reduces the cooldowns of your skills.");

        health = new Stat(vitality.GetValue() * 10, "Health", "Your health. If it is at 0 you die!");
        float hps = vitality.GetValue() * 0.1f;
        healthPerSecond = new Stat(Mathf.RoundToInt(hps), "Health per Second", "How much health you regenerate every second.");
        mana = new Stat(intelligence.GetValue() * 10, "Mana", "Spiritual energy used for spells.");
        float mps = intelligence.GetValue() * 0.1f;
        manaPerSecond = new Stat(Mathf.RoundToInt(mps), "Mana per Second", "How much mana you regenerate every second.");
        maximumHealth = health.GetValue();
        maximumMana = mana.GetValue();

        currentHealth = maximumHealth;
        currentMana = maximumMana;

        if (isLocalPlayer) {
            PlayerEventHandler.LevelUp(level);
            CharacterManager.Instance.player = this;
            LootManager.Instance.playerClass = className;
            InventoryManager.Instance.SetPlayerModel(playerModel);
            CharacterManager.Instance.SetNameAndClass(playerName, className);
            CharacterManager.Instance.UpdateStats();

            HUDManager.Instance.UpdateHP(currentHealth, health.GetValue());
            HUDManager.Instance.UpdateMana(currentMana, mana.GetValue());
            HUDManager.Instance.UpdateXPBar(xp, xpToLevel);

            InventoryEventHandler.OnItemEquipped += EquipItem;
            InventoryEventHandler.OnItemUnequipped += UnequipItem;
            InventoryEventHandler.OnItemBagged += AddItemToBag;
            InventoryEventHandler.OnItemUnbagged += RemoveItemFromBag;
            InventoryEventHandler.OnItemDestroyed += DestroyItem;
        }
        playerIsOnServer = isServer;
        QuestManager.Instance.isOnServer = playerIsOnServer;
        if (isServer)
        {
            PlayerEventHandler.OnXpGained += GiveXP;
        }
        StartCoroutine(LearnPrimarySkill());
		InvokeRepeating("RegenManaAndHealth", 1f, 1f);
    }

    private void Update()
    {
        if (isLocalPlayer)
        {
            HUDManager.Instance.UpdateXPBar(xp, xpToLevel);
            HUDManager.Instance.UpdateMana(currentMana, maximumMana);
            HUDManager.Instance.UpdateHP(currentHealth, maximumHealth);
        }
    }

    IEnumerator LearnPrimarySkill() {
        yield return new WaitForSeconds(0.1f);
        attack.healPotion.SetProperties(this);
        attack.manaPotion.SetProperties(this);
        attack.LearnPrimarySkill();
    }

    public void GiveXP(int amount) {
        xp += amount;
        if (xp >= xpToLevel)
        {
            PlayerEventHandler.LevelUp(level + 1);
            LevelUp();
            RpcLevelUpOnClient();
        }
    }


    [ClientRpc]
    private void RpcLevelUpOnClient() {
        if (!isServer) {
            LevelUp();
            attack.LevelUp(level);
			CharacterManager.Instance.UpdateLevel (level);
        }
    }

    [Command]
    public void CmdSpawnEffect()
    {
        GameObject levelUpEffect = (GameObject)Resources.Load("Skills/LevelUp");
        GameObject obj = Instantiate(levelUpEffect, GetComponent<NetworkTransform>().gameObject.transform.position, GetComponent<NetworkTransform>().gameObject.transform.rotation, GetComponent<NetworkTransform>().gameObject.transform);
        NetworkServer.Spawn(obj);
    }

    private void LevelUp() {
        CmdSpawnEffect();
        level++;
        xp -= xpToLevel;
        xpToLevel *= 2;
        vitality.baseValue += 2;
        dexterity.baseValue += 1;
        intelligence.baseValue += 1;
        strength.baseValue += 1;
        if (className == "Warrior") {
            strength.baseValue += 2;
        } else if (className == "Mage") {
            intelligence.baseValue += 2;
        } else {
            dexterity.baseValue += 2;
        }
        UpdateDynamicStats();
    }


    public void RegenManaAndHealth() {
        if (currentHealth < maximumHealth) {
            Heal(healthPerSecond.GetValue());
        }
        if (currentMana < maximumMana) {
            IncreaseMana(manaPerSecond.GetValue());
        }
    }


    [Command]
    private void CmdUpdateStats(int maxhHealth, int maxMana) {
        if (isLocalPlayer) {
            return;
        }
        maximumHealth = maxhHealth;
        maximumMana = maxMana;
    }

	public void UpdateDynamicStats () {
		int oldHealth = health.GetValue ();
		health.baseValue = vitality.GetValue () * 10;
		int healthDifference = health.GetValue () - oldHealth;
		currentHealth += healthDifference;
		float hps = vitality.GetValue () * 0.1f;
		healthPerSecond.baseValue = Mathf.RoundToInt (hps);
		int oldMana = mana.GetValue ();
		mana.baseValue = intelligence.GetValue () * 10;
		int manaDifference = mana.GetValue () - oldMana;
		currentMana += manaDifference;
		float mps = intelligence.GetValue () * 0.1f;
		manaPerSecond.baseValue = Mathf.RoundToInt (mps);
		maximumHealth = health.GetValue();
		maximumMana = mana.GetValue();
        CmdUpdateStats(maximumHealth, maximumMana);
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

    [Command]
    private void CmdIncreaseStats(string type, int value)
    {
        if (isLocalPlayer) {
            return;
        }
        switch (type)
        {
        case "Damage":
            damage.AddBonus(value);
            break;
        case "Vitality":
            vitality.AddBonus(value);
            UpdateDynamicStats();
            break;
        case "Dexterity":
            dexterity.AddBonus(value);
            break;
        case "Intelligence":
            intelligence.AddBonus(value);
            UpdateDynamicStats();
            break;
        case "Strength":
            strength.AddBonus(value);
            break;
        case "Armor":
            armor.AddBonus(value);
            break;
        case "Crit Chance":
            critChance.AddBonus(value);
            break;
        case "Cooldown Reduction":
            cooldownReduction.AddBonus(value);
            break;
        case "Health":
            health.AddBonus(value);
            break;
        case "Health per Second":
            healthPerSecond.AddBonus(value);
            break;
        case "Mana":
            mana.AddBonus(value);
            break;
        case "Mana per Second":
            manaPerSecond.AddBonus(value);
            break;
        }
    }

    public void EquipItem (Item item) {
		item.itemEquipped = true;
		foreach (StatBonus bonus in item.itemStats) {
			switch (bonus.statType) 
			{
			case "Damage": 
				damage.AddBonus (bonus.value);
                CmdIncreaseStats(bonus.statType, bonus.value);
                break;
			case "Vitality": 
				vitality.AddBonus (bonus.value);
				UpdateDynamicStats ();
                CmdIncreaseStats(bonus.statType, bonus.value);
                break;
			case "Dexterity": 
				dexterity.AddBonus (bonus.value);
                CmdIncreaseStats(bonus.statType, bonus.value);
                break;
			case "Intelligence": 
				intelligence.AddBonus (bonus.value);
				UpdateDynamicStats ();
                CmdIncreaseStats(bonus.statType, bonus.value);
                break;
			case "Strength": 
				strength.AddBonus (bonus.value);
                CmdIncreaseStats(bonus.statType, bonus.value);
                break;
			case "Armor": 
				armor.AddBonus (bonus.value);
                CmdIncreaseStats(bonus.statType, bonus.value);
                break;
			case "Crit Chance": 
				critChance.AddBonus (bonus.value);
                CmdIncreaseStats(bonus.statType, bonus.value);
                break;
			case "Cooldown Reduction":
                CmdIncreaseStats(bonus.statType, bonus.value);
                break;
			case "Health": 
				health.AddBonus (bonus.value);
                CmdIncreaseStats(bonus.statType, bonus.value);
                break;
			case "Health per Second": 
				healthPerSecond.AddBonus (bonus.value);
                CmdIncreaseStats(bonus.statType, bonus.value);
                break;
			case "Mana": 
				mana.AddBonus (bonus.value);
                CmdIncreaseStats(bonus.statType, bonus.value);
                break;
			case "Mana per Second": 
				manaPerSecond.AddBonus (bonus.value);
                CmdIncreaseStats(bonus.statType, bonus.value);
                break;
			}
		}
		CharacterManager.Instance.UpdateStats ();

	}

    [Command]
    private void CmdDecreaseStats(string type, int value)
    {
        if (isLocalPlayer)
        {
            return;
        }
        switch (type)
        {
            case "Damage":
                damage.RemoveBonus(value);
                break;
            case "Vitality":
                vitality.RemoveBonus(value);
                UpdateDynamicStats();
                break;
            case "Dexterity":
                dexterity.RemoveBonus(value);
                break;
            case "Intelligence":
                intelligence.RemoveBonus(value);
                UpdateDynamicStats();
                break;
            case "Strength":
                strength.RemoveBonus(value);
                break;
            case "Armor":
                armor.RemoveBonus(value);
                break;
            case "Crit Chance":
                critChance.RemoveBonus(value);
                break;
            case "Cooldown Reduction":
                cooldownReduction.RemoveBonus(value);
                break;
            case "Health":
                health.RemoveBonus(value);
                break;
            case "Health per Second":
                healthPerSecond.RemoveBonus(value);
                break;
            case "Mana":
                mana.RemoveBonus(value);
                break;
            case "Mana per Second":
                manaPerSecond.RemoveBonus(value);
                break;
        }
    }


    public void UnequipItem (Item item) {
		if (bag.Count < maximumBagSlots) {
			foreach (StatBonus bonus in item.itemStats) {
				switch (bonus.statType) 
				{
				case "Damage": 
					damage.RemoveBonus (bonus.value);
                    CmdDecreaseStats(bonus.statType, bonus.value);
                    break;
				case "Vitality": 
					vitality.RemoveBonus (bonus.value);
					UpdateDynamicStats ();
                    CmdDecreaseStats(bonus.statType, bonus.value);
                    break;
				case "Dexterity": 
					dexterity.RemoveBonus (bonus.value);
                    CmdDecreaseStats(bonus.statType, bonus.value);
                    break;
				case "Intelligence": 
					intelligence.RemoveBonus (bonus.value);
					UpdateDynamicStats ();
                    CmdDecreaseStats(bonus.statType, bonus.value);
                    break;
				case "Strength": 
					strength.RemoveBonus (bonus.value);
                    CmdDecreaseStats(bonus.statType, bonus.value);
                    break;
				case "Armor": 
					armor.RemoveBonus (bonus.value);
                    CmdDecreaseStats(bonus.statType, bonus.value);
                    break;
				case "Crit Chance": 
					critChance.RemoveBonus (bonus.value);
                    CmdDecreaseStats(bonus.statType, bonus.value);
                    break;
				case "Cooldown Reduction": 
					cooldownReduction.RemoveBonus (bonus.value);
                    CmdDecreaseStats(bonus.statType, bonus.value);
                    break;
				case "Health": 
					health.RemoveBonus (bonus.value);
                    CmdDecreaseStats(bonus.statType, bonus.value);
                    break;
				case "Health per Second": 
					healthPerSecond.RemoveBonus (bonus.value);
                    CmdDecreaseStats(bonus.statType, bonus.value);
                    break;
				case "Mana": 
					mana.RemoveBonus (bonus.value);
                    CmdDecreaseStats(bonus.statType, bonus.value);
                    break;
				case "Mana per Second":
                    CmdDecreaseStats(bonus.statType, bonus.value);
                    manaPerSecond.RemoveBonus (bonus.value);
					break;
				}
			}
			if (!item.toDestroy) {
				AddItemToBag (item);
			}
			CharacterManager.Instance.UpdateStats ();
		}
	}

	public bool GetCritted () {
		if (Random.Range (0, 100) <= critChance.GetValue ()) {
			return true;
		}
		return false;
	}

	public void TakeDamage (int amount) {
        amount -= armor.GetValue();
        if (amount > 0)
        {
            ReduceHealth(amount);
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
        if (isServer)
        {
            currentHealth += amount;
            if (currentHealth > health.GetValue())
            {
                currentHealth = health.GetValue();
            }
        }
    }

	public void ReduceHealth (int amount) {
        if (isServer)
        {
            if (currentHealth > health.GetValue())
            {
                currentHealth = health.GetValue();
            }
            currentHealth -= amount;
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

	public void IncreaseMana (int amount) {
        if (isServer)
        {
            currentMana += amount;
            if (currentMana > mana.GetValue())
            {
                currentMana = mana.GetValue();
            }
            
        }
    }

	public void ReduceMana (int amount) {
        if (isServer)
        {
            currentMana -= amount;
        }
    }

	void Die() {
		currentHealth = maximumHealth;
	}
}
