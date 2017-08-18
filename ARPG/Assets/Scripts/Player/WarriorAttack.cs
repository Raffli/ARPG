using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        skills[4].SetProperties(player);
    }


    protected override void CastPrimaryAttack()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        skills[0].Execute();
        playerAgent.isStopped = false;
        worldInteraction.CmdSetCanInteract(true);
    }

    protected override void CastSecondaryAttack()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        skills[1].Execute();
        playerAgent.isStopped = false;
        worldInteraction.CmdSetCanInteract(true);
    }

    protected override void CastFirstSpell()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        print("firstspell");
        skills[2].Execute();
        playerAgent.isStopped = false;
        worldInteraction.CmdSetCanInteract(true);
    }

    protected override void CastSecondSpell()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        skills[3].Execute();
        playerAgent.isStopped = false;
        worldInteraction.CmdSetCanInteract(true);
    }

    protected override void CastThirdSpell()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        skills[4].Execute(spellOrigin);
        playerAgent.isStopped = false;
        worldInteraction.CmdSetCanInteract(true);
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
