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
	private GameObject destination;
	public float attackRange;
	private Quaternion destinationRotation;

	void Start () {
		playerAgent = GetComponent<NavMeshAgent> ();
		animator = GetComponent<Animator> ();
		floorMask = LayerMask.GetMask ("Floor");
		enemyMask = LayerMask.GetMask ("Enemy");
		interactionMask = LayerMask.GetMask ("Interactable");
		canInteract = true;
		destination = Resources.Load<GameObject> ("UI/Destination");
		destinationRotation = Quaternion.Euler (90, 0, 0);
	}
	
	void Update () {
        if (!isLocalPlayer)
        {
            return;
        }
        if (canInteract) {
			if (Input.GetButton("Fire1")) {
				if (InventoryManager.Instance.GetInventoryAcitve ()) {
					if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject ()) {
						GetInteraction (Input.GetButtonDown ("Fire1"));
					}
				} else {
					GetInteraction (Input.GetButtonDown ("Fire1"));
				}
			}
		}

		if (playerAgent.remainingDistance <= playerAgent.stoppingDistance) {
			animator.SetBool ("Walk", false);
		} else {
			animator.SetBool ("Walk", true);
		}
	}

    public void SetCanInteract(bool canInteract)
    {
        this.canInteract = canInteract;
    }

    void GetInteraction (bool buttonDown){
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

			if (buttonDown) {
				Instantiate (destination, interactionInfo.point + new Vector3(0,0.08f,0), destinationRotation);
			}
			playerAgent.stoppingDistance = 0.3f;
			playerAgent.SetDestination (interactionInfo.point);
			animator.SetBool("Walk", true);
		}
	}


}
