using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI: MonoBehaviour {

    NavMeshAgent agent;
    Transform playerPosition;
    PlayerHealth playerHealth;
    Animator anim;
    Collider[] withinAggroColliders;
    public LayerMask aggroLayerMask;
    public float aggroRadius;
    public int damage;
    public float attackSpeed;
    bool aggro;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        withinAggroColliders = Physics.OverlapSphere(transform.position, aggroRadius, aggroLayerMask);
        if (withinAggroColliders.Length > 0)
        {
            aggro = true;
            playerPosition = withinAggroColliders[0].GetComponent<Transform>();
            playerHealth = withinAggroColliders[0].GetComponent<PlayerHealth>();
        }
        else if (aggro){
            aggro = false;
        }
    }

    void DealDamage() {
        if (playerHealth)
        {
            playerHealth.TakeDamage(damage);
        }
    }

    void AttackPlayer()
    {
        anim.SetTrigger("Attack");        
    }


    void Update() {
        if (!anim.GetBool("Dead") && aggro)
        {
            agent.isStopped = false;
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                anim.SetBool("Walk", false);
                if (!IsInvoking("AttackPlayer")) {
                    InvokeRepeating("AttackPlayer", 0.5f, attackSpeed);
                }
            }
            else
            {
                anim.SetBool("Walk", true);
                CancelInvoke("AttackPlayer");
            }
            agent.SetDestination(playerPosition.position);
        }
        else
        {
            agent.isStopped = true;
            CancelInvoke("AttackPlayer");
            anim.SetBool("Walk", false);
        }
    }
}
