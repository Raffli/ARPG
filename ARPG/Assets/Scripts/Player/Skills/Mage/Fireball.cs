using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Fireball : Skill {

	public override void SetProperties () {
		skillName = "Fireball";
		skillDescription = "You create a fireball that is thrown at the target and deals damage.";
		skillIcon = (Sprite) Resources.Load ("UI/fireball");
		manaCost = 0;
		baseDamage = 10;
		damage = baseDamage;
		cooldown = 2f;
		cooldownLeft = 0f;
		onCooldown = false;
	}

	public override void Execute (NavMeshAgent playerAgent, GameObject enemy, GameObject spellOrigin) { 
		Vector3 spawnPoint = spellOrigin.transform.position;
		Vector3 targetPoint = enemy.transform.position;
		Vector3 toTarget = targetPoint - spawnPoint;
		GameObject fireball = (GameObject) Resources.Load ("Skills/Fireball");
		GameObject obj = Instantiate (fireball, spawnPoint, Quaternion.LookRotation (toTarget));
		obj.GetComponent<FireballBehaviour> ().SetPlayerAgent (playerAgent);
		obj.GetComponent<FireballBehaviour> ().SetFireballDamage (damage);
	}
}
