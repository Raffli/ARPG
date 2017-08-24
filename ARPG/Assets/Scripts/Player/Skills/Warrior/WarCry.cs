using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Networking;

public class WarCry : Skill {

	public override void SetProperties (Player player) {
		this.player = player;
		skillName = "War Cry";
		skillDescription = "You cry out gaining strength and health for a few seconds.";
		skillIcon =  Resources.Load<Sprite> ("UI/Icons/warcry");
		skillSlot = 0;
		scale = 0.5f;
		manaCost = 15;
		cooldownLeft = 0f;
		onCooldown = false;
		duration = 5f;
		ModifyProperties ();
	}

	protected override void ModifyProperties ()
	{
		baseDamage = Mathf.RoundToInt(player.strength.GetValue() * scale);
		damage = Mathf.RoundToInt(player.health.GetValue() * scale);
		cooldown = 15f * (1 - player.cooldownReduction.GetValue ()/100);
	}

	public void GiveStrengthHealth () {
		player.strength.AddBonus (baseDamage);
		player.health.AddBonus (damage);
		player.Heal (damage);
		StartCoroutine (WaitDuration());
	}

	public void RemoveStrengthHealth () {
		player.strength.RemoveBonus (baseDamage);
		player.health.RemoveBonus (damage);
		player.ReduceHealth (0);
	}

	IEnumerator WaitDuration () {
		yield return new WaitForSeconds (duration);
		RemoveStrengthHealth ();
	}

    [Command]
    private void CmdSpawnIt()
    {
		GiveStrengthHealth ();
        GameObject warCry = (GameObject)Resources.Load("Skills/WarCry");
        GameObject obj = Instantiate(warCry, GetComponent<NetworkTransform>().gameObject.transform.position, GetComponent<NetworkTransform>().gameObject.transform.rotation, GetComponent<NetworkTransform>().gameObject.transform);
        NetworkServer.Spawn(obj);
    }

    public override void Execute()
    {
		ModifyProperties ();
        CmdSpawnIt();
    }
}
