using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FireballBehaviour : MonoBehaviour {

	private int lifeTicks = 80;
	private int speed = 200;
	private int damage;
	private int aliveFor;
	private NavMeshAgent playerAgent;

	private void Start () {
		GetComponent<Rigidbody>().AddForce(transform.forward * speed);
	}
	
	private void FixedUpdate() {
		aliveFor++;
		if (aliveFor == lifeTicks)
		{
			Destroy(gameObject);
		}
	}

	public void SetPlayerAgent (NavMeshAgent playerAgent) {
		this.playerAgent = playerAgent;
	}

	public void SetFireballDamage(int damage) {
		this.damage = damage;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "Enemy")
		{
			other.GetComponent<EnemyAI> ().SetAttacked (playerAgent.transform);
			other.GetComponent<EnemyHealth>().ReduceHealth(damage);
			Destroy(gameObject);
		}
	}
}
