using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Bladestorm : MonoBehaviour, ISkill {

	public string skillName { get; set; }
	public string skillDescription { get; set; }
	public Sprite skillIcon { get; set; }
	public int manaCost { get; set; }
	public int baseDamage { get; set; }
	public int damage { get; set; }
	public float cooldown { get; set; }
	public float cooldownLeft { get; set; }
	public bool onCooldown { get; set; }

	private SwordAttack swordAttack;
	private GameObject sword;

	public void SetProperties () {}
	public void SetProperties (Player player) {}
	public void SetProperties (GameObject leftSword, GameObject rightSword) {}

	public void SetProperties (GameObject sword) {
		this.sword = sword;
		swordAttack = sword.GetComponent<SwordAttack> ();
		skillName = "Bladestorm";
		skillDescription = "You rotate around yourself slicing through every enemy around you.";
		skillIcon =  Resources.Load<Sprite> ("UI/Icons/bladestorm");
		manaCost = 0;
		baseDamage = 10;
		damage = baseDamage;
		cooldown = 4f;
		cooldownLeft = 0f;
		onCooldown = false;
	}

	void Update () {
		if (onCooldown) {
			cooldownLeft -= Time.deltaTime;
			HUDManager.Instance.UpdateCooldown (6, cooldownLeft, cooldown);
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
		swordAttack.SetHeavyDamage (baseDamage);
		swordAttack.SetAttack(false, true);
	}

	public void Execute (GameObject spellOrigin) {}
	public void Execute (NavMeshAgent playerAgent, Vector3 targetPoint) {}
	public void Execute (NavMeshAgent playerAgent, GameObject enemy) {}
	public void Execute (NavMeshAgent playerAgent, GameObject enemy, GameObject spellOrigin) {}
}
