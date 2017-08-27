using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat {

	public int baseValue { get; set; }
	public int statBonus { get; set; }
	public int finalValue { get; set; }
	public string statName { get; set; }
	public string statDescription { get; set; }

	public Stat (int baseValue, string statName, string statDescription) {
		this.baseValue = baseValue;
		this.statName = statName;
		this.statDescription = statDescription;
		statBonus = 0;
	}

	public void AddBonus (int bonus) {
		Debug.Log (bonus + " " + statName + " added!");
		statBonus += bonus;
	}

	public void RemoveBonus (int bonus) {
		Debug.Log (bonus + " " + statName + " removed!");
		statBonus -= bonus;
	}

	public int GetValue () {
		finalValue = baseValue + statBonus;
		return finalValue;
	}
}

