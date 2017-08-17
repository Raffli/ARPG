using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RougeAttack : Attack {

	public GameObject leftSword;
	public GameObject rightSword;

	private SwordAttack leftSwordAttack;
	private SwordAttack rightSwordAttack;

	public override void EquipSkill () {
		leftSwordAttack = leftSword.GetComponent<SwordAttack> ();
		rightSwordAttack = rightSword.GetComponent<SwordAttack> ();

		skills [0] = gameObject.AddComponent <SparklingStrike> () as SparklingStrike;
		skills [0].SetProperties (leftSword);
		HUDManager.Instance.AddSkillToUI (skills [0].skillIcon, 5);
		skills [1] = gameObject.AddComponent <TwinBlades> () as TwinBlades;
		skills [1].SetProperties (leftSword, rightSword);
		HUDManager.Instance.AddSkillToUI (skills [1].skillIcon, 6);
		skills [2] = gameObject.AddComponent <PoisonousBlade> () as PoisonousBlade;
		skills [2].SetProperties (player);
		HUDManager.Instance.AddSkillToUI (skills [2].skillIcon, 0);
		skills [3] = gameObject.AddComponent <Stealth> () as Stealth;
		skills [3].SetProperties (player);
		HUDManager.Instance.AddSkillToUI (skills [3].skillIcon, 1);
		skills [4] = gameObject.AddComponent <SweepingLotus> () as SweepingLotus;
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
		skills [4].Execute ();
		playerAgent.isStopped = false;
		worldInteraction.SetCanInteract (true);
	}

	private void RestartPlayerAgent () {
		playerAgent.isStopped = false;
	}

	private void DisableSwords () {
		leftSwordAttack.DisableSword ();
		rightSwordAttack.DisableSword ();
	}
		
}
