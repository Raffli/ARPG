﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class SparklingStrike : MonoBehaviour, ISkill {

	public string skillName { get; set; }
	public string skillDescription { get; set; }
	public Image skillIcon { get; set; }
	public int manaCost { get; set; }
	public int baseDamage { get; set; }
	public int damage { get; set; }
	public float cooldown { get; set; }
	public float cooldownLeft { get; set; }
	public bool onCooldown { get; set; }

	private SwordAttack leftSwordAttack;
	private GameObject leftSword;
	//private SwordAttack rightSwordAttack;
	//private GameObject rightSword;

	public void SetProperties () {}
	public void SetProperties (Player player) {}

	public void SetProperties (GameObject sword) {
		leftSword = sword;
		leftSwordAttack = leftSword.GetComponent<SwordAttack> ();
		//rightSwordAttack = rightSword.GetComponent<SwordAttack> ();
		skillName = "Sparkling Strike";
		skillDescription = "You hit your enemy with a powerful strike with your blade.";
		skillIcon = (Image) Resources.Load ("UI/sparklingStrike");
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
		leftSwordAttack.SetLightDamage (baseDamage);
		leftSwordAttack.SetAttack(true, false);
	}

	public void Execute (NavMeshAgent playerAgent, Vector3 targetPoint) {}
	public void Execute (NavMeshAgent playerAgent, GameObject enemy) {}
	public void Execute (NavMeshAgent playerAgent, GameObject enemy, GameObject spellOrigin) {}

}
