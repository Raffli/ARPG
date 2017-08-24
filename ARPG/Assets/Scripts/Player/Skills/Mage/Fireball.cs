using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Fireball : Skill {

	public override void SetProperties(Player player) {
		this.player = player;
		skillName = "Fireball";
		skillDescription = "You create a fireball that is thrown at the target and deals damage.";
		manaCost = 0;
		skillIcon =  Resources.Load<Sprite> ("UI/Icons/fireball");
		skillSlot = 5;
		scale = 0.5f;
		cooldownLeft = 0f;
		onCooldown = false;
		ModifyProperties ();
	}

	protected override void ModifyProperties (){
		baseDamage = Mathf.RoundToInt((player.intelligence.GetValue() + player.damage.GetValue()) * scale);
		damage = baseDamage;
		cooldown = 1.5f * (1 - player.cooldownReduction.GetValue ()/100);
	}

    [Command]
    private void CmdSpawnIt(Vector3 spawnPoint, Vector3 targetPoint, Vector3 toTarget)
    {
        GameObject fireball = (GameObject)Resources.Load("Skills/Fireball");
        GameObject obj = Instantiate(fireball, spawnPoint, Quaternion.LookRotation(toTarget));
        obj.GetComponent<FireballBehaviour>().SetAttackingPlayer(gameObject);
		if (player.GetCritted ()) {
			damage = Mathf.RoundToInt (damage * player.critDamage);
		}
        obj.GetComponent<FireballBehaviour>().SetFireballDamage(damage);
        NetworkServer.Spawn(obj);
    }

    public override void Execute(GameObject enemy, GameObject spellOrigin)
    {
		ModifyProperties ();
        Vector3 spawnPoint = spellOrigin.transform.position;
        Vector3 targetPoint = enemy.transform.position;
        Vector3 toTarget = targetPoint - spawnPoint;
        CmdSpawnIt(spawnPoint, targetPoint, toTarget);
    }
}
