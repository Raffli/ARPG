using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Networking;

public class SweepingLotus : Skill {

	public override void SetProperties (Player player) {
		this.player = player;
		skillName = "Sweeping Lotus";
		skillDescription = "You spin with your blades creating a sweeping wind that deals damage to enemies.";
		skillIcon =  Resources.Load<Sprite> ("UI/Icons/sweepingLotus");
		skillSlot = 2;
		manaCost = 25;
		scale = 0.2f;
		cooldownLeft = 0f;
		onCooldown = false;
		ModifyProperties ();
	}

	protected override void ModifyProperties (){
		baseDamage = Mathf.RoundToInt((player.dexterity.GetValue() + player.damage.GetValue()) * scale);
		damage = baseDamage;
		cooldown = 15f * (1f - player.cooldownReduction.GetValue ()/100f);
	}

    [Command]
    private void CmdSpawnIt()
    {
        GameObject lotus = (GameObject)Resources.Load("Skills/SweepingLotus");
        GameObject obj = Instantiate(lotus, GetComponent<NetworkTransform>().gameObject.transform.position, GetComponent<NetworkTransform>().gameObject.transform.rotation, GetComponent<NetworkTransform>().gameObject.transform);
		if (player.GetCritted ()) {
			damage = Mathf.RoundToInt (damage * player.critDamage);
		}
        obj.GetComponent<SweepingLotusBehaviour>().SetDamage(damage);
        NetworkServer.Spawn(obj);
    }

    public override void Execute () {
        CmdSpawnIt();
    }
}
