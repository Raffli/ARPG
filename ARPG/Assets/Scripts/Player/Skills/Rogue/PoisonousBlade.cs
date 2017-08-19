using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Networking;

public class PoisonousBlade : Skill {


	private Player playerStats;

	public override void SetProperties (Player player) {
		playerStats = player.GetComponent<Player> ();
		skillName = "Poisonous Blade";
		skillDescription = "You put poison on your blade and deal more damage.";
		skillIcon =  Resources.Load<Sprite> ("UI/Icons/poisonousBlade");
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

    [Command]
    private void CmdSpawnIt()
    {
        GameObject poison = (GameObject)Resources.Load("Skills/PoisonousBlade");
        GameObject obj = Instantiate(poison, GetComponent<NetworkTransform>().gameObject.transform.position, GetComponent<NetworkTransform>().gameObject.transform.rotation, GetComponent<NetworkTransform>().gameObject.transform);
        NetworkServer.Spawn(obj);
    }

    public override void Execute()
    {
        // add to player.armor or something
        CmdSpawnIt();
    }

		
}
