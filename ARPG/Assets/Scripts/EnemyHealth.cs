using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyHealth : MonoBehaviour {


    public int currentHealth;
    public int maxHealth;
    Animator anim;
    Rigidbody rb;
    BoxCollider boxCollider;
    NavMeshAgent agent;
	EnemyAI enemyAI;
    bool isDead;

    void Start () {
        this.currentHealth = this.maxHealth;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        agent = GetComponent<NavMeshAgent>();
		enemyAI = GetComponent<EnemyAI> ();
    }

	void Die () {
		isDead = true;
		enemyAI.enabled = false;
		anim.SetTrigger("Dead");
		rb.isKinematic = true;
		rb.useGravity = false;
		Destroy(agent);
		Destroy(boxCollider);
		StartCoroutine(Remove());
	}

    public void ReduceHealth(int damage) {
        if (!isDead)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
				Die ();
            }
        }
    }

    IEnumerator Remove()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }



}
