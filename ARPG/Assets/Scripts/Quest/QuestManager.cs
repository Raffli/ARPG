using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour {

	public static QuestManager Instance { get; set; } 

	public List <Quest> quests { get; set; }
	private int questsActive;
	private int maximumQuestsActive;

	public GameObject questPanel;
	private GameObject quest1, quest2, quest3;
	private Text quest1Title, quest2Title, quest3Title, quest1Task, quest2Task, quest3Task;

	void Awake () {
		DontDestroyOnLoad (transform.gameObject);

		if (Instance != null && Instance != this) {
			Destroy (gameObject);
		} else {
			Instance = this;
		}

		quests = new List <Quest> ();
		CreateQuests ();
		questsActive = 0;
		maximumQuestsActive = 3;

		quest1 = questPanel.transform.Find ("Quests").Find ("Quest1").gameObject;
		quest1Title = quest1.transform.Find ("Quest").GetComponent<Text> ();
		quest1Task = quest1.transform.Find ("Quest").Find ("Task").GetComponent<Text> ();
		quest2 = questPanel.transform.Find ("Quests").Find ("Quest2").gameObject; 
		quest2Title = quest2.transform.Find ("Quest").GetComponent<Text> ();
		quest2Task = quest2.transform.Find ("Quest").Find ("Task").GetComponent<Text> ();
		quest3 = questPanel.transform.Find ("Quests").Find ("Quest3").gameObject;
		quest3Title = quest3.transform.Find ("Quest").GetComponent<Text> ();
		quest3Task = quest3.transform.Find ("Quest").Find ("Task").GetComponent<Text> ();
	}

	void Update () {
		if (Input.GetButtonDown ("Quest")) {
			questPanel.SetActive (!questPanel.activeSelf);
		}
	}

	public void StartQuest (int index) {
		if (questsActive < maximumQuestsActive) {
			questsActive++;
			quests [index].active = true;
			quests [index].uiSlot = questsActive;
			DisplayQuest (index);
		}
	}

	public void FinishQuest (int index) {
		quests [index].done = true;
		quests [index].active = false;
		PlayerEventHandler.XpGained (quests [index].xp);
		if (quests [index].uiSlot == 1) {
			if (quest2.activeSelf) {
				quest1Title.text = quest2Title.text;
				quest1Task.text = quest2Task.text;
				if (quest3.activeSelf) {
					quest2Title.text = quest3Title.text;
					quest2Task.text = quest3Task.text;
					quest3.SetActive (false);
				} else {
					quest2.SetActive (false);
				}
			} else {
				quest1.SetActive (false);
			}
		} else if (quests [index].uiSlot == 2) {
			if (quest3.activeSelf) {
				quest2Title.text = quest3Title.text;
				quest2Task.text = quest3Task.text;
				quest3.SetActive (false);
			} else {
				quest2.SetActive (false);
			}
		} else if (quests [index].uiSlot == 3) {
			quest3.SetActive (false);
		} 
		questsActive--;
	}

	private void DisplayQuest (int index) {
		if (questsActive == 1) {
			quest1Title.text = quests [index].name;
			quest1Task.text = quests [index].task;
			quest1.SetActive (true);
		} else if (questsActive == 2) {
			quest2Title.text = quests [index].name;
			quest2Task.text = quests [index].task;
			quest2.SetActive (true);
		} else if (questsActive == 3) {
			quest3Title.text = quests [index].name;
			quest3Task.text = quests [index].task;
			quest3.SetActive (true);
		}
	}

	private void CreateQuests () {
		quests.Add (new Quest ("Save Shroudmere", "Save the people of Shroudmere from the monsters.", 100));
		quests.Add (new Quest ("Myculn Valley", "Go to Myculn Valley and talk to Lander Stormwind.", 300));
		quests.Add (new Quest ("The Troll King", "Find and defeat the Troll King", 1000));
	}
}
