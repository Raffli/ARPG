using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class TwinBlades : MonoBehaviour, ISkill {

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
	private SwordAttack rightSwordAttack;
	private GameObject rightSword;

	public void SetProperties () {}
	public void SetProperties (Player player) {}
	public void SetProperties (GameObject sword) {}

	public void SetProperties (GameObject leftSword, GameObject rightSword) {
		this.leftSword = leftSword;
		leftSwordAttack = leftSword.GetComponent<SwordAttack>();
		this.rightSword = rightSword;
		rightSwordAttack = rightSword.GetComponent<SwordAttack>();
		skillName = "Twin Blades";
		skillDescription = "You hit your enemy with two lightning fast blades.";
		skillIcon = (Image) Resources.Load ("UI/twinBlades");
		manaCost = 0;
		baseDamage = 30;
		damage = baseDamage;
		cooldown = 1.3f;
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
		rightSwordAttack.SetHeavyDamage (baseDamage);
		rightSwordAttack.SetAttack(false, true);
        leftSwordAttack.SetHeavyDamage(baseDamage);
        leftSwordAttack.SetAttack(false, true);
    }

	public void Execute (GameObject spellOrigin) {}
	public void Execute (NavMeshAgent playerAgent, Vector3 targetPoint) {}
	public void Execute (NavMeshAgent playerAgent, GameObject enemy) {}
	public void Execute (NavMeshAgent playerAgent, GameObject enemy, GameObject spellOrigin) {}
}
