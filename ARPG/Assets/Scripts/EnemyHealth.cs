using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {


    public int health;
    Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if (health <= 0) {
            anim.SetTrigger("Dead");
        }
	    	
	}

    public void ReduceHealth(int damage) {
        health -= damage;
    }




}
