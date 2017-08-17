﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class SparklingStrike : MonoBehaviour, ISkill {

	public string skillName { get; set; }
	public string skillDescription { get; set; }
	public Sprite skillIcon { get; set; }
	public int manaCost { get; set; }
	public int baseDamage { get; set; }
	public int damage { get; set; }
	public float cooldown { get; set; }
	public float cooldownLeft { get; set; }
	public bool onCooldown { get; set; }

	private SwordAttack rightSwordAttack;
	private GameObject rightSword;

	public void SetProperties () {}
	public void SetProperties (Player player) {}
	public void SetProperties (GameObject leftSword, GameObject rightSword) {}

	public void SetProperties (GameObject sword) {
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

	public void Execute () {
        rightSwordAttack.SetLightDamage (baseDamage);
        rightSwordAttack.SetAttack(true, false);
	}

	public void Execute (GameObject spellOrigin) {}
	public void Execute (NavMeshAgent playerAgent, Vector3 targetPoint) {}
	public void Execute (NavMeshAgent playerAgent, GameObject enemy) {}
	public void Execute (NavMeshAgent playerAgent, GameObject enemy, GameObject spellOrigin) {}

}
