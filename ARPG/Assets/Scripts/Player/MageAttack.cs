using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class MageAttack : Attack {
	
	public GameObject spellOrigin;

	public override void LearnPrimarySkill () {
		Skill skill = gameObject.GetComponent<Fireball>();
		skill.SetProperties(player);
		SetSkill (skill, 0);
	}

	public override void LearnSecondarySkill () {
		Skill skill = gameObject.GetComponent<GroundBreaker>();
		skill.SetProperties(player);
		SetSkill (skill, 1);
	}

	public override void LearnFirstSpell () {
		Skill skill = gameObject.GetComponent<Shield>();
		skill.SetProperties(player);
		SetSkill (skill, 2);
	}

	public override void LearnSecondSpell () {
		Skill skill = gameObject.GetComponent<WaterCircle>();
		skill.SetProperties(player);
		SetSkill (skill, 3);
	}

	public override void LearnThirdSpell () {
		Skill skill = gameObject.GetComponent<ShockWave>();
		skill.SetProperties(player);
		SetSkill (skill, 4);
	}

    protected override void CastPrimaryAttack()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        GetComponent<Animator>().SetBool("AttackedPrimary", false);
        skills[0].Execute(enemy, spellOrigin);
        worldInteraction.SetCanInteract(true);
    }

    protected override void CastSecondaryAttack()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        GetComponent<Animator>().SetBool("AttackedSecondary",false);
        skills[1].Execute(castPosition);
        worldInteraction.SetCanInteract(true);
    }

    private void RestartPlayerAgent () {
        if (!isLocalPlayer)
        {
            return;
        }
        playerAgent.isStopped = false;
	}


		
}
