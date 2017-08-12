using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class GroundBreaker : MonoBehaviour, ISkill {

	public string skillName { get; set; }
	public string skillDescription { get; set; }
	public Image skillIcon { get; set; }
	public int manaCost { get; set; }
	public int baseDamage { get; set; }
	public int damage { get; set; }
	public float cooldown { get; set; }
	public float cooldownLeft { get; set; }
	public bool onCooldown { get; set; }

	private int floorMask;

	public void SetProperties () {
		skillName = "Ground Breaker";
		skillDescription = "You break the ground at the target location.";
		skillIcon = (Image) Resources.Load ("/UI/groundbreaker");
		manaCost = 25;
		baseDamage = 20;
		damage = baseDamage;
		cooldown = 3f;
		cooldownLeft = 0f;
		onCooldown = false;

		floorMask = LayerMask.GetMask ("Floor");
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
	public void Execute (NavMeshAgent playerAgent, GameObject enemy, GameObject spellOrigin) {}

	public void Execute (NavMeshAgent playerAgent, Vector3 targetPoint) { 
		Ray interactionRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit interactionInfo; 
		if (Physics.Raycast (interactionRay, out interactionInfo, Mathf.Infinity, floorMask)) {
			GameObject groundBreaker = (GameObject)Resources.Load ("Skills/GroundBreaker");
			GameObject obj = Instantiate (groundBreaker, interactionInfo.point, transform.rotation);
			obj.GetComponent<GroundBreakerBehaviour> ().SetPlayerAgent (playerAgent);
			obj.GetComponent<GroundBreakerBehaviour> ().SetDamage (damage);
		}
	}

	public void StartCooldown () {
		onCooldown = true;
		cooldownLeft = cooldown;
	}
}
