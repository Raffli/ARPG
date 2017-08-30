using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public static DialogueManager Instance { get; set; }
	public delegate void DialogueEndHandler ();
	public static event DialogueEndHandler OnDialogueEnded;

	public GameObject dialoguePanel;
	private Text npcName, dialogue;
	private Button continueButton;
	private int dialogueIndex;
	private List <string> dialogueLines;

	void Awake () {
		DontDestroyOnLoad (transform.gameObject);

		if (Instance != null && Instance != this) {
			Destroy (gameObject);
		} else {
			Instance = this;
		}

		npcName = dialoguePanel.transform.Find ("NPCName").GetComponent<Text> ();
		dialogue = dialoguePanel.transform.Find ("Dialogue").GetComponent<Text> ();
		/*continueButton = dialoguePanel.transform.Find ("ContinueButton").GetComponent<Button> ();
		continueButton.onClick.AddListener (delegate {
			ContinueDialogue ();
		});*/
	}

	public void AddNewDialogue (string [] lines, string npcName) {
		dialogueIndex = 0;
		dialogueLines = new List <string> ();
		dialogueLines.AddRange (lines);
		this.npcName.text = npcName;
		CreateDialogue ();
	}

	public void CreateDialogue () {
		dialogue.text = dialogueLines [dialogueIndex];
		dialoguePanel.SetActive (true);
	}

	public void ContinueDialogue () {
		Debug.Log ("continue dialogue");
		if (dialogueIndex < dialogueLines.Count - 1) {
			dialogueIndex++;
			dialogue.text = dialogueLines [dialogueIndex];
		} else {
			dialoguePanel.SetActive (false);
			OnDialogueEnded ();
		}
	}
		
}
