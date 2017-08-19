using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Networking;



public class ShockWave : Skill {



	public override void SetProperties() {
		skillName = "Shockwave";
		skillDescription = "You emit a shockwave around you that deals damage to any enemy it hits.";
		skillIcon =  Resources.Load<Sprite> ("UI/Icons/shockwave");
		manaCost = 20;
		baseDamage = 15;
		damage = baseDamage;
		cooldown = 4f;
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

    [Command]
    private void CmdSpawnIt()
    {
        GameObject shockwave = (GameObject)Resources.Load("Skills/Shockwave");
        GameObject obj = Instantiate(shockwave, GetComponent<NetworkTransform>().gameObject.transform.position , GetComponent<NetworkTransform>().gameObject.transform.rotation,  GetComponent<NetworkTransform>().gameObject.transform);
        obj.GetComponent<ShockWaveBehaviour>().SetDamage(damage);
        NetworkServer.Spawn(obj);
    }

    public override void Execute()
    {
        CmdSpawnIt();
    }
}
