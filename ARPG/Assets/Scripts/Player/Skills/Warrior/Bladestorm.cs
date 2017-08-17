using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Bladestorm : Skill {

	private SwordAttack swordAttack;
	private GameObject sword;

	public override void SetProperties (GameObject sword) {
		this.sword = sword;
		swordAttack = sword.GetComponent<SwordAttack> ();
		skillName = "Bladestorm";
		skillDescription = "You rotate around yourself slicing through every enemy around you.";
		skillIcon = (Sprite) Resources.Load ("UI/bladestorm");
		manaCost = 0;
		baseDamage = 10;
		damage = baseDamage;
		cooldown = 4f;
		cooldownLeft = 0f;
		onCooldown = false;
	}

	public override void Execute () {
		swordAttack.SetHeavyDamage (baseDamage);
		swordAttack.SetAttack(false, true);
	}
}
