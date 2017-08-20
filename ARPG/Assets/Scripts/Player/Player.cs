using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    [SyncVar]
	public int level;
 	public int xp;
	public int xpToLevel;
    [SyncVar]
    public int maximumMana;
    [SyncVar]
    public int currentMana;
    [SyncVar]
    public int maxHealth;
    [SyncVar]
    public int currentHealth;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }


    void Start() {
        if (!isLocalPlayer)
        {
            Destroy(transform.Find("Main Camera").gameObject);
        }
        currentHealth = maxHealth;
		currentMana = maximumMana;
		HUDManager.Instance.UpdateHP (currentHealth, maxHealth);
		HUDManager.Instance.UpdateMana (currentMana, maximumMana);
        currentHealth = maxHealth;
	}

	public void TakeDamage (int amount) {
		currentHealth -= amount;
		HUDManager.Instance.UpdateHP (currentHealth, maxHealth);
		if (currentHealth <= 0) {
			Die();
		}
	}

	public void Heal (int amount) {
		currentHealth += amount;
		if (currentHealth > maxHealth) {
			currentHealth = maxHealth;
		}
		HUDManager.Instance.UpdateHP (currentHealth, maxHealth);
	}

	public void IncreaseMana (int amount) {
		currentMana += amount;
		if (currentMana > maximumMana) {
			currentMana = maximumMana;
		}
		HUDManager.Instance.UpdateMana (currentMana, maximumMana);
	}

	public void ReduceMana (int amount) {
		currentMana -= amount;
		HUDManager.Instance.UpdateMana (currentMana, maximumMana);
	}

	void Die() {
		currentHealth = maxHealth;
	}
}
