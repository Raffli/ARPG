using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorldInteraction : MonoBehaviour {

	private NavMeshAgent playerAgent;
	private Animator animator;
	private int floorMask;
	private int interactionMask;
	private int enemyMask;

	public float attackRange;

	void Start () {
		playerAgent = GetComponent<NavMeshAgent> ();
		animator = GetComponent<Animator> ();
		floorMask = LayerMask.GetMask ("Floor");
		enemyMask = LayerMask.GetMask ("Enemy");
		interactionMask = LayerMask.GetMask ("Interactable");
	}
	
	void Update () {
		if (Input.GetButtonDown ("Fire1") /*&& !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()*/) {
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
		if (Physics.Raycast (interactionRay, out interactionInfo, Mathf.Infinity, enemyMask)) {
			GameObject interactedObject = interactionInfo.collider.gameObject;
			interactedObject.GetComponent<Interactable> ().MoveToInteraction (playerAgent, attackRange);
		} else if (Physics.Raycast (interactionRay, out interactionInfo, Mathf.Infinity, interactionMask)){
			GameObject interactedObject = interactionInfo.collider.gameObject;
			interactedObject.GetComponent<Interactable> ().MoveToInteraction (playerAgent, 8f);
		} else if (Physics.Raycast (interactionRay, out interactionInfo, Mathf.Infinity, floorMask)){
			playerAgent.stoppingDistance = 0f;
			playerAgent.SetDestination (interactionInfo.point);
			animator.SetBool("Walk", true);
		}
	}
}
