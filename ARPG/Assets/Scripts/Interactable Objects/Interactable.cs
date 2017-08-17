using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class Interactable : NetworkBehaviour
{

	private NavMeshAgent playerAgent;
	private bool hasInteracted;
	private bool isEnemy;

	public virtual void MoveToInteraction (NavMeshAgent playerAgent, float stoppingDistance) {
		isEnemy = gameObject.tag == "Enemy";
		hasInteracted = false;
		this.playerAgent = playerAgent; 
		playerAgent.stoppingDistance = stoppingDistance;
		playerAgent.destination = transform.position;
	}
	
	void Update () {
		if (playerAgent != null && !playerAgent.pathPending && !hasInteracted) {
			if (playerAgent.remainingDistance <= playerAgent.stoppingDistance) {
				EnsureLookDirection ();
				hasInteracted = true;
                if (isEnemy) {
                    playerAgent.transform.GetComponent<Attack>().CmdAttackPrimary(this.gameObject);         
                } else {
					transform.GetComponent<IInteract> ().Interact (playerAgent.transform);
				}
			}
		}
	}

	void EnsureLookDirection () {
		playerAgent.updateRotation = false;
		Vector3 lookDirection = new Vector3 (transform.position.x, playerAgent.transform.position.y, transform.position.z);
		playerAgent.transform.LookAt (lookDirection);
		playerAgent.updateRotation = true;
	}
}
