using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Networking;

public class WaterCircle : Skill {


	public override void SetProperties (Player player) {
		skillName = "Water Circle";
		skillDescription = "You get sourrounded by water that deals damage to everything it comes in contact with.";
		skillIcon = (Sprite) Resources.Load ("UI/waterCircle");
		manaCost = 25;
		baseDamage = 10;
		damage = baseDamage;
		cooldown = 5f;
		cooldownLeft = 0f;
		onCooldown = false;
	}

    [Command]
    private void CmdSpawnIt()
    {
        GameObject waterCircle = (GameObject)Resources.Load("Skills/WaterCircle");
        GameObject obj = Instantiate(waterCircle, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform);
        obj.GetComponent<WaterCircleBehaviour>().SetDamage(damage);
        NetworkServer.Spawn(obj);
    }

    public override void Execute()
    {
        CmdSpawnIt();
    }


}
