using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class RougeAttack : Attack {

	public GameObject leftSword;
	public GameObject rightSword;

	private SwordAttack leftSwordAttack;
	private SwordAttack rightSwordAttack;

    public override void EquipSkill()
    {
        leftSwordAttack = leftSword.GetComponent<SwordAttack>();
        rightSwordAttack = rightSword.GetComponent<SwordAttack>();

        skills[0] = gameObject.GetComponent<SparklingStrike>();
        skills[0].SetProperties(player, rightSword);
        skills[1] = gameObject.GetComponent<TwinBlades>();
        skills[1].SetProperties(player, leftSword, rightSword);
        skills[2] = gameObject.GetComponent<PoisonousBlade>();
        skills[2].SetProperties(player);
        skills[3] = gameObject.GetComponent<Stealth>();
        skills[3].SetProperties(player);
        skills[4] = gameObject.GetComponent<SweepingLotus>();
        skills[4].SetProperties(player);

        if (isLocalPlayer)
        {
			HUDManager.Instance.AddSkillToUI(skills[0].skillIcon, skills[0].skillSlot);
			HUDManager.Instance.AddSkillToUI(skills[1].skillIcon, skills[1].skillSlot);
			HUDManager.Instance.AddSkillToUI(skills[2].skillIcon, skills[2].skillSlot);
			HUDManager.Instance.AddSkillToUI(skills[3].skillIcon, skills[3].skillSlot);
			HUDManager.Instance.AddSkillToUI(skills[4].skillIcon, skills[4].skillSlot);
        }
    }

    private void RestartPlayerAgent() {
        if (!isLocalPlayer)
        {
            return;
        }
        playerAgent.isStopped = false;
	}

	private void DisableSwords() {
        if (!isLocalPlayer)
        {
            return;
        }
        leftSwordAttack.DisableSword ();
		rightSwordAttack.DisableSword ();
	}
		
}
