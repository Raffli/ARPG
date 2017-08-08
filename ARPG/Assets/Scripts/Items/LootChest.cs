using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootChest : MonoBehaviour, IInteract {
	
	public void Interact (){
		Animator objectAnimator = transform.GetComponent<Animator> ();
		objectAnimator.SetTrigger ("hasInteracted");
		// Spawn Loot
	}
}
