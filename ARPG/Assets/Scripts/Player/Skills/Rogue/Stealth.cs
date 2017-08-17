using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Stealth : Skill {

	private GameObject stealth;
	private Player playerStats;

	public override void SetProperties (Player player) {
		playerStats = player.GetComponent<Player> ();
		stealth = player.transform.Find ("Stealth").gameObject;
		skillName = "Stealth";
		skillDescription = "You concentrate and become invisible to enemies for a while.";
		skillIcon = (Sprite) Resources.Load ("UI/stealth");
		manaCost = 15;
		baseDamage = 0;
		damage = baseDamage;
		cooldown = 6f;
		cooldownLeft = 0f;
		onCooldown = false;
	}

	public override void CmdExecute () {
		stealth.SetActive (true);
		// add to player.damage or something
	}
}
