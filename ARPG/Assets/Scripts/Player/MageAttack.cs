using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class MageAttack : Attack {
	
	public GameObject spellOrigin;

    public override void EquipSkill()
    {
        
        skills[0] = gameObject.GetComponent<Fireball>();
        skills[0].SetProperties(player);
        skills[1] = gameObject.GetComponent<GroundBreaker>();
        skills[1].SetProperties(player);
        skills[2] = gameObject.GetComponent<Shield>();
        skills[2].SetProperties(player);
        skills[3] = gameObject.GetComponent<WaterCircle>();
        skills[3].SetProperties(player);
        skills[4] = gameObject.GetComponent<ShockWave>();
        skills[4].SetProperties(player);

        if (isLocalPlayer)
        {
			HUDManager.Instance.AddSkillToUI(skills[0].skillIcon, skills[0].skillSlot);
			CharacterManager.Instance.primaryIcon.sprite = skills [0].skillIcon;
			CharacterManager.Instance.primaryIcon.enabled = true;
			CharacterManager.Instance.primary.text = skills [0].skillName;
			HUDManager.Instance.AddSkillToUI(skills[1].skillIcon, skills[1].skillSlot);
			CharacterManager.Instance.secondaryIcon.sprite = skills [1].skillIcon;
			CharacterManager.Instance.secondaryIcon.enabled = true;
			CharacterManager.Instance.secondary.text = skills [1].skillName;
			HUDManager.Instance.AddSkillToUI(skills[2].skillIcon, skills[2].skillSlot);
			CharacterManager.Instance.firstSpellIcon.sprite = skills [2].skillIcon;
			CharacterManager.Instance.firstSpellIcon.enabled = true;
			CharacterManager.Instance.firstSpell.text = skills [2].skillName;
			HUDManager.Instance.AddSkillToUI(skills[3].skillIcon, skills[3].skillSlot);
			CharacterManager.Instance.secondSpellIcon.sprite = skills [3].skillIcon;
			CharacterManager.Instance.secondSpellIcon.enabled = true;
			CharacterManager.Instance.secondSpell.text = skills [3].skillName;
			HUDManager.Instance.AddSkillToUI(skills[4].skillIcon, skills[4].skillSlot);
			CharacterManager.Instance.thirdSpellIcon.sprite = skills [4].skillIcon;
			CharacterManager.Instance.thirdSpellIcon.enabled = true;
			CharacterManager.Instance.thirdSpell.text = skills [4].skillName;
        }
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
