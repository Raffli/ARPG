﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MageAttack : MonoBehaviour, IAttack {
	
	private Animator animator;
	public GameObject spellOrigin;
	public GameObject fireBall;
	public GameObject shockWave;

	public int lightDamage;
	public int heavyDamage;
	public float cooldownPrimaryAttack;
	public float heavyCooldown;

	private bool primaryOnCooldown;
	private NavMeshAgent playerAgent;

	void Start () {
		animator = GetComponent<Animator>();
		playerAgent = GetComponent<NavMeshAgent> ();
	}

	public void AttackPrimary (GameObject enemy) {
		if (!primaryOnCooldown) {
			Vector3 spawnPoint = spellOrigin.transform.position;
			Vector3 targetPoint = enemy.transform.position;
			Vector3 toTarget = targetPoint - spawnPoint;
			GameObject obj = Instantiate (fireBall, spawnPoint, Quaternion.LookRotation (toTarget));
			obj.GetComponent<Fireball> ().SetPlayerAgent (playerAgent);
			obj.GetComponent<Fireball> ().SetFireballDamage (lightDamage);
			primaryOnCooldown = true;
			StartCoroutine (ResetPrimaryAttack ());
		}
	}

	public void AttackSecondary (GameObject enemy) {
		throw new System.NotImplementedException ();
	}

	IEnumerator ResetPrimaryAttack () {
		yield return new WaitForSeconds (cooldownPrimaryAttack);
		primaryOnCooldown = false;
	}
		
}
