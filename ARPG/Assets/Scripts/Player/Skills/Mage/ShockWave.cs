using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Networking;



public class ShockWave : Skill {

	public override void SetProperties(Player player) {
		this.player = player;
		skillName = "Shockwave";
		skillDescription = "You emit a shockwave around you that deals damage to any enemy it hits.";
		skillIcon =  Resources.Load<Sprite> ("UI/Icons/shockwave");
		skillSlot = 2;
		scale = 0.5f;
		manaCost = 20;
		cooldownLeft = 0f;
		onCooldown = false;
		ModifyProperties ();
	}

	protected override void ModifyProperties ()
	{
		baseDamage = Mathf.RoundToInt((player.intelligence.GetValue() + player.damage.GetValue()) * scale);
		damage = baseDamage;
		cooldown = 15f * (1 - player.cooldownReduction.GetValue ()/100);
	}

    [Command]
    private void CmdSpawnIt()
    {
        GameObject shockwave = (GameObject)Resources.Load("Skills/Shockwave");
        GameObject obj = Instantiate(shockwave, GetComponent<NetworkTransform>().gameObject.transform.position , GetComponent<NetworkTransform>().gameObject.transform.rotation,  GetComponent<NetworkTransform>().gameObject.transform);
		if (player.GetCritted ()) {
			damage = Mathf.RoundToInt (damage * player.critDamage);
		}
        obj.GetComponent<ShockWaveBehaviour>().SetDamage(damage);
        NetworkServer.Spawn(obj);
    }

    public override void Execute()
    {
		ModifyProperties ();
        CmdSpawnIt();
    }
}
