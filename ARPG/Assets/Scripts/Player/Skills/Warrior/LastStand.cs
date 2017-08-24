using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Networking;

public class LastStand : Skill {

	private int armorBonus;

	public override void SetProperties (Player player) {
		this.player = player;
		skillName = "Last Stand";
		skillDescription = "You concentrate all your energy becoming invicible for a short period.";
		skillIcon =  Resources.Load<Sprite> ("UI/Icons/lastStand");
		skillSlot = 1;
		manaCost = 25;
		cooldownLeft = 0f;
		onCooldown = false;
		duration = 5f;
		armorBonus = 999999999;
		ModifyProperties ();
	}

	protected override void ModifyProperties ()
	{
		cooldown = 30f * (1 - player.cooldownReduction.GetValue ()/100);
	}

	public void MakeInvicible () {
		player.armor.AddBonus (armorBonus);
		StartCoroutine (WaitDuration());
	}

	public void RemoveInvicibility () {
		player.armor.RemoveBonus (armorBonus);
	}

	IEnumerator WaitDuration () {
		yield return new WaitForSeconds (duration);
		RemoveInvicibility ();
	}

    [Command]
    private void CmdSpawnIt()
    {
		MakeInvicible ();
        GameObject lastStand = (GameObject)Resources.Load("Skills/LastStand");
        GameObject obj = Instantiate(lastStand, GetComponent<NetworkTransform>().gameObject.transform.position, GetComponent<NetworkTransform>().gameObject.transform.rotation, GetComponent<NetworkTransform>().gameObject.transform);
        NetworkServer.Spawn(obj);
    }

    public override void Execute()
    {
		ModifyProperties ();
        CmdSpawnIt();
    }
}
