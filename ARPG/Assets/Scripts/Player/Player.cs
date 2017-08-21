using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AI;


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
    private NetworkStartPosition[] spawnPoints;


    void Awake()
    {

        DontDestroyOnLoad(transform.gameObject);
        
    }



    void Start() {
        if (!isLocalPlayer)
        {
            Destroy(transform.Find("Main Camera").gameObject);
        }
        else {
            transform.Find("Main Camera").gameObject.SetActive(true);
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


    private void OnLevelWasLoaded(int level)
    {
        if (level > 1) {

            GetComponent<NavMeshAgent>().enabled = false;
            spawnPoints = FindObjectsOfType<NetworkStartPosition>();

            Vector3 spawnPoint = Vector3.zero;

            if (spawnPoints != null && spawnPoints.Length > 0)
            {
                spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
            }
            transform.position = spawnPoint;
            GetComponent<NavMeshAgent>().enabled = true;

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
