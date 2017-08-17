using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public int level;
 	public int xp;
	public int xpToLevel;
	public int maximumMana;
	public int currentMana;
	public int maxHealth;
	public int currentHealth;

	void Start() {
		currentHealth = maxHealth;
		currentMana = maximumMana;
		HUDManager.Instance.UpdateHP (currentHealth, maxHealth);
		HUDManager.Instance.UpdateMana (currentMana, maximumMana);
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
		Debug.Log("Player ist tot");
		currentHealth = maxHealth;
	}
}
