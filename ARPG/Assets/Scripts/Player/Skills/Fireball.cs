using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Fireball : MonoBehaviour, ISkill {

	public string skillName { get; set; }
	public Image skillIcon { get; set; }
	public int manaCost { get; set; }
	public int baseDamage { get; set; }
	public int damage { get; set; }
	public float cooldown { get; set; }
	public float cooldownLeft { get; set; }
	public bool onCooldown { get; set; }

	public void SetProperties () {
		skillName = "Fireball";
		skillIcon = (Image) Resources.Load ("/UI/Fireball");
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

	public void Execute (Transform player) {}

	public void Execute (NavMeshAgent playerAgent, GameObject enemy, GameObject spellOrigin) { 
		Vector3 spawnPoint = spellOrigin.transform.position;
		Vector3 targetPoint = enemy.transform.position;
		Vector3 toTarget = targetPoint - spawnPoint;
		GameObject fireball = (GameObject) Resources.Load ("Skills/Fireball");
		GameObject obj = Instantiate (fireball, spawnPoint, Quaternion.LookRotation (toTarget));
		obj.GetComponent<FireballBehaviour> ().SetPlayerAgent (playerAgent);
		Debug.Log ("damage is " + damage);
		obj.GetComponent<FireballBehaviour> ().SetFireballDamage (damage);
	}

	public void StartCooldown () {
		onCooldown = true;
		cooldownLeft = cooldown;
	}
}
