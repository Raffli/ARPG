using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Networking;


public class EnemyHealth : NetworkBehaviour {

    [SyncVar]
    public int currentHealth;

    public int level;
    public int maxHealth;
    public int lastHealth;
	private int xp;
    public Slider healthBar;
    public GameObject combatTextPrefab;
    Animator anim;
    Rigidbody rb;
    BoxCollider boxCollider;
    NavMeshAgent agent;
    EnemyAI enemyAI;
    bool isDead;
    MusicController musicController;
	GameObject [] warriors;
	GameObject [] mages;
	GameObject [] rogues;
	List <Player> players;


    void Start() {
        this.maxHealth = this.maxHealth * level;
        this.currentHealth = this.maxHealth;
        this.lastHealth = this.currentHealth;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        agent = GetComponent<NavMeshAgent>();
        enemyAI = GetComponent<EnemyAI>();
        healthBar.value = currentHealth / maxHealth;
        musicController = GameObject.FindGameObjectWithTag("MusicController").GetComponent<MusicController>();
		xp = Mathf.RoundToInt (maxHealth * 0.5f);
		warriors = GameObject.FindGameObjectsWithTag ("Warrior");
		mages = GameObject.FindGameObjectsWithTag ("Mage");
		rogues = GameObject.FindGameObjectsWithTag ("Rouge");
		players = new List<Player> ();
		foreach (GameObject warrior in warriors) {
			players.Add (warrior.GetComponent<Player> ());
		}
		foreach (GameObject mage in mages) {
			players.Add (mage.GetComponent<Player> ());
		}
		foreach (GameObject rogue in rogues) {
			players.Add (rogue.GetComponent<Player> ());
		}
    }

    private void Die() {
        musicController.EndBattle();
        isDead = true;
        enemyAI.enabled = false;
        anim.SetBool("Dead",true);
        rb.isKinematic = true;
        rb.useGravity = false;
        Destroy(transform.Find("EnemyCanvas").gameObject);
        Destroy(boxCollider);
        agent.isStopped = true;
		if (Random.Range (0, 4) == 0) {
			LootManager.Instance.SpawnLoot (transform.position);
		}
		foreach (Player player in players) {
			player.GiveXP (xp);
		}
        StartCoroutine(RemoveSelf());
    }

    private void Update()
    {
        if (lastHealth != currentHealth)
        {
            healthBar.value = (float)currentHealth / (float)maxHealth;
            InitCombatText((lastHealth-currentHealth).ToString());
            lastHealth = currentHealth;
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    public void SetLevel(int level)
    {
        this.level = level;
    }

    public void ReduceHealth(int damage) {
        if (!isDead)
        {
            currentHealth -= damage;
        }
    }


    private void InitCombatText(string damage) {
        GameObject tempCombatText = Instantiate(combatTextPrefab) as GameObject;
        RectTransform tempCombatTextRect = tempCombatText.GetComponent<RectTransform>();
        tempCombatText.transform.SetParent(transform.Find("EnemyCanvas"));
        tempCombatText.transform.localPosition = combatTextPrefab.transform.localPosition;
        tempCombatText.transform.localScale = combatTextPrefab.transform.localScale;
        tempCombatText.transform.localRotation = combatTextPrefab.transform.localRotation;

        tempCombatText.GetComponent<Text>().text = damage;
        Destroy(tempCombatText.gameObject, 2f);

    }

    IEnumerator RemoveSelf()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(agent);
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }



}
