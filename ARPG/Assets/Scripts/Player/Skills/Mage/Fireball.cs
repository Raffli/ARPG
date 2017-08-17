using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Fireball : MonoBehaviour, ISkill {

	public string skillName { get; set; }
	public string skillDescription { get; set; }
	public Sprite skillIcon { get; set; }
	public int manaCost { get; set; }
	public int baseDamage { get; set; }
	public int damage { get; set; }
	public float cooldown { get; set; }
	public float cooldownLeft { get; set; }
	public bool onCooldown { get; set; }

	public void SetProperties (Player player) {}
	public void SetProperties (GameObject sword) {}
	public void SetProperties (GameObject leftSword, GameObject rightSword) {}

	public void SetProperties () {
		skillName = "Fireball";
		skillDescription = "You create a fireball that is thrown at the target and deals damage.";
		skillIcon =  Resources.Load<Sprite> ("UI/Icons/fireball");
		manaCost = 10;
		baseDamage = 10;
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

	public void Execute (GameObject spellOrigin) {}
	public void Execute () {}
	public void Execute (NavMeshAgent playerAgent, Vector3 targetPoint) {}
	public void Execute (NavMeshAgent playerAgent, GameObject enemy) {}

	public void Execute (NavMeshAgent playerAgent, GameObject enemy, GameObject spellOrigin) { 
		Vector3 spawnPoint = spellOrigin.transform.position;
		Vector3 targetPoint = enemy.transform.position;
		Vector3 toTarget = targetPoint - spawnPoint;
		GameObject fireball = (GameObject) Resources.Load ("Skills/Fireball");
		GameObject obj = Instantiate (fireball, spawnPoint, Quaternion.LookRotation (toTarget));
		obj.GetComponent<FireballBehaviour> ().SetPlayerAgent (playerAgent);
		obj.GetComponent<FireballBehaviour> ().SetFireballDamage (damage);
	}

	public void StartCooldown () {
		onCooldown = true;
		cooldownLeft = cooldown;
	}
}
