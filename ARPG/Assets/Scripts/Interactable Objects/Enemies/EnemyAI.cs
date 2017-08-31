﻿using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class EnemyAI: NetworkBehaviour {

    NavMeshAgent agent;
    Transform playerPosition;
	Player player;
    Animator anim;
    Collider[] withinAggroColliders;
    MusicController musicController;
    public LayerMask aggroLayerMask;
    public float aggroRadius;
    public int damage;
    public float attackSpeed;
    public int level;
    bool aggro;
    bool setMusic;


	private bool wasAttacked;
	private Vector3 spawnPosition;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
		spawnPosition = transform.position;
        musicController = GameObject.FindGameObjectWithTag("MusicController").GetComponent<MusicController>();
    }

    private void FixedUpdate()
    {
        withinAggroColliders = Physics.OverlapSphere(transform.position, aggroRadius, aggroLayerMask);
		if (withinAggroColliders.Length > 0)
        {
            playerPosition = withinAggroColliders[0].GetComponent<Transform>();
            player = withinAggroColliders[0].GetComponent<Player>();
            if (player != null) {
				if (!player.invisible) {
					aggro = true;
			        if (!setMusic && !anim.GetBool("Dead"))
			        {
			            musicController.SetBattle();
			            setMusic = true;
			        }
			    } else {
			        aggro = false;
			    }
			}
        }
        else if (aggro){
            aggro = false;
        }

        if (!anim.GetBool("Dead") && (aggro || wasAttacked) && agent)
        {
            agent.isStopped = false;
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                anim.SetBool("Walk", false);
                EnsureLookDirection();
                if (!IsInvoking("AttackPlayer"))
                {
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
            StopBattleMusic();
            if (agent) {
				agent.isStopped = true;
			}
			CancelInvoke("AttackPlayer");
			anim.SetBool("Walk", false);      
        }
    }

    void StopBattleMusic() {
        if (setMusic)
        {
            musicController.EndBattle();
            setMusic = false;
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
            player.TakeDamage(damage * level);
        }
    }

    void AttackPlayer()
    {
        anim.SetTrigger("Attack");        
    }

    public void SetLevel(int level) {
        this.level = level;
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
