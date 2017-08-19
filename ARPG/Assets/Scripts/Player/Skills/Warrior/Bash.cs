﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Bash : Skill {

	private SwordAttack swordAttack;
	private GameObject sword;

	public override void SetProperties (GameObject sword) {
		this.sword = sword;
		swordAttack = sword.GetComponent<SwordAttack> ();
		skillName = "Bash";
		skillDescription = "You slice through your enemy with full power.";
		skillIcon = Resources.Load <Sprite> ("UI/Icons/bash");
		manaCost = 0;
		baseDamage = 15;
		damage = baseDamage;
		cooldown = 2f;
		cooldownLeft = 0f;
		onCooldown = false;
	}


	void Update () {
		if (onCooldown) {
			cooldownLeft -= Time.deltaTime;
			HUDManager.Instance.UpdateCooldown (5, cooldownLeft, cooldown);
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
		swordAttack.SetLightDamage (baseDamage);
		swordAttack.SetAttack(true, false);
	}

}
