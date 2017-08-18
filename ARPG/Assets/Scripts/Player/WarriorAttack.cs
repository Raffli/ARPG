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

        skills[0] = gameObject.AddComponent<Bash>() as Bash;
        skills[0].SetProperties(sword);
        skills[1] = gameObject.AddComponent<Bladestorm>() as Bladestorm;
        skills[1].SetProperties(sword);
        skills[2] = gameObject.AddComponent<WarCry>() as WarCry;
        skills[2].SetProperties(player);
        skills[3] = gameObject.AddComponent<LastStand>() as LastStand;
        skills[3].SetProperties(player);
        skills[4] = gameObject.AddComponent<FireFissure>() as FireFissure;
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
