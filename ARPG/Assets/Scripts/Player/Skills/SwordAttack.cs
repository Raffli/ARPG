using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour {

    int lightDamage;
    int heavyDamage;
    bool lightAttack;
    bool heavyAttack;
    Collider swordColl;
    Collider lastCollider;

    void Start() {
        swordColl = GetComponent<BoxCollider>();
		DisableSword ();
    }

    public void SetLightDamage(int damage) {
        lightDamage = damage;
    }
    public void SetHeavyDamage(int damage) {
        heavyDamage = damage;
    }

    public void SetAttack(bool lAttack,bool hAttack)
    {
        lightAttack = lAttack;
        heavyAttack = hAttack;

        if (lightAttack || heavyAttack)
        {
            swordColl.enabled = true;
        }
    }

    public void DisableSword (){
		swordColl.enabled = false;
        lastCollider = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Enemy") {
            if (lastCollider != other)
            {
                lastCollider = null;
                if (lightAttack)
                {
                    other.GetComponent<EnemyHealth>().ReduceHealth(lightDamage);
                    lastCollider=other;
                }
                else if (heavyAttack)
                {
                    other.GetComponent<EnemyHealth>().ReduceHealth(heavyDamage);
                    lastCollider=other;
                }
            }
            else {
                return;
            }
        }
    }
}
