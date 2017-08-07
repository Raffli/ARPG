using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorldInteraction : MonoBehaviour {

	private NavMeshAgent playerAgent;
	private Animator animator;
	private int floorMask;

	public float attackRange;

	void Start () {
		playerAgent = GetComponent<NavMeshAgent> ();
		animator = GetComponent<Animator> ();
		floorMask = LayerMask.GetMask ("Floor");
	}
	
	void Update () {
		if (Input.GetMouseButtonDown (0) /*&& !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()*/) {
			GetInteraction ();
		}

		if (playerAgent.remainingDistance <= playerAgent.stoppingDistance) {
			animator.SetBool ("Walk", false);
		} else {
			animator.SetBool ("Walk", true);
		}
	}

	void GetInteraction (){
		Ray interactionRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit interactionInfo; 
		if (Physics.Raycast (interactionRay, out interactionInfo, Mathf.Infinity, floorMask)){
			GameObject interactedObject = interactionInfo.collider.gameObject;
			if (interactedObject.tag == "Enemy") {
				interactedObject.GetComponent<Interactable> ().MoveToInteraction (playerAgent, attackRange);
			}
			else if (interactedObject.tag == "Interactable Object") {
				interactedObject.GetComponent<Interactable> ().MoveToInteraction (playerAgent, 1f);
			} else {
				playerAgent.stoppingDistance = 0f;
				playerAgent.SetDestination (interactionInfo.point);
				animator.SetBool("Walk", true);
			}
		}
	}
}
