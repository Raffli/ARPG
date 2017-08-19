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
		skillIcon =  Resources.Load<Sprite> ("UI/Icons/bladestorm");
		manaCost = 0;
		baseDamage = 10;
		damage = baseDamage;
		cooldown = 4f;
		cooldownLeft = 0f;
		onCooldown = false;
	}

	void Update () {
		if (onCooldown) {
			cooldownLeft -= Time.deltaTime;
			HUDManager.Instance.UpdateCooldown (6, cooldownLeft, cooldown);
			if (cooldownLeft <= 0) {
				onCooldown = false;
			}
		}
	}

	public void StartCooldown () {
		onCooldown = true;
		cooldownLeft = cooldown;
	}

	public override void Execute () {
		swordAttack.SetHeavyDamage (baseDamage);
		swordAttack.SetAttack(false, true);
	}
}
