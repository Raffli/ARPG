using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MageAttack : MonoBehaviour, IAttack {
	
	private Animator animator;
	public GameObject spellOrigin;
	public GameObject fireBall;
	public GameObject shockWave;

	public int primaryDamage;
	public int secondaryDamage;
	public float cooldownPrimaryAttack;
	public float cooldownSecondaryAttack;

	private bool primaryOnCooldown;
	private bool secondaryOnCooldown;
	private NavMeshAgent playerAgent;
	private GameObject enemy;

	void Start () {
		animator = GetComponent<Animator>();
		playerAgent = GetComponent<NavMeshAgent> ();
	}

	void FixedUpdate () {
		if (Input.GetAxis ("Fire2") > 0) {
			AttackSecondary ();
		}
	}

	public void AttackPrimary (GameObject enemy) {
		this.enemy = enemy;
		if (!primaryOnCooldown) {
			if (animator) {
				animator.SetTrigger ("attackedPrimary"); // Animator calls the function that instantiates the fireball
			}
		}
	}

	public void AttackSecondary () {
		if (animator) {
			animator.SetTrigger ("attackedSecondary");
		}
	}

	public void UseFirstSpell () {
		throw new System.NotImplementedException ();
	}
	public void UseSecondSpell () {
		throw new System.NotImplementedException ();
	}
	public void UseThirdSpell () {
		throw new System.NotImplementedException ();
	}

	private void CastFireball () {
		Vector3 spawnPoint = spellOrigin.transform.position;
		Vector3 targetPoint = enemy.transform.position;
		Vector3 toTarget = targetPoint - spawnPoint;
		GameObject obj = Instantiate (fireBall, spawnPoint, Quaternion.LookRotation (toTarget));
		obj.GetComponent<Fireball> ().SetPlayerAgent (playerAgent);
		obj.GetComponent<Fireball> ().SetFireballDamage (primaryDamage);
		primaryOnCooldown = true;
		StartCoroutine (ResetPrimaryAttack ());
	}

	private void CastShockWave () {
		shockWave.SetActive (true);
		shockWave.GetComponent<ShockWave> ().SetDamage (secondaryDamage);
	}


	IEnumerator ResetPrimaryAttack () {
		yield return new WaitForSeconds (cooldownPrimaryAttack);
		primaryOnCooldown = false;
	}
		
}
