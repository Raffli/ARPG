using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Stealth : MonoBehaviour, ISkill {

	public string skillName { get; set; }
	public string skillDescription { get; set; }
	public Sprite skillIcon { get; set; }
	public int manaCost { get; set; }
	public int baseDamage { get; set; }
	public int damage { get; set; }
	public float cooldown { get; set; }
	public float cooldownLeft { get; set; }
	public bool onCooldown { get; set; }

	private GameObject stealth;
	private Player playerStats;

	public void SetProperties () {}
	public void SetProperties (GameObject sword) {}
	public void SetProperties (GameObject leftSword, GameObject rightSword) {}

	public void SetProperties (Player player) {
		playerStats = player.GetComponent<Player> ();
		stealth = player.transform.Find ("Stealth").gameObject;
		skillName = "Stealth";
		skillDescription = "You concentrate and become invisible to enemies for a while.";
		skillIcon =  Resources.Load<Sprite> ("UI/Icons/stealth");
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
			HUDManager.Instance.UpdateCooldown (1, cooldownLeft, cooldown);
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
		stealth.SetActive (true);
		// add to player.damage or something
	}

	public void Execute (GameObject spellOrigin) {}
	public void Execute (NavMeshAgent playerAgent, GameObject enemy, GameObject spellOrigin) {}
	public void Execute (NavMeshAgent playerAgent, Vector3 targetPoint) {}
	public void Execute (NavMeshAgent playerAgent, GameObject enemy) {}
}
