using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

	public GameObject hudPanel;
	private Slider xpBar;
	private Image healthPool;
	private Image manaPool;

	private Player player;

	// Use this for initialization
	void Start () {
		xpBar = hudPanel.transform.Find ("XpBar").GetComponent<Slider> ();
		healthPool = hudPanel.transform.Find ("HealthPool").GetChild (0).GetComponent<Image> ();
		manaPool = hudPanel.transform.Find ("ManaPool").GetChild (0).GetComponent<Image> ();
		player = GetComponent<Player> ();
	}
	
	// Update is called once per frame
	void Update () {
		xpBar.value = ((float) player.xp) / ((float) player.xpToLevel);
		healthPool.fillAmount = ((float) player.currentHealth) / ((float) player.maxHealth);
		manaPool.fillAmount = ((float) player.currentMana) / ((float) player.maximumMana);
	}
}
