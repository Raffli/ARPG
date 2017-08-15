using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

	public GameObject hudPanel;
	private Slider xpBar;
	private Image healthPool;
	private Image manaPool;
	//private GameObject skillGroup;
	//private Image[] skills;

	private Player player;
	//private Attack attack;
	//private Image notLearned;

	// Use this for initialization
	void Start () {
		player = GetComponent<Player> ();
		//attack = GetComponent<Attack> ();
		//notLearned = (Image) Resources.Load ("UI/Icons/notLearned");
		xpBar = hudPanel.transform.Find ("XpBar").GetComponent<Slider> ();
		healthPool = hudPanel.transform.Find ("HealthPool").GetChild (0).GetComponent<Image> ();
		manaPool = hudPanel.transform.Find ("ManaPool").GetChild (0).GetComponent<Image> ();
		//skills = new Image [skillGroup.transform.childCount];
		/*for (int i = 0; i < skillGroup.transform.childCount; i++) {
			skills [i] = skillGroup.transform.GetChild (i).GetComponent<Image> ();
			if (attack.skills [i] != null) {
				skills [i].sprite = attack.skills [i].skillIcon.sprite;
			} else {
				skills [i].sprite = notLearned.sprite;
			}
		}*/

	}
	
	// Update is called once per frame
	void Update () {
		xpBar.value = ((float) player.xp) / ((float) player.xpToLevel);
		healthPool.fillAmount = ((float) player.currentHealth) / ((float) player.maxHealth);
		manaPool.fillAmount = ((float) player.currentMana) / ((float) player.maximumMana);
	}

	public void AddSkillToUI (Image icon, int index) {

	}
}
