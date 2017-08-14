using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Bash : MonoBehaviour, ISkill {

	public string skillName { get; set; }
	public string skillDescription { get; set; }
	public Image skillIcon { get; set; }
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

	public void SetProperties (GameObject sword) {
		this.sword = sword;
		swordAttack = sword.GetComponent<SwordAttack> ();
		skillName = "Bash";
		skillDescription = "You slice through your enemy with full power.";
		skillIcon = (Image) Resources.Load ("UI/bash");
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
		swordAttack.SetLightDamage (baseDamage);
		swordAttack.SetAttack(true, false);
	}

	public void Execute (NavMeshAgent playerAgent, Vector3 targetPoint) {}
	public void Execute (NavMeshAgent playerAgent, GameObject enemy) {}
	public void Execute (NavMeshAgent playerAgent, GameObject enemy, GameObject spellOrigin) {}

}
