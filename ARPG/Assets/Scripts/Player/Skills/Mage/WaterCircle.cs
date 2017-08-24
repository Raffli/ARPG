using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Networking;

public class WaterCircle : Skill {

	public override void SetProperties (Player player) {
		this.player = player;
		skillName = "Water Circle";
		skillDescription = "You get sourrounded by water that deals damage to everything it comes in contact with.";
		skillIcon =  Resources.Load<Sprite> ("UI/Icons/waterCircle");
		skillSlot = 1;
		scale = 0.3f;
		manaCost = 25;
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
        GameObject waterCircle = (GameObject)Resources.Load("Skills/WaterCircle");
        GameObject obj = Instantiate(waterCircle, GetComponent<NetworkTransform>().gameObject.transform.position, GetComponent<NetworkTransform>().gameObject.transform.rotation, GetComponent<NetworkTransform>().gameObject.transform);
		if (player.GetCritted ()) {
			damage = Mathf.RoundToInt (damage * player.critDamage);
		}
        obj.GetComponent<WaterCircleBehaviour>().SetDamage(damage);
        NetworkServer.Spawn(obj);
    }

    public override void Execute()
    {
		ModifyProperties ();
        CmdSpawnIt();
    }


}
