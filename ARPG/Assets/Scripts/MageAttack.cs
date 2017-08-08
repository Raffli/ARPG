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
	private WorldInteraction worldInteraction;

	void Start () {
		animator = GetComponent<Animator>();
		playerAgent = GetComponent<NavMeshAgent> ();
		worldInteraction = GetComponent<WorldInteraction> ();
	}

	void FixedUpdate () {
		if (Input.GetButtonDown ("Fire2")) {
			AttackSecondary ();
		} else if (Input.GetButtonDown ("Spell1")) {
			UseFirstSpell ();
		} else if (Input.GetButtonDown ("Spell2")) {
			UseSecondSpell ();
		} else if (Input.GetButtonDown ("Spell3")) {
			UseThirdSpell ();
		} else if (Input.GetButtonDown ("HealPotion")) {
			UseHealPotion ();
		} else if (Input.GetButtonDown ("ManaPotion")) {
			UseManaPotion ();
		}
	}

	public void AttackPrimary (GameObject enemy) {
		this.enemy = enemy;
		if (!primaryOnCooldown) {
			worldInteraction.SetCanInteract (false);
			primaryOnCooldown = true;
			if (animator) {
				animator.SetTrigger ("attackedPrimary"); // Animator calls CastFireball
			}
		}
	}

	public void AttackSecondary () {
		if (!secondaryOnCooldown) {
			worldInteraction.SetCanInteract (false);
			playerAgent.isStopped = true;
			secondaryOnCooldown = true;
			if (animator) {
				animator.SetTrigger ("attackedSecondary"); // Animator calls CastShockWave
			}
		}
	}

	public void UseFirstSpell () {
		Debug.Log ("First spell");
	}
	public void UseSecondSpell () {
		Debug.Log ("Second spell");
	}
	public void UseThirdSpell () {
		Debug.Log ("Third spell");
	}
	public void UseHealPotion () {
		Debug.Log ("heal potion used");
	}
	public void UseManaPotion () {
		Debug.Log ("mana potion used");
	}

	private void CastFireball () {
		Vector3 spawnPoint = spellOrigin.transform.position;
		Vector3 targetPoint = enemy.transform.position;
		Vector3 toTarget = targetPoint - spawnPoint;
		GameObject obj = Instantiate (fireBall, spawnPoint, Quaternion.LookRotation (toTarget));
		obj.GetComponent<Fireball> ().SetPlayerAgent (playerAgent);
		obj.GetComponent<Fireball> ().SetFireballDamage (primaryDamage);
		worldInteraction.SetCanInteract (true);
		StartCoroutine (ResetPrimaryAttack ());
	}

	private void CastShockWave () {
		shockWave.SetActive (true);
		shockWave.GetComponent<ShockWave> ().SetDamage (secondaryDamage);
		worldInteraction.SetCanInteract (true);
		StartCoroutine (ResetSecondaryAttack ());
	}

	private void RestartPlayerAgent () {
		playerAgent.isStopped = false;
	}

	IEnumerator ResetPrimaryAttack () {
		yield return new WaitForSeconds (cooldownPrimaryAttack);
		primaryOnCooldown = false;
	}

	IEnumerator ResetSecondaryAttack () {
		yield return new WaitForSeconds (cooldownSecondaryAttack);
		secondaryOnCooldown = false;
	}
		
}
