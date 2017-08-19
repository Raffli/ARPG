using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Networking;


public class Shield : Skill {
	
	private Player playerStats;

	public override void SetProperties(Player player) {
		playerStats = player.GetComponent<Player> ();
        skillName = "Protective Aura";
		skillDescription = "You concentrate your magic energy to generate a protective aura taht shields you.";
		skillIcon =  Resources.Load<Sprite> ("UI/Icons/protectingAura");
		manaCost = 15;
		baseDamage = 0;
		damage = baseDamage;
		cooldown = 6f;
		cooldownLeft = 0f;
		onCooldown = false;
	}

	void Update () {
		if (onCooldown) {
			cooldownLeft -= Time.deltaTime;
			HUDManager.Instance.UpdateCooldown (0, cooldownLeft, cooldown);
			if (cooldownLeft <= 0) {
				onCooldown = false;
			}
		}
	}

	public void StartCooldown () {
		onCooldown = true;
		cooldownLeft = cooldown;
	}


    [Command]
    private void CmdSpawnIt()
    {
        GameObject shield = (GameObject)Resources.Load("Skills/Shield");
        GameObject obj = Instantiate(shield, GetComponent<NetworkTransform>().gameObject.transform.position, GetComponent<NetworkTransform>().gameObject.transform.rotation, GetComponent<NetworkTransform>().gameObject.transform);
        NetworkServer.Spawn(obj);
    }

    public override void Execute() {
        // add to player.armor or something
        CmdSpawnIt();
    }
}
