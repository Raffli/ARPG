using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Networking;


public class Shield : Skill {

	public override void SetProperties(Player player) {
		this.player = player;
        skillName = "Protective Aura";
		skillDescription = "You concentrate your magic energy to generate a protective aura taht shields you.";
		skillIcon =  Resources.Load<Sprite> ("UI/Icons/protectingAura");
		skillSlot = 0;
		manaCost = 15;
		scale = 0.5f;
		cooldownLeft = 0f;
		onCooldown = false;
		duration = 5f;
		ModifyProperties ();
	}

	protected override void ModifyProperties (){
		baseDamage = Mathf.RoundToInt (player.intelligence.GetValue () * scale);
		damage = baseDamage;
		cooldown = 15f * (1 - player.cooldownReduction.GetValue ()/100);
	}

	public void GiveArmor () {
		player.armor.AddBonus (baseDamage);
		StartCoroutine (WaitDuration());
	}

	public void RemoveArmor () {
		player.armor.RemoveBonus (baseDamage);
	}

	IEnumerator WaitDuration () {
		yield return new WaitForSeconds (duration);
		RemoveArmor ();
	}


    [Command]
    private void CmdSpawnIt()
    {
        GameObject shield = (GameObject)Resources.Load("Skills/Shield");
		GiveArmor ();
        GameObject obj = Instantiate(shield, GetComponent<NetworkTransform>().gameObject.transform.position, GetComponent<NetworkTransform>().gameObject.transform.rotation, GetComponent<NetworkTransform>().gameObject.transform);
        NetworkServer.Spawn(obj);
    }

    public override void Execute() {
		ModifyProperties ();
        CmdSpawnIt();
    }
}
