using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class WarCry : MonoBehaviour, ISkill {

	public string skillName { get; set; }
	public string skillDescription { get; set; }
	public Image skillIcon { get; set; }
	public int manaCost { get; set; }
	public int baseDamage { get; set; }
	public int damage { get; set; }
	public float cooldown { get; set; }
	public float cooldownLeft { get; set; }
	public bool onCooldown { get; set; }

	private GameObject warCry;
	private Player playerStats;

	public void SetProperties () {}
	public void SetProperties (GameObject sword) {}

	public void SetProperties (Player player) {
		playerStats = player.GetComponent<Player> ();
		warCry = player.transform.Find ("WarCry").gameObject;
		skillName = "War Cry";
		skillDescription = "You cry out gaining strength and health for x seconds.";
		skillIcon = (Image) Resources.Load ("UI/warcry");
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

	public void Execute () {
		warCry.SetActive (true);
	}

	public void Execute (NavMeshAgent playerAgent, GameObject enemy, GameObject spellOrigin) {}
	public void Execute (NavMeshAgent playerAgent, Vector3 targetPoint) {}
	public void Execute (NavMeshAgent playerAgent, GameObject enemy) {}
}
