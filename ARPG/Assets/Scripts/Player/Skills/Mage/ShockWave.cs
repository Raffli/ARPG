﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Networking;


public class ShockWave : Skill {


	public override void SetProperties(Player player) {
		skillName = "Shockwave";
		skillDescription = "You emit a shockwave around you that deals damage to any enemy it hits.";
		skillIcon = (Sprite) Resources.Load ("UI/shockwave");
		manaCost = 20;
		baseDamage = 15;
		damage = baseDamage;
		cooldown = 4f;
		cooldownLeft = 0f;
		onCooldown = false;
	}


    [Command]
    private void CmdSpawnIt()
    {
        GameObject shockwave = (GameObject)Resources.Load("Skills/Shockwave");
        GameObject obj = Instantiate(shockwave, gameObject.transform.position , gameObject.transform.rotation,  gameObject.transform);
        obj.GetComponent<ShockWaveBehaviour>().SetDamage(damage);
        NetworkServer.Spawn(obj);
    }

    public override void Execute()
    {
        CmdSpawnIt();
    }
}
