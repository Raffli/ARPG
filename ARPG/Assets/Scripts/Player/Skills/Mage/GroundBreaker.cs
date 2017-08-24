using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Networking;


public class GroundBreaker : Skill {

	private int floorMask;

	public override void SetProperties (Player player) {
		this.player = player;
		skillName = "Ground Breaker";
		skillDescription = "You break the ground at the target location.";
		skillIcon =  Resources.Load<Sprite> ("UI/Icons/groundbreaker");
		skillSlot = 6;
		manaCost = 25;
		scale = 0.8f;
		cooldownLeft = 0f;
		onCooldown = false;
		ModifyProperties ();

		floorMask = LayerMask.GetMask ("Floor");
	}

	protected override void ModifyProperties (){
		baseDamage = Mathf.RoundToInt((player.intelligence.GetValue() + player.damage.GetValue()) * scale);
		damage = baseDamage;
		cooldown = 3f * (1 - player.cooldownReduction.GetValue ()/100);
	}

    [Command]
    private void CmdSpawnIt(Vector3 point) {
        GameObject groundBreaker = (GameObject)Resources.Load("Skills/GroundBreaker");
        GameObject obj = Instantiate(groundBreaker, point, transform.rotation);
        //obj.GetComponent<GroundBreakerBehaviour>().SetAttackingPlayer(gameObject);
		if (player.GetCritted ()) {
			damage = Mathf.RoundToInt (damage * player.critDamage);
		}
        obj.GetComponent<GroundBreakerBehaviour>().SetDamage(damage);
        NetworkServer.Spawn(obj);
    }


    public override void Execute (Vector3 targetPoint) {
		ModifyProperties ();
		Ray interactionRay = Camera.main.ScreenPointToRay (targetPoint);
		RaycastHit interactionInfo; 
		if (Physics.Raycast (interactionRay, out interactionInfo, Mathf.Infinity, floorMask)) {		
            CmdSpawnIt(interactionInfo.point);
        }
	}
}
