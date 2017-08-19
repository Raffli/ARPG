using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Networking;

public class FireFissure : Skill {

	//private Player playerStats;

	public override void SetProperties () {
		//playerStats = player.GetComponent<Player> ();
		skillName = "Fire Fissure";
		skillDescription = "You smash the ground causing the ground to erupt in fire.";
		skillIcon =  Resources.Load<Sprite> ("UI/Icons/fireFissure");
		manaCost = 15;
		baseDamage = 20;
		damage = baseDamage;
		cooldown = 6f;
		cooldownLeft = 0f;
		onCooldown = false;
	}

	void Update () {
		if (onCooldown) {
			cooldownLeft -= Time.deltaTime;
			HUDManager.Instance.UpdateCooldown (2, cooldownLeft, cooldown);
			if (cooldownLeft <= 0) {
				onCooldown = false;
			}
		}
	}

	public void StartCooldown () {
		onCooldown = true;
		cooldownLeft = cooldown;
	}


    private void CmdSpawnIt(Vector3 spawnPoint, Quaternion spawnRotation)
    {
        GameObject fireFissure = (GameObject)Resources.Load("Skills/FireFissure");
        GameObject obj = Instantiate(fireFissure, spawnPoint, spawnRotation);
        obj.GetComponent<FireFissureBehaviour>().SetFireFissureDamage(damage);

        NetworkServer.Spawn(obj);
    }

    public override void Execute (GameObject spellOrigin) {
        Vector3 spawnPoint = spellOrigin.transform.position;
        Quaternion spawnRotation = spellOrigin.transform.rotation;
        CmdSpawnIt(spawnPoint, spawnRotation);
    }
}
