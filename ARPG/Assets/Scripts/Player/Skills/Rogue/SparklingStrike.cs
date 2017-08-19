﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class SparklingStrike : Skill {


	private SwordAttack rightSwordAttack;
	private GameObject rightSword;

	public override void SetProperties (GameObject sword) {
		rightSword = sword;
		rightSwordAttack = rightSword.GetComponent<SwordAttack> ();
		skillName = "Sparkling Strike";
		skillDescription = "You hit your enemy with a powerful strike with your blade.";
		skillIcon =  Resources.Load<Sprite> ("UI/Icons/sparklingStrike");
		manaCost = 0;
		baseDamage = 20;
		damage = baseDamage;
		cooldown = 0.8f;
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
        rightSwordAttack.SetLightDamage (baseDamage);
        rightSwordAttack.SetAttack(true, false);
	}

}
