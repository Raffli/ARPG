using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Networking;

public class PoisonousBlade : Skill {

	public override void SetProperties (Player player) {
		this.player = player;
		skillName = "Poisonous Blade";
		skillDescription = "You put poison on your blade and deal more damage.";
		skillIcon =  Resources.Load<Sprite> ("UI/Icons/poisonousBlade");
		skillSlot = 0;
		manaCost = 15;
		scale = 0.5f;
		duration = 5f;
		cooldownLeft = 0f;
		onCooldown = false;
		ModifyProperties ();
	}

	protected override void ModifyProperties () {
		baseDamage = Mathf.RoundToInt(player.dexterity.GetValue() * scale);
		damage = baseDamage;
		cooldown = 15f * (1 - player.cooldownReduction.GetValue ()/100);
	}

	public void GivePoisonDamage () {
		player.damage.AddBonus (baseDamage);
		StartCoroutine (WaitDuration());
	}

	public void RemovePoisonDamage () {
		player.damage.RemoveBonus (baseDamage);
	}

	IEnumerator WaitDuration () {
		yield return new WaitForSeconds (duration);
		RemovePoisonDamage ();
	}

    [Command]
    private void CmdSpawnIt(){
        GameObject poison = (GameObject)Resources.Load("Skills/PoisonousBlade");
		GivePoisonDamage ();
        GameObject obj = Instantiate(poison, GetComponent<NetworkTransform>().gameObject.transform.position, GetComponent<NetworkTransform>().gameObject.transform.rotation, GetComponent<NetworkTransform>().gameObject.transform);
        NetworkServer.Spawn(obj);
    }

    public override void Execute(){
		ModifyProperties ();
        CmdSpawnIt();
    }

		
}
