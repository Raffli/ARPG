using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Shield : MonoBehaviour, ISkill {

	public string skillName { get; set; }
	public string skillDescription { get; set; }
	public Image skillIcon { get; set; }
	public int manaCost { get; set; }
	public int baseDamage { get; set; }
	public int damage { get; set; }
	public float cooldown { get; set; }
	public float cooldownLeft { get; set; }
	public bool onCooldown { get; set; }

	private GameObject shield;
	private Player playerStats;

	public void SetProperties () {
		skillName = "Protective Aura";
		skillDescription = "You concentrate your magic energy to generate a protective aura taht shields you.";
		skillIcon = (Image) Resources.Load ("UI/protectingAura");
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

	public void Execute (Transform player) {
		shield = player.Find ("Shield").gameObject;
		playerStats = player.GetComponent<Player> ();
		shield.SetActive (true);
		// add to player.armor or something
	}

	public void Execute (NavMeshAgent playerAgent, GameObject enemy, GameObject spellOrigin) {}
	public void Execute (NavMeshAgent playerAgent, Vector3 targetPoint) {}
}
