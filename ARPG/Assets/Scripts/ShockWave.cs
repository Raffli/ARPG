using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : MonoBehaviour {

    Collider[] withinRangeColliders;
    public LayerMask enemyLayerMask;
    public float range;
    int heavyDamage;	

    public void SetDamage(int damage) {
        heavyDamage = damage;
    }

    private void OnEnable()
    {
        StartCoroutine(Procedure());
    }

    IEnumerator Procedure()
    {
        yield return new WaitForSeconds(0.3f);
        withinRangeColliders = Physics.OverlapSphere(transform.position, range, enemyLayerMask);
        for (int i = 0; i < withinRangeColliders.Length; i++) {
            EnemyHealth enemyHealth = withinRangeColliders[i].GetComponent<EnemyHealth>();
            enemyHealth.ReduceHealth(heavyDamage);
        }
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
    }
}
