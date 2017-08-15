using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour {

    int lightDamage;
    int heavyDamage;
    bool lightAttack;
    bool heavyAttack;
    Collider swordColl;

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
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Enemy") {
            if (lightAttack)
            {      
                other.GetComponent<EnemyHealth>().ReduceHealth(lightDamage);
                swordColl.enabled = false;
            }
            else if(heavyAttack)
            {
                other.GetComponent<EnemyHealth>().ReduceHealth(heavyDamage);
                swordColl.enabled = false;
            }
        }
    }
}
