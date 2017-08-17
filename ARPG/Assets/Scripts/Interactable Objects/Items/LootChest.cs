using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootChest : MonoBehaviour, IInteract {

	private Transform player;
	
	public void Interact (Transform player){
		this.player = player;
		Animator objectAnimator = transform.GetComponent<Animator> ();
		objectAnimator.SetTrigger ("hasInteracted");
		// Spawn Loot
	}

	private void ResetPlayerCanInteract () {
		player.GetComponent<WorldInteraction> ().CmdSetCanInteract(true);
	}
}
