using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class SweepingLotus : Skill {

	private GameObject sweepingLotus;
	private Player playerStats;

	public override void SetProperties (Player player) {
		playerStats = player.GetComponent<Player> ();
		sweepingLotus = player.transform.Find ("SweepingLotus").gameObject;
		skillName = "Sweeping Lotus";
		skillDescription = "You spin with your blades creating a sweeping wind that deals damage to enemies.";
		skillIcon = (Sprite) Resources.Load ("UI/sweepingLotus");
		manaCost = 15;
		baseDamage = 8;
		damage = baseDamage;
		cooldown = 6f;
		cooldownLeft = 0f;
		onCooldown = false;
	}

	public override void CmdExecute () {
        sweepingLotus.SetActive (true);
        sweepingLotus.GetComponent<SweepingLotusBehaviour>().SetDamage(baseDamage);
    }
}
