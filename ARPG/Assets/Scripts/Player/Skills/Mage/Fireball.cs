using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Fireball : Skill {

	public override void SetProperties() {
		skillName = "Fireball";
		skillDescription = "You create a fireball that is thrown at the target and deals damage.";
		skillIcon = (Sprite) Resources.Load ("UI/fireball");
		manaCost = 0;
		skillIcon =  Resources.Load<Sprite> ("UI/Icons/fireball");
		baseDamage = 10;
		damage = baseDamage;
		cooldown = 2f;
		cooldownLeft = 0f;
		onCooldown = false;
	}

	void Update () {
		if (onCooldown) {
			cooldownLeft -= Time.deltaTime;
			HUDManager.Instance.UpdateCooldown (5, cooldownLeft, cooldown);
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
    private void CmdSpawnIt(Vector3 spawnPoint, Vector3 targetPoint, Vector3 toTarget)
    {
        GameObject fireball = (GameObject)Resources.Load("Skills/Fireball");
        GameObject obj = Instantiate(fireball, spawnPoint, Quaternion.LookRotation(toTarget));
        obj.GetComponent<FireballBehaviour>().SetAttackingPlayer(gameObject);
        obj.GetComponent<FireballBehaviour>().SetFireballDamage(damage);
        print("damage fireball" + damage);
        NetworkServer.Spawn(obj);
    }

    public override void Execute(NavMeshAgent playerAgent, GameObject enemy, GameObject spellOrigin)
    {
        Vector3 spawnPoint = spellOrigin.transform.position;
        Vector3 targetPoint = enemy.transform.position;
        Vector3 toTarget = targetPoint - spawnPoint;
        CmdSpawnIt(spawnPoint, targetPoint, toTarget);
    }
}
