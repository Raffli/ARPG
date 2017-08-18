using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class MageAttack : Attack {
	
	public GameObject spellOrigin;

    public override void EquipSkill()
    {
        
        skills[0] = gameObject.AddComponent<Fireball>() as Fireball;
        skills[0].SetProperties();
        skills[1] = gameObject.AddComponent<GroundBreaker>() as GroundBreaker;
        skills[1].SetProperties();
        skills[2] = gameObject.AddComponent<Shield>() as Shield;
        skills[2].SetProperties(player);
        skills[3] = gameObject.AddComponent<WaterCircle>() as WaterCircle;
        skills[3].SetProperties(player);
        skills[4] = gameObject.AddComponent<ShockWave>() as ShockWave;
        skills[4].SetProperties(player);
    }


    protected override void CastPrimaryAttack()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        skills[0].Execute(playerAgent, enemy, spellOrigin);
        worldInteraction.CmdSetCanInteract(true);
    }

    protected override void CastSecondaryAttack()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        skills[1].Execute(playerAgent, castPosition);
        worldInteraction.CmdSetCanInteract(true);
    }

    protected override void CastFirstSpell()
    {
        if (!isLocalPlayer)
        {
            return;
        }
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
        skills[4].Execute();
        playerAgent.isStopped = false;
        worldInteraction.CmdSetCanInteract(true);
    }

    private void RestartPlayerAgent () {
        if (!isLocalPlayer)
        {
            return;
        }
        playerAgent.isStopped = false;
	}


		
}
