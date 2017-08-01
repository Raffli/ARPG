using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {


    public int currentHealth;
    public int maxHealth;
    Animator anim;
    bool isDead;

    void Start () {
        this.currentHealth = this.maxHealth;
        anim = GetComponent<Animator>();
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
            }
        }
    }




}
