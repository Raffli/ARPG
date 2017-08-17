using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class FireFissure : Skill {

	private GameObject fireFissure;
	private Player playerStats;

	public override void SetProperties (Player player) {
		playerStats = player.GetComponent<Player> ();
		skillName = "Fire Fissure";
		skillDescription = "You smash the ground causing the ground to erupt in fire.";
		skillIcon = (Sprite) Resources.Load ("UI/fireFissure");
		manaCost = 15;
		baseDamage = 20;
		damage = baseDamage;
		cooldown = 6f;
		cooldownLeft = 0f;
		onCooldown = false;
	}

	public override void Execute (GameObject spellOrigin) {
        GameObject fireFissure = (GameObject)Resources.Load("Skills/FireFissure");
        GameObject obj = Instantiate(fireFissure, spellOrigin.transform.position, spellOrigin.transform.rotation);
        obj.GetComponent<FireFissureBehaviour>().SetFireFissureDamage(damage);
    }
}
