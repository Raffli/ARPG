using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Networking;


public class Shield : Skill {
	
	private Player playerStats;

	public override void SetProperties(Player player) {

		playerStats = player.GetComponent<Player> ();
        skillName = "Protective Aura";
		skillDescription = "You concentrate your magic energy to generate a protective aura taht shields you.";
		skillIcon = (Sprite) Resources.Load ("UI/protectingAura");
		manaCost = 15;
		baseDamage = 0;
		damage = baseDamage;
		cooldown = 6f;
		cooldownLeft = 0f;
		onCooldown = false;
	}

    [Command]
    private void CmdSpawnIt()
    {
        GameObject shield = (GameObject)Resources.Load("Skills/Shield");
        GameObject obj = Instantiate(shield, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform);
        NetworkServer.Spawn(obj);
    }

    public override void Execute() {
        // add to player.armor or something
        CmdSpawnIt();
    }
}
