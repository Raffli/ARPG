using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

	public int level;
 	public int xp;
	public int xpToLevel;
	public int maximumMana;
	public int currentMana;
	public int maxHealth;
	public int currentHealth;


	void Start() {
        print("localplayer : " + isLocalPlayer);
        if (!isLocalPlayer) { 
            Destroy(transform.Find("Main Camera").gameObject);
        }
        currentHealth = maxHealth;
	}

	public void TakeDamage (int amount) {
		currentHealth -= amount;
		if (currentHealth <= 0) {
			Die();
		}
	}

	void Die() {
		Debug.Log("Player ist tot");
	}
}
