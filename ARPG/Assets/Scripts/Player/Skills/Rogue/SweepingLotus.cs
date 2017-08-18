using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Networking;

public class SweepingLotus : Skill {

	//private Player playerStats;

	public override void SetProperties () {
		//playerStats = player.GetComponent<Player> ();
		skillName = "Sweeping Lotus";
		skillDescription = "You spin with your blades creating a sweeping wind that deals damage to enemies.";
		skillIcon = (Sprite) Resources.Load ("UI/sweepingLotus");
		manaCost = 15;
		baseDamage = 8;
		damage = baseDamage;
		cooldown = 6f;
		cooldownLeft = 0f;
		onCooldown = false;
	}

    [Command]
    private void CmdSpawnIt()
    {
        GameObject lotus = (GameObject)Resources.Load("Skills/SweepingLotus");
        GameObject obj = Instantiate(lotus, GetComponent<NetworkTransform>().gameObject.transform.position, GetComponent<NetworkTransform>().gameObject.transform.rotation, GetComponent<NetworkTransform>().gameObject.transform);
        obj.GetComponent<SweepingLotusBehaviour>().SetDamage(damage);
        NetworkServer.Spawn(obj);
    }


    public override void Execute () {
        CmdSpawnIt();
    }
}
