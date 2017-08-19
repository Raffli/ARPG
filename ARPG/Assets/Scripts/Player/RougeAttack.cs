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
        skills[0].SetProperties(rightSword);
        skills[1] = gameObject.GetComponent<TwinBlades>();
        skills[1].SetProperties(leftSword, rightSword);
        skills[2] = gameObject.GetComponent<PoisonousBlade>();
        skills[2].SetProperties(player);
        skills[3] = gameObject.GetComponent<Stealth>();
        skills[3].SetProperties(player);
        skills[4] = gameObject.GetComponent<SweepingLotus>();
        skills[4].SetProperties();

        if (isLocalPlayer)
        {
            HUDManager.Instance.AddSkillToUI(skills[0].skillIcon, 5);
            HUDManager.Instance.AddSkillToUI(skills[1].skillIcon, 6);
            HUDManager.Instance.AddSkillToUI(skills[2].skillIcon, 0);
            HUDManager.Instance.AddSkillToUI(skills[3].skillIcon, 1);
            HUDManager.Instance.AddSkillToUI(skills[4].skillIcon, 2);
        }
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
