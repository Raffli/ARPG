using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventHandler : MonoBehaviour {

	public delegate void LevelUpHandler (int newLevel);
	public static event LevelUpHandler OnPlayerLevelUp;

	public static void LevelUp (int newLevel) {
		OnPlayerLevelUp (newLevel);
	}
}
