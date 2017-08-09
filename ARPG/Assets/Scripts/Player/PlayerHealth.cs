using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public int currentHealth;
    public int maxHealth;

    void Start() {
        this.currentHealth = this.maxHealth;
    }

    public void TakeDamage(int amount) {
        currentHealth -= amount;
        print("got hit -" + amount + " Health");
        if (currentHealth <= 0) {
            Die();
        }
    }

    void Die() {
        print("Player ist tot");
    }
}
