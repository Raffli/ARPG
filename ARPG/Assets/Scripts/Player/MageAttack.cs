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
        skills[3].SetProperties();
        skills[4] = gameObject.AddComponent<ShockWave>() as ShockWave;
        skills[4].SetProperties();
    }


    protected override void CastPrimaryAttack()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        GetComponent<Animator>().SetBool("AttackedPrimary", false);
        skills[0].Execute(playerAgent, enemy, spellOrigin);
        worldInteraction.SetCanInteract(true);
    }

    protected override void CastSecondaryAttack()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        GetComponent<Animator>().SetBool("AttackedSecondary",false);
        skills[1].Execute(playerAgent, castPosition);
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

    private void RestartPlayerAgent () {
        if (!isLocalPlayer)
        {
            return;
        }
        playerAgent.isStopped = false;
	}


		
}
