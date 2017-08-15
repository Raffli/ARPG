﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class SweepingLotus : MonoBehaviour, ISkill {

	public string skillName { get; set; }
	public string skillDescription { get; set; }
	public Image skillIcon { get; set; }
	public int manaCost { get; set; }
	public int baseDamage { get; set; }
	public int damage { get; set; }
	public float cooldown { get; set; }
	public float cooldownLeft { get; set; }
	public bool onCooldown { get; set; }

	private GameObject sweepingLotus;
	private Player playerStats;

	public void SetProperties () {}
	public void SetProperties (GameObject sword) {}
	public void SetProperties (GameObject leftSword, GameObject rightSword) {}

	public void SetProperties (Player player) {
		playerStats = player.GetComponent<Player> ();
		sweepingLotus = player.transform.Find ("SweepingLotus").gameObject;
		skillName = "Sweeping Lotus";
		skillDescription = "You spin with your blades creating a sweeping wind that deals damage to enemies.";
		skillIcon = (Image) Resources.Load ("UI/sweepingLotus");
		manaCost = 15;
		baseDamage = 0;
		damage = baseDamage;
		cooldown = 6f;
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
		sweepingLotus.SetActive (true);

	}

	public void Execute (NavMeshAgent playerAgent, GameObject enemy, GameObject spellOrigin) {}
	public void Execute (NavMeshAgent playerAgent, Vector3 targetPoint) {}
	public void Execute (NavMeshAgent playerAgent, GameObject enemy) {}
}
