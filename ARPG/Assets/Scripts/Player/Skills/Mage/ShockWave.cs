using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class ShockWave : MonoBehaviour, ISkill {

	public string skillName { get; set; }
	public string skillDescription { get; set; }
	public Image skillIcon { get; set; }
	public int manaCost { get; set; }
	public int baseDamage { get; set; }
	public int damage { get; set; }
	public float cooldown { get; set; }
	public float cooldownLeft { get; set; }
	public bool onCooldown { get; set; }

	private GameObject shockWave;

	public void SetProperties () {}
	public void SetProperties (GameObject sword) {}
	public void SetProperties (GameObject leftSword, GameObject rightSword) {}

	public void SetProperties (Player player) {
		shockWave = player.transform.Find ("Shockwave").gameObject;
		skillName = "Shockwave";
		skillDescription = "You emit a shockwave around you that deals damage to any enemy it hits.";
		skillIcon = (Image) Resources.Load ("UI/shockwave");
		manaCost = 20;
		baseDamage = 15;
		damage = baseDamage;
		cooldown = 4f;
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
		shockWave.SetActive (true);
		shockWave.GetComponent<ShockWaveBehaviour> ().SetDamage (damage);
	}

	public void Execute (NavMeshAgent playerAgent, GameObject enemy, GameObject spellOrigin) {}
	public void Execute (NavMeshAgent playerAgent, Vector3 targetPoint) {}
	public void Execute (NavMeshAgent playerAgent, GameObject enemy) {}
}
