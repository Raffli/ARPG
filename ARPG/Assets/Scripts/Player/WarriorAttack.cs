using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WarriorAttack : Attack {

    public GameObject sword;
	private SwordAttack swordAttack;
	public GameObject spellOrigin;

	public override void EquipSkill () {
		swordAttack = sword.GetComponent<SwordAttack> ();

		skills [0] = gameObject.AddComponent <Bash> () as Bash;
		skills [0].SetProperties (sword);
		HUDManager.Instance.AddSkillToUI (skills [0].skillIcon, 5);
		skills [1] = gameObject.AddComponent <Bladestorm> () as Bladestorm;
		skills [1].SetProperties (sword);
		HUDManager.Instance.AddSkillToUI (skills [1].skillIcon, 6);
		skills [2] = gameObject.AddComponent <WarCry> () as WarCry;
		skills [2].SetProperties (player);
		HUDManager.Instance.AddSkillToUI (skills [2].skillIcon, 0);
		skills [3] = gameObject.AddComponent <LastStand> () as LastStand;
		skills [3].SetProperties (player);
		HUDManager.Instance.AddSkillToUI (skills [3].skillIcon, 1);
		skills [4] = gameObject.AddComponent <FireFissure> () as FireFissure;
		skills [4].SetProperties (player);
		HUDManager.Instance.AddSkillToUI (skills [4].skillIcon, 2);
	}

	protected override void CastPrimaryAttack () {
		skills [0].Execute ();
		playerAgent.isStopped = false;
		worldInteraction.SetCanInteract (true);
	}

	protected override void CastSecondaryAttack () {
		skills [1].Execute ();
		playerAgent.isStopped = false;
		worldInteraction.SetCanInteract (true);
	}

	protected override void CastFirstSpell () {
		skills [2].Execute ();
		playerAgent.isStopped = false;
		worldInteraction.SetCanInteract (true);
	}

	protected override void CastSecondSpell () {
		skills [3].Execute ();
		playerAgent.isStopped = false;
		worldInteraction.SetCanInteract (true);
	}

	protected override void CastThirdSpell () {
		skills [4].Execute (spellOrigin);
		playerAgent.isStopped = false;
		worldInteraction.SetCanInteract (true);
	}

	private void RestartPlayerAgent () {
		playerAgent.isStopped = false;
		swordAttack.DisableSword ();
	}
}
