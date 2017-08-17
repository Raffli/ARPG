using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class LastStand : Skill {

	private GameObject lastStand;
	private Player playerStats;

	public override void SetProperties (Player player) {
		playerStats = player.GetComponent<Player> ();
		lastStand = player.transform.Find ("LastStand").gameObject;
		skillName = "Last Stand";
		skillDescription = "You concentrate all your energy becoming invicible for the next x seconds.";
		skillIcon = (Sprite) Resources.Load ("UI/lastStand");
		manaCost = 15;
		baseDamage = 0;
		damage = baseDamage;
		cooldown = 6f;
		cooldownLeft = 0f;
		onCooldown = false;
	}

	public override void CmdExecute () {
		lastStand.SetActive (true);
	}
}
