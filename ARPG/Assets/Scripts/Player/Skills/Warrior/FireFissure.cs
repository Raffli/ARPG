using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Networking;

public class FireFissure : Skill {

	public override void SetProperties (Player player) {
		this.player = player;
		skillName = "Fire Fissure";
		skillDescription = "You smash the ground causing the ground to erupt in fire.";
		skillIcon =  Resources.Load<Sprite> ("UI/Icons/fireFissure");
		skillSlot = 2;
		manaCost = 15;
		scale = 1.3f;
		cooldownLeft = 0f;
		onCooldown = false;
		ModifyProperties ();
	}

	protected override void ModifyProperties ()
	{
		baseDamage = Mathf.RoundToInt((player.strength.GetValue() + player.damage.GetValue()) * scale);
		damage = baseDamage;
		cooldown = 10f * (1 - player.cooldownReduction.GetValue ()/100);
	}


    private void CmdSpawnIt(Vector3 spawnPoint, Quaternion spawnRotation)
    {
        GameObject fireFissure = (GameObject)Resources.Load("Skills/FireFissure");
        GameObject obj = Instantiate(fireFissure, spawnPoint, spawnRotation);
		if (player.GetCritted ()) {
			damage = Mathf.RoundToInt (damage * player.critDamage);
		}
        obj.GetComponent<FireFissureBehaviour>().SetFireFissureDamage(damage);
        NetworkServer.Spawn(obj);
    }

    public override void Execute (GameObject spellOrigin) {
		ModifyProperties ();
        Vector3 spawnPoint = spellOrigin.transform.position;
        Quaternion spawnRotation = spellOrigin.transform.rotation;
        CmdSpawnIt(spawnPoint, spawnRotation);
    }
}
