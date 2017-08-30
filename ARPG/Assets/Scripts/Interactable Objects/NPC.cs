using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable {

	private WorldInteraction worldInteraction;
	public string[] dialogue;
	public string name;

	protected override void Interact (Transform player) {
		worldInteraction = player.GetComponent<WorldInteraction> ();
		DialogueManager.Instance.AddNewDialogue (dialogue, name);
		DialogueManager.OnDialogueEnded += ResetPlayerInteraction;
	}

	public void ResetPlayerInteraction () {
		worldInteraction.SetCanInteract (true);
		DialogueManager.OnDialogueEnded -= ResetPlayerInteraction;
	}


}
