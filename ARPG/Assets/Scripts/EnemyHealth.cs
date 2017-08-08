using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class EnemyHealth : MonoBehaviour {


    public int currentHealth;
    public int maxHealth;
    public Slider healthBar;
    public GameObject combatTextPrefab;
    Animator anim;
    Rigidbody rb;
    BoxCollider boxCollider;
    NavMeshAgent agent;
    EnemyAI enemyAI;
    bool isDead;

    void Start() {
        this.currentHealth = this.maxHealth;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        agent = GetComponent<NavMeshAgent>();
        enemyAI = GetComponent<EnemyAI>();
        healthBar.value = currentHealth / maxHealth;

    }

    private void Die() {
        isDead = true;
        enemyAI.enabled = false;
        anim.SetTrigger("Dead");
        rb.isKinematic = true;
        rb.useGravity = false;
        RemoveComponents();
    }

    private void RemoveComponents(){
        Destroy(transform.Find("EnemyCanvas").gameObject);
        Destroy(agent);
        Destroy(boxCollider);
        StartCoroutine(RemoveSelf());
    }

    public void ReduceHealth(int damage) {
        if (!isDead)
        {
            currentHealth -= damage;
            healthBar.value = (float)currentHealth / (float)maxHealth;
            InitCombatText(damage.ToString());
            if (currentHealth <= 0)
            {
				Die ();
            }
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
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }



}
