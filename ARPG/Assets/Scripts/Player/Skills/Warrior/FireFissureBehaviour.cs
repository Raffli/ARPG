using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class FireFissureBehaviour : MonoBehaviour {

    public Collider[] colliderArray;
    private int damage;
    private GameObject spellOrigin;

    private void OnEnable()
	{
		StartCoroutine(Procedure());
	}

    public void SetFireFissureDamage(int damage)
    {
        this.damage = damage;
    }

    IEnumerator Procedure()
	{
        for (int i = 0; i < colliderArray.Length; i++)
        {
            colliderArray[i].enabled = true;
            yield return new WaitForSeconds(0.1f);
            colliderArray[i].enabled = false;
            yield return new WaitForSeconds(0.1f);
        }

        Destroy(gameObject);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
            other.GetComponent<EnemyHealth>().ReduceHealth(damage);
        }
    }



}
