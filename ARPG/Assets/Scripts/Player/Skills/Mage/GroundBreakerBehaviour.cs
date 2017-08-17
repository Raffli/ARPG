﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundBreakerBehaviour : MonoBehaviour {

	private int damage;
	private NavMeshAgent playerAgent;
    private Collider[] withinRangeColliders;
    public LayerMask enemyLayerMask;
    public float range;

    private void OnEnable()
    {
        StartCoroutine(Procedure());
    }

    public void SetDamage(int damage) {
		this.damage = damage;
	}

	public void SetPlayerAgent (NavMeshAgent playerAgent) {
		this.playerAgent = playerAgent;
	}


    IEnumerator Procedure()
    {
        yield return new WaitForSeconds(0.5f);
        withinRangeColliders = Physics.OverlapSphere(transform.position, range, enemyLayerMask);
        for (int j = 0; j < withinRangeColliders.Length; j++)
        {
            withinRangeColliders[j].GetComponent<EnemyAI>().SetAttacked(playerAgent.transform);
            EnemyHealth enemyHealth = withinRangeColliders[j].GetComponent<EnemyHealth>();
            enemyHealth.ReduceHealth(damage);
        }
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
