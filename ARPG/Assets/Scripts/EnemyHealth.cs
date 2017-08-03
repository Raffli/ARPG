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
    bool isDead;

    void Start () {
        this.currentHealth = this.maxHealth;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        agent = GetComponent<NavMeshAgent>();
    }
	
	void Update () {
        
	}

    public void ReduceHealth(int damage) {
        if (!isDead)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                isDead = true;
                anim.SetTrigger("Dead");
                rb.isKinematic = true;
                rb.useGravity = false;
                Destroy(agent);
                Destroy(boxCollider);
                StartCoroutine(Remove());
            }
        }
    }

    IEnumerator Remove()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }



}
