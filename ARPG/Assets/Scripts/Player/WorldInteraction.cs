using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class WorldInteraction : NetworkBehaviour
{

	private NavMeshAgent playerAgent;
	private Animator animator;
	private int floorMask;
	private int interactionMask;
	private int enemyMask;
	private bool canInteract;

	public float attackRange;

	void Start () {
		playerAgent = GetComponent<NavMeshAgent> ();
		animator = GetComponent<Animator> ();
		floorMask = LayerMask.GetMask ("Floor");
		enemyMask = LayerMask.GetMask ("Enemy");
		interactionMask = LayerMask.GetMask ("Interactable");
		canInteract = true;
	}
	
	void Update () {
        if (!isLocalPlayer)
        {
            return;
        }
        if (canInteract) {
            if (Input.GetButtonDown("Fire1")) { //&& !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) {
				GetInteraction ();
			}
		}

		if (playerAgent.remainingDistance <= playerAgent.stoppingDistance) {
			animator.SetBool ("Walk", false);
		} else {
			animator.SetBool ("Walk", true);
		}
	}

    [Command]
    public void CmdSetCanInteract(bool canInteract)
    {
        this.canInteract = canInteract;
    }

    void GetInteraction (){
		Ray interactionRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit interactionInfo; 
		if (Physics.Raycast (interactionRay, out interactionInfo, Mathf.Infinity, enemyMask)) {
			GameObject interactedObject = interactionInfo.collider.gameObject;
			interactedObject.GetComponent<Interactable> ().MoveToInteraction (playerAgent, attackRange);
		} else if (Physics.Raycast (interactionRay, out interactionInfo, Mathf.Infinity, interactionMask)){
			canInteract = false;
			GameObject interactedObject = interactionInfo.collider.gameObject;
			interactedObject.GetComponent<Interactable> ().MoveToInteraction (playerAgent, 8f);
		} else if (Physics.Raycast (interactionRay, out interactionInfo, Mathf.Infinity, floorMask)){
			playerAgent.stoppingDistance = 0.3f;
			playerAgent.SetDestination (interactionInfo.point);
			animator.SetBool("Walk", true);
		}
	}


}
