using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class WarriorAttack : Attack {

    public GameObject sword;
	private SwordAttack swordAttack;
	public GameObject spellOrigin;

	public override void LearnPrimarySkill () {
		Skill skill = gameObject.GetComponent<Bash>();
		skill.SetProperties(player, sword);
		SetSkill (skill, 0);
	}

	public override void LearnSecondarySkill () {
		Skill skill = gameObject.GetComponent<Bladestorm>();
		skill.SetProperties(player, sword);
		SetSkill (skill, 1);
	}

	public override void LearnFirstSpell () {
		Skill skill = gameObject.GetComponent<WarCry>();
		skill.SetProperties(player);
		SetSkill (skill, 2);
	}

	public override void LearnSecondSpell () {
		Skill skill = gameObject.GetComponent<LastStand>();
		skill.SetProperties(player);
		SetSkill (skill, 3);
	}

	public override void LearnThirdSpell () {
		Skill skill = gameObject.GetComponent<FireFissure>();
		skill.SetProperties(player);
		SetSkill (skill, 4);
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
