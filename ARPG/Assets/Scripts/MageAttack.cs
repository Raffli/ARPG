using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageAttack : MonoBehaviour, IAttack {
	
	private Animator animator;
	public GameObject spellOrigin;
	public GameObject fireBall;
	public GameObject shockWave;

	public int lightDamage;
	public int heavyDamage;
	public float lightCooldown;
	public float heavyCooldown;

	void Start () {
		animator = GetComponent<Animator>();
	}

	public void AttackPrimary (GameObject enemy) {
		
		Vector3 spawnPoint = spellOrigin.transform.position;
		Vector3 targetPoint = enemy.transform.position;
		Vector3 toTarget = targetPoint - spawnPoint;
		GameObject obj = Instantiate(fireBall, spawnPoint, Quaternion.LookRotation(toTarget));
		obj.GetComponent<Fireball>().SetFireballDamage(lightDamage);
	}

	public void AttackSecondary (GameObject enemy) {
		throw new System.NotImplementedException ();
	}
		
}
