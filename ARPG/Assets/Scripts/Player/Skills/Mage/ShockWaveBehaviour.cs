using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWaveBehaviour : MonoBehaviour {

    private Collider[] withinRangeColliders;
    public LayerMask enemyLayerMask;
    public float range;
    private int damage;	

    public void SetDamage(int damage) {
		this.damage = damage;
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
            enemyHealth.ReduceHealth(damage);
        }
        yield return new WaitForSeconds(0.6f);
        gameObject.SetActive(false);
    }
}
