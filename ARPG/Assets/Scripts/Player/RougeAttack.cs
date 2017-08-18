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

        skills[0] = gameObject.AddComponent<SparklingStrike>() as SparklingStrike;
        skills[0].SetProperties(rightSword);
        skills[1] = gameObject.AddComponent<TwinBlades>() as TwinBlades;
        skills[1].SetProperties(leftSword, rightSword);
        skills[2] = gameObject.AddComponent<PoisonousBlade>() as PoisonousBlade;
        skills[2].SetProperties(player);
        skills[3] = gameObject.AddComponent<Stealth>() as Stealth;
        skills[3].SetProperties(player);
        skills[4] = gameObject.AddComponent<SweepingLotus>() as SweepingLotus;
        skills[4].SetProperties();
    }


    protected override void CastPrimaryAttack()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        GetComponent<Animator>().SetBool("AttackedPrimary", false);
        skills[0].Execute();
        playerAgent.isStopped = false;
        worldInteraction.SetCanInteract(true);
    }

    protected override void CastSecondaryAttack()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        GetComponent<Animator>().SetBool("AttackedSecondary", false);
        skills[1].Execute();
        playerAgent.isStopped = false;
        worldInteraction.SetCanInteract(true);
    }

    protected override void CastFirstSpell()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        GetComponent<Animator>().SetBool("UsedFirstSpell", false);
        skills[2].Execute();
        playerAgent.isStopped = false;
        worldInteraction.SetCanInteract(true);
    }

    protected override void CastSecondSpell()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        GetComponent<Animator>().SetBool("UsedSecondSpell", false);
        skills[3].Execute();
        playerAgent.isStopped = false;
        worldInteraction.SetCanInteract(true);
    }

    protected override void CastThirdSpell()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        GetComponent<Animator>().SetBool("UsedThirdSpell", false);
        skills[4].Execute();
        playerAgent.isStopped = false;
        worldInteraction.SetCanInteract(true);
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
