using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaPotion : MonoBehaviour {

	private int manaAmount;

	public Image icon { get; set; }
	public string description { get; set; }
	public int price { get; set; }
	public float cooldown { get; set; }
	public float cooldownLeft { get; set; }
	public bool onCooldown { get; set; }

	private GameObject manaPotionEffect;

	public void SetProperties () {
		manaAmount = 50;
		price = 50;
		icon = (Image) Resources.Load ("UI/manaPotion");
		description = "Potion with refunding magical energy.";
		cooldown = 5f;
		cooldownLeft = 0f;
		onCooldown = false;
	}

	void Update () {
		if (onCooldown) {
			cooldownLeft -= Time.deltaTime;
			if (cooldownLeft <= 0) {
				onCooldown = false;
			}
		}
	}

	public void Use (Player player) {
		onCooldown = true;
		player.currentMana += manaAmount;
		if (player.currentMana > player.maximumMana) {
			player.currentMana = player.maximumMana;
		}
		manaPotionEffect = player.transform.Find ("ManaPotionEffect").gameObject;
		manaPotionEffect.SetActive (true);
	}
}
