using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Interactable : MonoBehaviour {

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
				if (isEnemy) {
					playerAgent.transform.GetComponent<IAttack> ().AttackPrimary (this.gameObject);
					transform.GetComponent<EnemyAI> ().SetAttacked (playerAgent.transform);
				} else {
					Debug.Log ("interact with object");
					/*Animator objectAnimator = transform.GetComponent<Animator> ();
					if (objectAnimator != null) {
						objectAnimator.SetTrigger ("hasInteracted");
					}*/
				}
				EnsureLookDirection ();
				hasInteracted = true;
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
