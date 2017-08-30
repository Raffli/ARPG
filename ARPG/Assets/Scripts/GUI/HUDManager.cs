using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.EventSystems;

public class HUDManager : NetworkBehaviour {

	public static HUDManager Instance { get; set; }

	public GameObject hudPanel;
	private Slider xpBar;
	private Image healthPool;
	private Image manaPool;
	private GameObject skillGroup;
	private Image[] skills;
	private Image [] cooldownFills;
	private Text [] cooldownLefts;

	private Sprite notLearned;
	private Sprite healPotion;
	private Sprite manaPotion;

	public Texture2D normalCursor;
	public Texture2D attackCursor;
	public Texture2D lootCursor;
	public Texture2D speechCursor;
	private bool lootCursorSet;
	private Ray ray;

	void Awake () {
        DontDestroyOnLoad(transform.gameObject);

		if (Instance != null && Instance != this) {
			Destroy (gameObject);
		} else {
			Instance = this;
		}

        xpBar = hudPanel.transform.Find ("XpBar").GetComponent<Slider> ();
		healthPool = hudPanel.transform.Find ("HealthPool").GetChild (0).GetComponent<Image> ();
		manaPool = hudPanel.transform.Find ("ManaPool").GetChild (0).GetComponent<Image> ();
		skillGroup = hudPanel.transform.Find ("Skills").gameObject;
		notLearned = Resources.Load<Sprite> ("UI/Icons/notLearned");
		healPotion = Resources.Load<Sprite> ("UI/Icons/healPotion");
		manaPotion = Resources.Load<Sprite> ("UI/Icons/manaPotion");
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

		SetCursorTexture (normalCursor);
	}

    void Update()
    {
        if (isLocalPlayer) {
            ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
				if (hit.collider.gameObject.tag.Equals ("Enemy")) {
					SetCursorTexture (attackCursor);
				} else if (hit.collider.gameObject.tag.Equals ("NPC")) {
					SetCursorTexture (speechCursor);
				} else {
					if (lootCursorSet) {
						SetCursorTexture (lootCursor);
					} else {
						SetCursorTexture (normalCursor);
					}
				}
			}
        }
	}

	private void SetCursorTexture (Texture2D tex) {
		Cursor.SetCursor (tex, Vector2.zero, CursorMode.Auto);
	}

	public void SetLootCursor (bool onLoot) {
		lootCursorSet = onLoot;
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
