using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour {

	public static CharacterManager Instance { get; set; }

	public GameObject characterPanel;

	private Text playerLevel, playerName, playerClass;

	private Transform statsGroup;
	private Text vitality, intelligence, dexterity, strength, armor, damage, mana, health, mps, hps, critChance, cdr;

	private Transform skillsGroup;
	public Image primaryIcon, secondaryIcon, firstSpellIcon, secondSpellIcon, thirdSpellIcon;
	public Text primary, secondary, firstSpell, secondSpell, thirdSpell;

	public Player player { get; set; }

	void Awake () {
		DontDestroyOnLoad (transform.gameObject);

		if (Instance != null && Instance != this) {
			Destroy (gameObject);
		} else {
			Instance = this;
		}

		playerLevel = characterPanel.transform.Find ("PlayerLevel").GetComponent<Text> ();
		playerName = characterPanel.transform.Find ("PlayerName").GetComponent<Text> ();
		playerClass = characterPanel.transform.Find ("PlayerClass").GetComponent<Text> ();

		statsGroup = characterPanel.transform.Find ("Stats");
		vitality = statsGroup.Find ("Vitality").GetChild (0).GetComponent<Text> ();
		intelligence = statsGroup.Find ("Intelligence").GetChild (0).GetComponent<Text> ();
		strength = statsGroup.Find ("Strength").GetChild (0).GetComponent<Text> ();
		dexterity = statsGroup.Find ("Dexterity").GetChild (0).GetComponent<Text> ();
		armor = statsGroup.Find ("Armor").GetChild (0).GetComponent<Text> ();
		damage = statsGroup.Find ("Damage").GetChild (0).GetComponent<Text> ();
		mana = statsGroup.Find ("Mana").GetChild (0).GetComponent<Text> ();
		health = statsGroup.Find ("Health").GetChild (0).GetComponent<Text> ();
		mps = statsGroup.Find ("MPS").GetChild (0).GetComponent<Text> ();
		hps = statsGroup.Find ("HPS").GetChild (0).GetComponent<Text> ();
		critChance = statsGroup.Find ("CritChance").GetChild (0).GetComponent<Text> ();
		cdr = statsGroup.Find ("CooldownReduction").GetChild (0).GetComponent<Text> ();

		skillsGroup = characterPanel.transform.Find ("Skills");
		primaryIcon = skillsGroup.Find ("Primary").GetComponent<Image> ();
		primary = skillsGroup.Find ("Primary").transform.GetChild (0).GetComponent<Text> ();
		secondaryIcon = skillsGroup.Find ("Secondary").GetComponent<Image> ();
		secondary = skillsGroup.Find ("Secondary").transform.GetChild (0).GetComponent<Text> ();
		firstSpellIcon = skillsGroup.Find ("First").GetComponent<Image> ();
		firstSpell = skillsGroup.Find ("First").transform.GetChild (0).GetComponent<Text> ();
		secondSpellIcon = skillsGroup.Find ("Second").GetComponent<Image> ();
		secondSpell = skillsGroup.Find ("Second").transform.GetChild (0).GetComponent<Text> ();
		thirdSpellIcon = skillsGroup.Find ("Third").GetComponent<Image> ();
		thirdSpell = skillsGroup.Find ("Third").transform.GetChild (0).GetComponent<Text> ();

		PlayerEventHandler.OnPlayerLevelUp += UpdateLevel;
	}

	void Update () {
		if (Input.GetButtonDown ("Character")) {
			characterPanel.SetActive (!characterPanel.activeSelf);
		}
	}

	public void AddSkillToUI (int index, Sprite icon, string name) {
		switch (index) {
		case 0:
			primaryIcon.sprite = icon;
			primaryIcon.enabled = true;
			primary.text = name;
			break;
		case 1:
			secondaryIcon.sprite = icon;
			secondaryIcon.enabled = true;
			secondary.text = name;
			break;
		case 2:
			firstSpellIcon.sprite = icon;
			firstSpellIcon.enabled = true;
			firstSpell.text = name;
			break;
		case 3:
			secondSpellIcon.sprite = icon;
			secondSpellIcon.enabled = true;
			secondSpell.text = name;
			break;
		case 4:
			thirdSpellIcon.sprite = icon;
			thirdSpellIcon.enabled = true;
			thirdSpell.text = name;
			break;
		}
	}

	private void UpdateLevel (int newLevel) {
		playerLevel.text = "Level " + newLevel;
	}

	public void SetNameAndClass (string playerName, string className) {
		this.playerName.text = playerName;
		playerClass.text = className;
	}

	public void UpdateStats () {
		vitality.text = player.vitality.GetValue ().ToString();
		intelligence.text = player.intelligence.GetValue ().ToString();
		strength.text = player.strength.GetValue ().ToString();
		dexterity.text = player.dexterity.GetValue ().ToString();
		armor.text = player.armor.GetValue ().ToString();
		damage.text = player.damage.GetValue ().ToString();
		mana.text = player.mana.GetValue ().ToString();
		health.text = player.health.GetValue ().ToString();
		mps.text = player.manaPerSecond.GetValue ().ToString();
		hps.text = player.healthPerSecond.GetValue ().ToString();
		critChance.text = player.critChance.GetValue ().ToString();
		cdr.text = player.cooldownReduction.GetValue ().ToString();
	}
		
}
