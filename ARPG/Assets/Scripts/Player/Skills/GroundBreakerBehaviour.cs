using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundBreakerBehaviour : MonoBehaviour {

	private int damage;
	private NavMeshAgent playerAgent;

	public void SetDamage(int damage) {
		this.damage = damage;
	}

	public void SetPlayerAgent (NavMeshAgent playerAgent) {
		this.playerAgent = playerAgent;
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
