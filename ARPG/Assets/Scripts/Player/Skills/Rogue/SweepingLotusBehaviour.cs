using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepingLotusBehaviour : MonoBehaviour {

    private int damage;
    private Collider[] withinRangeColliders;
    public LayerMask enemyLayerMask;
    public float range;
    public int ticks;

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    private void OnEnable()
    {
        StartCoroutine(Procedure());
    }

    IEnumerator Procedure()
    {
        for (int i = 0; i <= ticks; i++)
        {
            yield return new WaitForSeconds(0.7f);
            withinRangeColliders = Physics.OverlapSphere(transform.position, range, enemyLayerMask);
            for (int j = 0; j < withinRangeColliders.Length; j++)
            {
                EnemyHealth enemyHealth = withinRangeColliders[j].GetComponent<EnemyHealth>();
                enemyHealth.ReduceHealth(damage);
            }
        }
        gameObject.SetActive(false);
    }
}
