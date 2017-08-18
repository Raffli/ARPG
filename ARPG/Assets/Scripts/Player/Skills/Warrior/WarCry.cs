﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Networking;

public class WarCry : Skill {

	private Player playerStats;

	public override void SetProperties (Player player) {
		playerStats = player.GetComponent<Player> ();
		skillName = "War Cry";
		skillDescription = "You cry out gaining strength and health for x seconds.";
		skillIcon = (Sprite) Resources.Load ("UI/warcry");
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
        GameObject warCry = (GameObject)Resources.Load("Skills/WarCry");
        GameObject obj = Instantiate(warCry, GetComponent<NetworkTransform>().gameObject.transform.position, GetComponent<NetworkTransform>().gameObject.transform.rotation, GetComponent<NetworkTransform>().gameObject.transform);
        NetworkServer.Spawn(obj);
    }

    public override void Execute()
    {
        // add to player.armor or something
        CmdSpawnIt();
    }
}
