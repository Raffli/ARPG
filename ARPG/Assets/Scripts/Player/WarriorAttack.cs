using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class WarriorAttack : Attack {

    public GameObject sword;
	private SwordAttack swordAttack;
	public GameObject spellOrigin;

    public override void EquipSkill()
    {
        swordAttack = sword.GetComponent<SwordAttack>();

        skills[0] = gameObject.GetComponent<Bash>();
        skills[0].SetProperties(player, sword);
        skills[1] = gameObject.GetComponent<Bladestorm>();
        skills[1].SetProperties(player, sword);
        skills[2] = gameObject.GetComponent<WarCry>();
        skills[2].SetProperties(player);
        skills[3] = gameObject.GetComponent<LastStand>();
        skills[3].SetProperties(player);
        skills[4] = gameObject.GetComponent<FireFissure>();
        skills[4].SetProperties(player);

        if (isLocalPlayer) {
			HUDManager.Instance.AddSkillToUI(skills[0].skillIcon, skills[0].skillSlot);
			HUDManager.Instance.AddSkillToUI(skills[1].skillIcon, skills[1].skillSlot);
			HUDManager.Instance.AddSkillToUI(skills[2].skillIcon, skills[2].skillSlot);
			HUDManager.Instance.AddSkillToUI(skills[3].skillIcon, skills[3].skillSlot);
			HUDManager.Instance.AddSkillToUI(skills[4].skillIcon, skills[4].skillSlot);
        }

    }

	// Implements other "Cast"-Functions from Attack, overrides thirdSpell because of different execute

    protected override void CastThirdSpell()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        GetComponent<Animator>().SetBool("UsedThirdSpell", false);
        skills[4].Execute(spellOrigin);
        playerAgent.isStopped = false;
        worldInteraction.SetCanInteract(true);
    }

    private void RestartPlayerAgent() {
        if (!isLocalPlayer)
        {
            return;
        }
        playerAgent.isStopped = false;
		swordAttack.DisableSword ();
	}
}
