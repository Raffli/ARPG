using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Networking;

public class WaterCircle : Skill {


	public override void SetProperties () {
		skillName = "Water Circle";
		skillDescription = "You get sourrounded by water that deals damage to everything it comes in contact with.";
		skillIcon =  Resources.Load<Sprite> ("UI/Icons/waterCircle");
		manaCost = 25;
		baseDamage = 10;
		damage = baseDamage;
		cooldown = 5f;
		cooldownLeft = 0f;
		onCooldown = false;
	}

	void Update () {
		if (onCooldown) {
			cooldownLeft -= Time.deltaTime;
			HUDManager.Instance.UpdateCooldown (1, cooldownLeft, cooldown);
			if (cooldownLeft <= 0) {
				onCooldown = false;
			}
		}
	}

    [Command]
    private void CmdSpawnIt()
    {
        GameObject waterCircle = (GameObject)Resources.Load("Skills/WaterCircle");
        GameObject obj = Instantiate(waterCircle, GetComponent<NetworkTransform>().gameObject.transform.position, GetComponent<NetworkTransform>().gameObject.transform.rotation, GetComponent<NetworkTransform>().gameObject.transform);
        obj.GetComponent<WaterCircleBehaviour>().SetDamage(damage);
        NetworkServer.Spawn(obj);
    }

    public override void Execute()
    {
        CmdSpawnIt();
    }


}
