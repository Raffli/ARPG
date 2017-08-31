using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPC : Interactable {

	private WorldInteraction worldInteraction;
	public string[] dialogue;
	public string name;
	public bool startsQuest;
	public bool finishesQuest;
	public int startQuestIndex;
	public int finishQuestIndex;
	public bool endsGame;


	protected override void Interact (Transform player) {
		worldInteraction = player.GetComponent<WorldInteraction> ();
		DialogueManager.Instance.AddNewDialogue (dialogue, name);
		if (finishesQuest) {
			FinishQuest ();
		}
		DialogueManager.OnDialogueEnded += ResetPlayerInteraction;
		if (startsQuest) {
			DialogueManager.OnDialogueEnded += StartQuest;
		}
		if (endsGame) {
			DialogueManager.OnDialogueEnded += EndGame;
		}
	}

	public void EndGame () {
		StartCoroutine (Wait ());
	}

	IEnumerator Wait () {
		yield return new WaitForSeconds (5f);
		SceneManager.LoadScene ("Lobby");
	}

	public void StartQuest () {
		QuestManager.Instance.StartQuest (startQuestIndex);
		DialogueManager.OnDialogueEnded -= StartQuest;
	}

	public void FinishQuest () {
		QuestManager.Instance.FinishQuest (finishQuestIndex);
	}

	public void ResetPlayerInteraction () {
		worldInteraction.SetCanInteract (true);
		DialogueManager.OnDialogueEnded -= ResetPlayerInteraction;
	}


}
