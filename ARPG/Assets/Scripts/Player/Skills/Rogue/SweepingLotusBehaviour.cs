using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepingLotusBehaviour : MonoBehaviour {

    private int damage;
    private Collider[] withinRangeColliders;
    public LayerMask enemyLayerMask;
    public float range;
    public int ticks;
    AudioSource source;
    public AudioClip[] hitSounds;

    public void SetDamage(int damage)
    {
        print("lotusdamage" + damage);
        this.damage = damage;
    }

    private void OnEnable()
    {
        source = GetComponent<AudioSource>();
        StartCoroutine(Procedure());
    }

    IEnumerator Procedure()
    {
        for (int i = 0; i <= ticks; i++)
        {
            yield return new WaitForSeconds(0.1f);
            withinRangeColliders = Physics.OverlapSphere(transform.position, range, enemyLayerMask);
            for (int j = 0; j < withinRangeColliders.Length; j++)
            {
                source.PlayOneShot(hitSounds[Random.Range(0, hitSounds.Length)]);
                EnemyHealth enemyHealth = withinRangeColliders[j].GetComponent<EnemyHealth>();
                enemyHealth.ReduceHealth(damage);
            }
        }
        Destroy(gameObject);
    }
}
