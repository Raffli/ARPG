using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class WarCry : Skill {

	private GameObject warCry;
	private Player playerStats;

	public override void SetProperties (Player player) {
		playerStats = player.GetComponent<Player> ();
		warCry = player.transform.Find ("WarCry").gameObject;
		skillName = "War Cry";
		skillDescription = "You cry out gaining strength and health for x seconds.";
		skillIcon = (Sprite) Resources.Load ("UI/warcry");
		manaCost = 15;
		baseDamage = 0;
		damage = baseDamage;
		cooldown = 6f;
		cooldownLeft = 0f;
		onCooldown = false;
	}

	public override void Execute () {
		warCry.SetActive (true);
	}
}
