using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class ShockWave : MonoBehaviour, ISkill {

	public string skillName { get; set; }
	public Image skillIcon { get; set; }
	public int manaCost { get; set; }
	public int baseDamage { get; set; }
	public int damage { get; set; }
	public float cooldown { get; set; }
	public float cooldownLeft { get; set; }
	public bool onCooldown { get; set; }

	private GameObject shockWave;

	public void SetProperties () {
		skillName = "Shockwave";
		skillIcon = (Image) Resources.Load ("/UI/Shockwave");
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

	public void Execute (Transform player) {
		Debug.Log ("Execute the shock wave");
		shockWave = player.Find ("Shockwave").gameObject;
		shockWave.SetActive (true);
		shockWave.GetComponent<ShockWaveBehaviour> ().SetDamage (damage);
	}

	public void Execute (NavMeshAgent playerAgent, GameObject enemy, GameObject spellOrigin) {}
}
