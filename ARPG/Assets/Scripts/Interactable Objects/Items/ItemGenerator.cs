using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour {

	public static ItemGenerator Instance;

	private List <ItemBasis> headItems;
	private List <ItemBasis> amulets;
	private List <ItemBasis> chestItems;
	private List <ItemBasis> gloves;
	private List <ItemBasis> swords;
	private List <ItemBasis> shields;
	private List <ItemBasis> dagger;
	private List <ItemBasis> staves;
	private List <ItemBasis> charms;
	private List <ItemBasis> rings;
	private List <ItemBasis> pants;
	private List <ItemBasis> shoes;

	private List <string> statTypes;

	void Start () {
		DontDestroyOnLoad (transform.gameObject);

		if (Instance != null && Instance != this) {
			Destroy (gameObject);
		} else {
			Instance = this;
		}

		statTypes = new List<string> ();
		statTypes.Add ("Vitality");
		statTypes.Add ("Dexterity");
		statTypes.Add ("Strength");
		statTypes.Add ("Intelligence");
		statTypes.Add ("Armor");
		statTypes.Add ("Damage");
		statTypes.Add ("Crit Chance");
		statTypes.Add ("Cooldown Reduction");
		statTypes.Add ("Health");
		statTypes.Add ("Health per Second");
		statTypes.Add ("Mana");
		statTypes.Add ("Mana per Second");

		headItems = new List <ItemBasis> ();
		CreateHeadItemBasis ();
		amulets = new List <ItemBasis> ();
		CreateAmuletItemBasis ();
		chestItems = new List <ItemBasis> ();
		CreateChestItemBasis ();
		gloves = new List <ItemBasis> ();
		CreateGlovesItemBasis ();
		swords = new List <ItemBasis> ();
		CreateSwordItemBasis ();
		shields = new List <ItemBasis> ();
		CreateShieldItemBasis ();
		dagger = new List <ItemBasis> ();
		CreateDaggerItemBasis ();
		staves = new List <ItemBasis> ();
		CreateStaffItemBasis ();
		charms = new List <ItemBasis> ();
		CreateCharmItemBasis ();
		rings = new List <ItemBasis> ();
		CreateRingItemBasis ();
		pants = new List <ItemBasis> ();
		CreatePantsItemBasis ();
		shoes = new List <ItemBasis> ();
		CreateShoesItemBasis ();

	}

	public Item GenerateRandomItem (int playerLevel, string playerClass) {
		Item newItem = new Item ("", null, Item.ItemPosition.Amulet);

		string itemName = "";
		Sprite itemSprite = null;
		Item.ItemPosition itemPosition = Item.ItemPosition.Amulet;
		string itemDescription = "";
		List <StatBonus> itemStats = new List<StatBonus> ();
		int randomStatsCount = 0;

		int randomizer = Random.Range (0, 100);
		Debug.Log ("randomizer " + randomizer);
		if (randomizer < 10) { // Range for Head Items - Primary Stat Armor
			itemPosition = Item.ItemPosition.Head;
			int randomHead = Random.Range (0, headItems.Count);
			itemName = headItems [randomHead].itemName;
			itemSprite = headItems [randomHead].itemSprite;
			itemDescription = headItems [randomHead].itemDescription;
			itemStats.Add (new StatBonus (playerLevel * headItems[randomHead].multiplier, "Armor"));
		} else if (randomizer < 20) { // Range for Amulet Items - all stats random
			randomStatsCount += 1;
			itemPosition = Item.ItemPosition.Amulet;
			int randomHead = Random.Range (0, amulets.Count);
			itemName = amulets [randomHead].itemName;
			itemSprite = amulets [randomHead].itemSprite;
			itemDescription = amulets [randomHead].itemDescription;
		} else if (randomizer < 30) { // Range for Chest Items - Primary Stat Armor
			itemPosition = Item.ItemPosition.Chest;
			int randomHead = Random.Range (0, (chestItems.Count));
			itemName = chestItems [randomHead].itemName;
			itemSprite = chestItems [randomHead].itemSprite;
			itemDescription = chestItems [randomHead].itemDescription;
			itemStats.Add (new StatBonus (playerLevel * chestItems[randomHead].multiplier, "Armor"));
		} else if (randomizer < 40) { // Range for Gloves Items - Primary Stat Armor
			itemPosition = Item.ItemPosition.Gloves;
			int randomHead = Random.Range (0, (gloves.Count));
			itemName = gloves [randomHead].itemName;
			itemSprite = gloves [randomHead].itemSprite;
			itemDescription = gloves [randomHead].itemDescription;
			itemStats.Add (new StatBonus (playerLevel * gloves[randomHead].multiplier, "Armor"));
		} else if (randomizer < 50) { // Range for Primary Items - Primary Stat Damage - kind of weapon depends on PlayerClass
			List <ItemBasis> primary;
			if (playerClass == "Warrior") {
				primary = swords;
			} else if (playerClass == "Mage") {
				primary = staves;
			} else {
				primary = dagger;
			}
			itemPosition = Item.ItemPosition.Primary;
			int randomHead = Random.Range (0, (primary.Count));
			itemName = primary [randomHead].itemName;
			itemSprite = primary [randomHead].itemSprite;
			itemDescription = primary [randomHead].itemDescription;
			itemStats.Add (new StatBonus (playerLevel * primary[randomHead].multiplier, "Damage"));
		} else if (randomizer < 60) { // Range for Secondary Items - Primary Stat Damage - kind of item depends on PlayerClass
			List <ItemBasis> secondary;
			if (playerClass == "Warrior") {
				secondary = shields;
			} else if (playerClass == "Mage") {
				secondary = charms;
			} else {
				secondary = dagger;
			}
			itemPosition = Item.ItemPosition.Secondary;
			int randomHead = Random.Range (0, (secondary.Count));
			itemName = secondary [randomHead].itemName;
			itemSprite = secondary [randomHead].itemSprite;
			itemDescription = secondary [randomHead].itemDescription;
			itemStats.Add (new StatBonus (playerLevel * secondary[randomHead].multiplier, "Damage"));
		} else if (randomizer < 70) { // Range for Ring1 Items - all stats random
			randomStatsCount += 1;
			itemPosition = Item.ItemPosition.Ring1;
			int randomHead = Random.Range (0, (rings.Count));
			itemName = rings [randomHead].itemName;
			itemSprite = rings [randomHead].itemSprite;
			itemDescription = rings [randomHead].itemDescription;
		} else if (randomizer < 80) { // Range for Ring2 Items - all stats random
			randomStatsCount += 1;
			itemPosition = Item.ItemPosition.Ring2;
			int randomHead = Random.Range (0, (rings.Count));
			itemName = rings [randomHead].itemName;
			itemSprite = rings [randomHead].itemSprite;
			itemDescription = rings [randomHead].itemDescription;
		} else if (randomizer < 90) { // Range for Pants Items - Primary Stat Armor
			itemPosition = Item.ItemPosition.Pants;
			int randomHead = Random.Range (0, (pants.Count));
			itemName = pants [randomHead].itemName;
			itemSprite = pants [randomHead].itemSprite;
			itemDescription = pants [randomHead].itemDescription;
			itemStats.Add (new StatBonus (playerLevel * pants[randomHead].multiplier, "Armor"));
		} else { // Range for Shoes Items - Primary Stat Armor
			itemPosition = Item.ItemPosition.Shoes;
			int randomHead = Random.Range (0, (shoes.Count));
			itemName = shoes [randomHead].itemName;
			itemSprite = shoes [randomHead].itemSprite;
			itemDescription = shoes [randomHead].itemDescription;
			itemStats.Add (new StatBonus (playerLevel * shoes[randomHead].multiplier, "Armor"));
		} 

		// Normal (1 more stat), Magic ( 2 more stats), Epic (3 more stats), Legendary (4 more stats)
		int randomItemState = Random.Range (0, 100);
		Item.ItemState itemState = Item.ItemState.Normal;
		if (randomItemState < 30) {
			randomStatsCount += 1;
			itemState = Item.ItemState.Normal;
		} else if (randomItemState < 55) {
			randomStatsCount += 2;
			itemState = Item.ItemState.Magic;
		} else if (randomItemState < 80) {
			itemState = Item.ItemState.Epic;
			randomStatsCount += 3;
		} else {
			itemState = Item.ItemState.Legendary;
			randomStatsCount += 4;
		}
		for (int i = 0; i < randomStatsCount; i++) {
			int randomStat = Random.Range (0, statTypes.Count);
			int randomStatValue = Random.Range (1 + 10 * (playerLevel - 1), 10 * playerLevel);
			itemStats.Add (new StatBonus (randomStatValue, statTypes[randomStat]));
		}

		newItem.name = itemName;
		newItem.sprite = itemSprite;
		newItem.itemPosition = itemPosition;
		newItem.itemDescription = itemDescription;
		newItem.itemStats = itemStats;
		newItem.itemState = itemState;
		return newItem;
	}

	private void CreateHeadItemBasis () {
		headItems.Add (new ItemBasis ("Adept Hood", Resources.Load<Sprite> ("UI/Icons/Items/Head/clothHelmet"), "Lightweight Hood", 1));
		headItems.Add (new ItemBasis ("Leathered Hood", Resources.Load <Sprite> ("UI/Icons/Items/Head/leatherHood"), "Hood made of Leather", 2));
		headItems.Add (new ItemBasis ("Chain Helmet", Resources.Load<Sprite> ("UI/Icons/Items/Head/chainHelmet"), "Helmet made of Chains", 3));
		headItems.Add (new ItemBasis ("Knight's Helmet", Resources.Load<Sprite> ("UI/Icons/Items/Head/helmetOfTheKnight"), "Heavy Helmet", 4));
	}

	private void CreateAmuletItemBasis () {
		amulets.Add (new ItemBasis ("Amulet of Sapphire", Resources.Load<Sprite> ("UI/Icons/Items/Amulets/saphireAmulet"), "Amulet made of sapphires", 1));
		amulets.Add (new ItemBasis ("Amulet of Diamonds", Resources.Load<Sprite> ("UI/Icons/Items/Amulets/diamondAmulet"), "Amulet made of diamonds", 2));
		amulets.Add (new ItemBasis ("Amulet of Luxury", Resources.Load<Sprite> ("UI/Icons/Items/Amulets/luxuryAmulet"), "Amulet made of all kinds of precious stones", 3));
	}

	private void CreateChestItemBasis () {
		chestItems.Add (new ItemBasis ("Common Robes", Resources.Load<Sprite> ("UI/Icons/Items/Chest/magesRobes"), "Simple robes made of cloth", 1));
		chestItems.Add (new ItemBasis ("Rangers Armor", Resources.Load<Sprite> ("UI/Icons/Items/Chest/rangersArmor"), "Light Leather armors", 2));
		chestItems.Add (new ItemBasis ("Thiefs Armor", Resources.Load<Sprite> ("UI/Icons/Items/Chest/thiefsArmor"), "Light Leather armor", 2));
		chestItems.Add (new ItemBasis ("Fiery Armor", Resources.Load<Sprite> ("UI/Icons/Items/Chest/fieryArmor"), "Light Leather armor", 2));
		chestItems.Add (new ItemBasis ("Poisonous Armor", Resources.Load<Sprite> ("UI/Icons/Items/Chest/poisonousArmor"), "Light leather armor", 2));
		chestItems.Add (new ItemBasis ("Sapphire Armor", Resources.Load<Sprite> ("UI/Icons/Items/Chest/sapphireArmor"), "Heavy sapphire plated armor", 3));
		chestItems.Add (new ItemBasis ("Silver Armor", Resources.Load<Sprite> ("UI/Icons/Items/Chest/silverArmor"), "Heavy silver plated armor", 3));
		chestItems.Add (new ItemBasis ("Golden Armor", Resources.Load<Sprite> ("UI/Icons/Items/Chest/goldenArmor"), "Heavy gold plated armor", 4));
		chestItems.Add (new ItemBasis ("Draconian Armor", Resources.Load<Sprite> ("UI/Icons/Items/Chest/draconianArmor"), "Heavy armor made from dragon bones", 5));
	}

	private void CreateGlovesItemBasis () {
		gloves.Add (new ItemBasis ("Linen Gloves", Resources.Load<Sprite> ("UI/Icons/Items/Gloves/magesGloves"), "Lightweight Gloves", 1));
		gloves.Add (new ItemBasis ("Leather Gloves", Resources.Load <Sprite> ("UI/Icons/Items/Gloves/leatherGloves"), "Gloves made of Leather", 2));
		gloves.Add (new ItemBasis ("Chain Gloves", Resources.Load<Sprite> ("UI/Icons/Items/Gloves/chainGloves"), "Gloves made of Chains", 3));
		gloves.Add (new ItemBasis ("Steel Gloves", Resources.Load<Sprite> ("UI/Icons/Items/Gloves/steelGloves"), "Gloves made of Steel", 4));
	}

	private void CreateSwordItemBasis () {
		swords.Add (new ItemBasis ("Sword of Swiftness", Resources.Load<Sprite> ("UI/Icons/Items/Swords/swiftySword"), "One handed sword", 1));
		swords.Add (new ItemBasis ("Sword of Lightning", Resources.Load <Sprite> ("UI/Icons/Items/Swords/lightningSword"), "One handed sword", 2));
		swords.Add (new ItemBasis ("Sword of Flames", Resources.Load<Sprite> ("UI/Icons/Items/Swords/flamingSword"), "One handed sword", 3));
		swords.Add (new ItemBasis ("Draconian Sword", Resources.Load<Sprite> ("UI/Icons/Items/Swords/draconianSword"), "Mythical one handed sword", 4));
	}

	private void CreateShieldItemBasis () {
		shields.Add (new ItemBasis ("Little Buckler", Resources.Load<Sprite> ("UI/Icons/Items/Shields/buckler"), "Small Shield", 1));
		shields.Add (new ItemBasis ("Steel Shield", Resources.Load <Sprite> ("UI/Icons/Items/Shields/steelShield"), "Big Strong Shield", 2));
	}

	private void CreateDaggerItemBasis () {
		dagger.Add (new ItemBasis ("Hunters Dagger", Resources.Load<Sprite> ("UI/Icons/Items/Dagger/huntersDagger"), "Simple dagger", 1));
		dagger.Add (new ItemBasis ("Flaming Dagger", Resources.Load<Sprite> ("UI/Icons/Items/Dagger/flamingDagger"), "Strong dagger", 2));
		dagger.Add (new ItemBasis ("Icy Dagger", Resources.Load<Sprite> ("UI/Icons/Items/Dagger/icyDagger"), "Strong dagger", 2));
		dagger.Add (new ItemBasis ("Lightning Dagger", Resources.Load<Sprite> ("UI/Icons/Items/Dagger/lightningDagger"), "Strong dagger", 2));
		dagger.Add (new ItemBasis ("Poisonous Dagger", Resources.Load<Sprite> ("UI/Icons/Items/Dagger/poisonousDagger"), "Strong dagger", 2));
		dagger.Add (new ItemBasis ("Tribal Dagger", Resources.Load<Sprite> ("UI/Icons/Items/Dagger/tribalDagger"), "Strong dagger", 2));
		dagger.Add (new ItemBasis ("Vampire Dagger", Resources.Load<Sprite> ("UI/Icons/Items/Dagger/vampireDagger"), "Strong dagger", 2));
		dagger.Add (new ItemBasis ("Daemonic Dagger", Resources.Load<Sprite> ("UI/Icons/Items/Dagger/daemonicDagger"), "Mythical dagger", 4));
	}

	private void CreateStaffItemBasis () {
		staves.Add (new ItemBasis ("Staff of Poison", Resources.Load<Sprite> ("UI/Icons/Items/Staffs/staffOfPoison"), "One handed staff", 1));
		staves.Add (new ItemBasis ("Staff of Lightning", Resources.Load <Sprite> ("UI/Icons/Items/Staffs/staffOfLightning"), "One handed staff", 2));
		staves.Add (new ItemBasis ("Staff of Fire", Resources.Load<Sprite> ("UI/Icons/Items/Staffs/staffOfFire"), "One handed staff", 3));
		staves.Add (new ItemBasis ("Staff of the Mind", Resources.Load<Sprite> ("UI/Icons/Items/Staffs/staffOfMind"), "Mythical one handed staff", 4));
	}

	private void CreateCharmItemBasis () {
		charms.Add (new ItemBasis ("Charm of Life", Resources.Load<Sprite> ("UI/Icons/Items/Charms/lifeCharm"), "Simple Charm", 1));
		charms.Add (new ItemBasis ("Charm of Light", Resources.Load <Sprite> ("UI/Icons/Items/Charms/lightCharm"), "Great Charm", 2));
		charms.Add (new ItemBasis ("Charm of the Viper", Resources.Load<Sprite> ("UI/Icons/Items/Charms/viperCharm"), "Rare Charm", 3));
		charms.Add (new ItemBasis ("Charm of the Undead", Resources.Load<Sprite> ("UI/Icons/Items/Charms/undeadCharm"), "Mythical Charm", 4));
	}

	private void CreateRingItemBasis () {
		rings.Add (new ItemBasis ("Ruby Ring", Resources.Load<Sprite> ("UI/Icons/Items/Rings/ringOfVitality"), "Silver Ring with ruby stone", 1));
		rings.Add (new ItemBasis ("Sunstone Ring", Resources.Load <Sprite> ("UI/Icons/Items/Rings/ringOfStrength"), "Silver Ring with sunstone stone", 1));
		rings.Add (new ItemBasis ("Aquamarine Ring", Resources.Load<Sprite> ("UI/Icons/Items/Rings/ringOfIntelligence"), "Silver Ring with aquamarine stone", 1));
		rings.Add (new ItemBasis ("Amethyst Ring", Resources.Load<Sprite> ("UI/Icons/Items/Rings/ringOfDexterity"), "Silver Ring with amethyst stone", 1));
	}

	private void CreatePantsItemBasis () {
		pants.Add (new ItemBasis ("Linen Skirt", Resources.Load<Sprite> ("UI/Icons/Items/Pants/magesPants"), "Lightweight Pants", 1));
		pants.Add (new ItemBasis ("Leather Pants", Resources.Load <Sprite> ("UI/Icons/Items/Pants/leatherPants"), "Pants made of Leather", 2));
		pants.Add (new ItemBasis ("Chain Pants", Resources.Load<Sprite> ("UI/Icons/Items/Pants/chainedPants"), "Pants made of Chains", 3));
		pants.Add (new ItemBasis ("Steel Pants", Resources.Load<Sprite> ("UI/Icons/Items/Pants/strongPants"), "Pants made of Steel", 4));
	}

	private void CreateShoesItemBasis () {
		shoes.Add (new ItemBasis ("Ruby Shoes", Resources.Load<Sprite> ("UI/Icons/Items/Shoes/bootsOfVitality"), "Boots with a ruby", 1));
		shoes.Add (new ItemBasis ("Sunstone Shoes", Resources.Load <Sprite> ("UI/Icons/Items/Shoes/bootsOfStrength"), "Boots with a sunstone", 1));
		shoes.Add (new ItemBasis ("Aquamarine Shoes", Resources.Load<Sprite> ("UI/Icons/Items/Shoes/bootsOfIntelligence"), "Boots with an aquamarine", 1));
		shoes.Add (new ItemBasis ("Amethyst Shoes", Resources.Load<Sprite> ("UI/Icons/Items/Shoes/bootsOfDexterity"), "Boots with an amethyst", 1));
	}
	

}
