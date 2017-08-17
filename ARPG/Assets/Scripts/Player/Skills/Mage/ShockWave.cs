using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class ShockWave : Skill {

	private GameObject shockWave;

	public override void SetProperties (Player player) {
		shockWave = player.transform.Find ("Shockwave").gameObject;
		skillName = "Shockwave";
		skillDescription = "You emit a shockwave around you that deals damage to any enemy it hits.";
		skillIcon = (Sprite) Resources.Load ("UI/shockwave");
		manaCost = 20;
		baseDamage = 15;
		damage = baseDamage;
		cooldown = 4f;
		cooldownLeft = 0f;
		onCooldown = false;
	}

	public override void Execute () {
		shockWave.SetActive (true);
		shockWave.GetComponent<ShockWaveBehaviour> ().SetDamage (damage);
	}
}
