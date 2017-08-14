using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI: MonoBehaviour {

    NavMeshAgent agent;
    Transform playerPosition;
	Player player;
    Animator anim;
    Collider[] withinAggroColliders;
    public LayerMask aggroLayerMask;
    public float aggroRadius;
    public int damage;
    public float attackSpeed;
    bool aggro;

	private bool wasAttacked;
	private Vector3 spawnPosition;
	private bool goingBack;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
		spawnPosition = transform.position;
    }

    private void FixedUpdate()
    {
        withinAggroColliders = Physics.OverlapSphere(transform.position, aggroRadius, aggroLayerMask);
		if (withinAggroColliders.Length > 0)
        {
            aggro = true;
			goingBack = false;
			agent.stoppingDistance = 8f;
            playerPosition = withinAggroColliders[0].GetComponent<Transform>();
            player = withinAggroColliders[0].GetComponent<Player>();
        }
        else if (aggro){
            aggro = false;
			GoBackToSpawn ();
        }
    }

	public void SetAttacked (Transform playerPosition) {
		StopCoroutine (ResetWasAttacked ());
		this.playerPosition = playerPosition;
		wasAttacked = true;
		StartCoroutine (ResetWasAttacked ());
	}

    void DealDamage() {
        if (player)
        {
            player.TakeDamage(damage);
        }
    }

    void AttackPlayer()
    {
        anim.SetTrigger("Attack");        
    }


    void Update() {
		if (!anim.GetBool("Dead") && (aggro || wasAttacked || goingBack) && agent)
        {
            agent.isStopped = false;
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                anim.SetBool("Walk", false);
				EnsureLookDirection ();
				if (!goingBack) {
					if (!IsInvoking ("AttackPlayer")) {
						InvokeRepeating ("AttackPlayer", 0.5f, attackSpeed);
					}
				} else {
					goingBack = false;
				}
            }
            else
            {
                anim.SetBool("Walk", true);
                CancelInvoke("AttackPlayer");
            }
			if (!goingBack)
           		agent.SetDestination(playerPosition.position);
        }
        else
        {
            if (agent) {
                agent.isStopped = true;
            }
            CancelInvoke("AttackPlayer");
            anim.SetBool("Walk", false);
        }
    }

	void EnsureLookDirection () {
		if (agent) {
			agent.updateRotation = false;
			Vector3 lookDirection = new Vector3 (playerPosition.position.x, transform.position.y, playerPosition.position.z);
			agent.transform.LookAt (lookDirection);
			agent.updateRotation = true;
		}
	}

	void GoBackToSpawn () {
		if (agent) {
			goingBack = true;
			agent.stoppingDistance = 0.1f;
			agent.SetDestination (spawnPosition);
		}
	}

	IEnumerator ResetWasAttacked () {
		yield return new WaitForSeconds (3f);
		wasAttacked = false;
		GoBackToSpawn ();
	}
}
