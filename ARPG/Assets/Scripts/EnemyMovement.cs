using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    NavMeshAgent agent;
    Transform playerPosition;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update() {
        if (!anim.GetBool("Dead")) { 
            GetComponent<NavMeshAgent>().destination = playerPosition.position;
            if (GetComponent<NavMeshAgent>().velocity != Vector3.zero) {
                anim.SetBool("Walk",true);
            }
            else
            {
                anim.SetBool("Walk", false);
            }
        }
    }
}
