using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Networking;

public class Stealth : Skill {

	public override void SetProperties (Player player) {
		this.player = player;
		skillName = "Stealth";
		skillDescription = "You concentrate and become invisible to enemies for a while.";
		skillIcon =  Resources.Load<Sprite> ("UI/Icons/stealth");
		skillSlot = 1;
		manaCost = 15;
		scale = 0.7f;
		cooldownLeft = 0f;
		onCooldown = false;
		duration = 5f;
		ModifyProperties ();
	}

	protected override void ModifyProperties (){
		baseDamage = Mathf.RoundToInt(player.dexterity.GetValue() * scale);
		damage = baseDamage;
		cooldown = 15f * (1f - player.cooldownReduction.GetValue ()/100f);
	}

	public void GiveCritChance () {
		player.critChance.AddBonus (baseDamage);
		CharacterManager.Instance.UpdateStats ();
		player.invisible = true;
		StartCoroutine (WaitDuration());
	}

	public void RemoveCritChance () {
		player.critChance.RemoveBonus (baseDamage);
		CharacterManager.Instance.UpdateStats ();
		player.invisible = false;
	}

	IEnumerator WaitDuration () {
		yield return new WaitForSeconds (duration);
		RemoveCritChance ();
	}

    [Command]
    private void CmdSpawnIt()
    {
        GameObject stealth = (GameObject)Resources.Load("Skills/Stealth");
		GiveCritChance ();
        GameObject obj = Instantiate(stealth, GetComponent<NetworkTransform>().gameObject.transform.position, GetComponent<NetworkTransform>().gameObject.transform.rotation, GetComponent<NetworkTransform>().gameObject.transform);
        NetworkServer.Spawn(obj);
    }

    public override void Execute()
    {
        CmdSpawnIt();
    }
}
