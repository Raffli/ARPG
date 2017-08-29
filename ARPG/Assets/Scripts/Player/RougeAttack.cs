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

	protected override void SetPrivateProperties () {
		leftSwordAttack = leftSword.GetComponent<SwordAttack> ();
		rightSwordAttack = rightSword.GetComponent<SwordAttack> ();
	}

	public override void LearnPrimarySkill () {
		Skill skill = gameObject.GetComponent<SparklingStrike>();
		skill.SetProperties(player, rightSword);
		SetSkill (skill, 0);
	}

	public override void LearnSecondarySkill () {
		Skill skill = gameObject.GetComponent<TwinBlades>();
		skill.SetProperties(player, leftSword, rightSword);
		SetSkill (skill, 1);
	}

	public override void LearnFirstSpell () {
		Skill skill = gameObject.GetComponent<PoisonousBlade>();
		skill.SetProperties(player);
		SetSkill (skill, 2);
	}

	public override void LearnSecondSpell () {
		Skill skill = gameObject.GetComponent<Stealth>();
		skill.SetProperties(player);
		SetSkill (skill, 3);
	}

	public override void LearnThirdSpell () {
		Skill skill = gameObject.GetComponent<SweepingLotus>();
		skill.SetProperties(player);
		SetSkill (skill, 4);
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
