﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RougeAttack : MonoBehaviour, IAttack {

	private Animator animator;

	public int primaryDamage;
	public int secondaryDamage;

	public float cooldownPrimaryAttack;
	public float cooldownSecondaryAttack;
	public float cooldownFirstSpell;
	public float cooldownSecondSpell;
	public float cooldownThirdSpell;

	private bool primaryOnCooldown;
	private bool secondaryOnCooldown;
	private bool firstSpellOnCooldown;
	private bool secondSpellOnCooldown;
	private bool thirdSpellOnCooldown;

	private NavMeshAgent playerAgent;
	private GameObject enemy;
	private WorldInteraction worldInteraction;

    public GameObject leftSword;
    public GameObject rightSword;

    void Start () {
        animator = GetComponent<Animator>();
		playerAgent = GetComponent<NavMeshAgent> ();
		worldInteraction = GetComponent<WorldInteraction> ();
		leftSword.GetComponent<SwordAttack>().SetLightDamage(primaryDamage);
        leftSword.GetComponent<SwordAttack>().SetHeavyDamage(secondaryDamage);
		rightSword.GetComponent<SwordAttack>().SetLightDamage(primaryDamage);
        rightSword.GetComponent<SwordAttack>().SetHeavyDamage(secondaryDamage);
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
			leftSword.GetComponent<SwordAttack>().SetAttack(true, false);
			if (animator) {				
				animator.SetTrigger ("attackedPrimary"); 
			}
			StartCoroutine (ResetPrimaryAttack ());
		}
	}

	public void AttackSecondary () {
		if (!secondaryOnCooldown) {
			worldInteraction.SetCanInteract (false);
			playerAgent.isStopped = true;
			secondaryOnCooldown = true;
			rightSword.GetComponent<SwordAttack>().SetAttack(false, true);
			if (animator) {		
				animator.SetTrigger ("attackedSecondary"); 
			}
			StartCoroutine (ResetSecondaryAttack ());
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

	private void RestartPlayerAgent () {
		Debug.Log ("restart player agent");
		playerAgent.isStopped = false;
		worldInteraction.SetCanInteract (true);
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
