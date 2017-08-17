using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

	public static HUDManager Instance { get; set; }

	public GameObject hudPanel;
	private Slider xpBar;
	private Image healthPool;
	private Image manaPool;
	private GameObject skillGroup;
	private Image[] skills;
	private Image [] cooldownFills;
	private Text [] cooldownLefts;

	public Sprite notLearned;
	public Sprite healPotion;
	private Sprite manaPotion;

	void Awake () {
		xpBar = hudPanel.transform.Find ("XpBar").GetComponent<Slider> ();
		healthPool = hudPanel.transform.Find ("HealthPool").GetChild (0).GetComponent<Image> ();
		manaPool = hudPanel.transform.Find ("ManaPool").GetChild (0).GetComponent<Image> ();
		skillGroup = hudPanel.transform.Find ("Skills").gameObject;
		manaPotion = Resources.Load<Sprite> ("UI/Icons/manaPotion");
		Debug.Log (skillGroup.transform.childCount);
		skills = new Image [skillGroup.transform.childCount];
		cooldownFills = new Image[skills.Length];
		cooldownLefts = new Text[skills.Length];
		for (int i = 0; i < skillGroup.transform.childCount; i++) {
			skills [i] = skillGroup.transform.GetChild (i).GetComponent<Image> ();
			skills [i].sprite = notLearned;
			cooldownFills [i] = skills [i].transform.Find ("CooldownFill").GetComponent<Image> ();
			cooldownFills [i].fillAmount = 0f;
			cooldownLefts [i] = skills [i].transform.Find ("CooldownLeft").GetComponent<Text> ();
			cooldownLefts [i].text = "";
		}
		skills [3].sprite = healPotion;
		skills [4].sprite = manaPotion;

		if (Instance != null && Instance != this) {
			Destroy (gameObject);
		} else {
			Instance = this;
		}
	}

	public void UpdateXPBar (int currentXp, int xpToLevel) {
		xpBar.value = ((float)currentXp) / ((float) xpToLevel);
	}

	public void UpdateHP (int currentHealth, int maxHealth) {
		healthPool.fillAmount = ((float)currentHealth) / ((float)maxHealth);
	}

	public void UpdateMana (int currentMana, int maxMana) {
		manaPool.fillAmount = ((float)currentMana) / ((float)maxMana);
	}

	public void UpdateCooldown (int index, float cooldownLeft, float cooldownMax) {
		cooldownFills [index].fillAmount = cooldownLeft / cooldownMax;
		cooldownLefts [index].text = "" + (Mathf.CeilToInt(cooldownLeft));
		if (cooldownLeft <= 0) {
			cooldownFills [index].fillAmount = 0;
			cooldownLefts [index].text = "";
		}
	}

	public void AddSkillToUI (Sprite icon, int index) {
		skills [index].sprite = icon;
	}
		

}
