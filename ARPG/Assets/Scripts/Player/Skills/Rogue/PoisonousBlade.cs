using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class PoisonousBlade : Skill {

	private GameObject poisonousBlade;
	private Player playerStats;

	public override void SetProperties (Player player) {
		playerStats = player.GetComponent<Player> ();
		poisonousBlade = player.transform.Find ("PoisonousBlade").gameObject;
		skillName = "Poisonous Blade";
		skillDescription = "You put poison on your blade and deal more damage.";
		skillIcon = (Sprite) Resources.Load ("UI/poisonousBlade");
		manaCost = 15;
		baseDamage = 0;
		damage = baseDamage;
		cooldown = 6f;
		cooldownLeft = 0f;
		onCooldown = false;
	}

	public override void Execute () {
		poisonousBlade.SetActive (true);
		// add to player.damage or something
	}
		
}
