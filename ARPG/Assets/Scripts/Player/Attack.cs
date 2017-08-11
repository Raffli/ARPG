using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : MonoBehaviour {

	private Animator animator;

	protected NavMeshAgent playerAgent;
	protected WorldInteraction worldInteraction;
	protected Player player;

	protected GameObject enemy;

	protected bool primaryOnCooldown;
	protected bool secondaryOnCooldown;
	protected bool firstSpellOnCooldown;
	protected bool secondSpellOnCooldown;
	protected bool thirdSpellOnCooldown;
	protected bool healPotionOnCooldown;
	protected bool manaPotionOnCooldown;

	protected ISkill [] skills;

	void Start () {
		animator = GetComponent<Animator>();
		playerAgent = GetComponent<NavMeshAgent> ();
		worldInteraction = GetComponent<WorldInteraction> ();
		skills = new ISkill[5]; 
		skills [0] = new Fireball ();
		skills [0].SetProperties ();
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

	public void SetSkill (ISkill skill, int index) {
		skills [index] = skill;
	}

	public void AttackPrimary (GameObject enemy) {
		if (skills [0] != null && !skills[0].onCooldown) {
			this.enemy = enemy;
			worldInteraction.SetCanInteract (false);
			skills [0].StartCooldown ();
			if (animator) {
				animator.SetTrigger ("attackedPrimary"); // Animator calls CastPrimaryAttack
			}
		}
	}

	public void AttackSecondary () {
		if (skills [1] != null && !skills[1].onCooldown) {
			worldInteraction.SetCanInteract (false);
			playerAgent.isStopped = true;
			skills [1].StartCooldown ();
			if (animator) {
				animator.SetTrigger ("attackedSecondary"); // Animator calls CastSecondaryAttack
			}
		}
	}

	public void UseFirstSpell () {
		if (skills [2] != null && skills[2].onCooldown) {
			worldInteraction.SetCanInteract (false);
			playerAgent.isStopped = true;
			skills [2].StartCooldown ();
			if (animator) {
				animator.SetTrigger ("usedFirstSpell"); // Animator calls CastFirstSpell
			}
		}
	}

	public void UseSecondSpell () {
		if (skills [3] != null && skills[3].onCooldown) {
			worldInteraction.SetCanInteract (false);
			playerAgent.isStopped = true;
			skills [3].StartCooldown ();
			if (animator) {
				animator.SetTrigger ("usedSecondSpell"); // Animator calls CastSecondSpell
			}
		}
	}

	public void UseThirdSpell () {
		if (skills [4] != null && skills[4].onCooldown) {
			worldInteraction.SetCanInteract (false);
			playerAgent.isStopped = true;
			skills [4].StartCooldown ();
			if (animator) {
				animator.SetTrigger ("usedThirdSpell"); // Animator calls CastThirdSpell
			}
		}
	}

	public void UseHealPotion () {
		Debug.Log ("heal potion used");
	}

	public void UseManaPotion () {
		Debug.Log ("mana potion used");
	}

	protected virtual void CastPrimaryAttack () {}
	protected virtual void CastSecondaryAttack () {}
	protected virtual void CastFirstSpell () {}
	protected virtual void CastSecondSpell () {}
	protected virtual void CastThirdSpell () {}

}
