﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Networking;

public class GroundBreaker : Skill {

	private int floorMask;

	public override void SetProperties () {
		skillName = "Ground Breaker";
		skillDescription = "You break the ground at the target location.";
		skillIcon = (Sprite) Resources.Load ("UI/groundbreaker");
		manaCost = 25;
		baseDamage = 20;
		damage = baseDamage;
		cooldown = 3f;
		cooldownLeft = 0f;
		onCooldown = false;

		floorMask = LayerMask.GetMask ("Floor");
	}

    [Command]
    private void CmdSpawnIt(Vector3 point) {
        GameObject groundBreaker = (GameObject)Resources.Load("Skills/GroundBreaker");
        GameObject obj = Instantiate(groundBreaker, point, transform.rotation);
        //obj.GetComponent<GroundBreakerBehaviour>().SetPlayerAgent(playerAgent);
        obj.GetComponent<GroundBreakerBehaviour>().SetDamage(damage);
        NetworkServer.Spawn(obj);
    }

    public override void Execute (NavMeshAgent playerAgent, Vector3 targetPoint) {

		Ray interactionRay = Camera.main.ScreenPointToRay (targetPoint);
		RaycastHit interactionInfo; 
		if (Physics.Raycast (interactionRay, out interactionInfo, Mathf.Infinity, floorMask)) {		
            CmdSpawnIt(interactionInfo.point);
        }
	}
}
