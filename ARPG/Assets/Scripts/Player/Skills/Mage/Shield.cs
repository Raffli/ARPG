using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Shield : Skill {
	
	private GameObject shield;
	private Player playerStats;

	public override void SetProperties(Player player) {

		playerStats = player.GetComponent<Player> ();
		shield = player.transform.Find ("Shield").gameObject;
		skillName = "Protective Aura";
		skillDescription = "You concentrate your magic energy to generate a protective aura taht shields you.";
		skillIcon = (Sprite) Resources.Load ("UI/protectingAura");
		manaCost = 15;
		baseDamage = 0;
		damage = baseDamage;
		cooldown = 6f;
		cooldownLeft = 0f;
		onCooldown = false;
	}

	public override void CmdExecute() {
		shield.SetActive (true);
		// add to player.armor or something
	}
}
