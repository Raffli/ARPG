using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MageAttack : Attack {
	
	public GameObject spellOrigin;
	public GameObject fireBall;
	public GameObject shockWave;

	public int primaryDamage;
	public int secondaryDamage;

	public float cooldownPrimaryAttack;
	public float cooldownSecondaryAttack;
	public float cooldownFirstSpell;
	public float cooldownSecondSpell;
	public float cooldownThirdSpell;

	protected override void CastPrimaryAttack () {
		skills [0].Execute (playerAgent, enemy, spellOrigin);
		worldInteraction.SetCanInteract (true);
	}

	protected override void CastSecondaryAttack () {
		skills [1].Execute (playerAgent, Input.mousePosition);
		worldInteraction.SetCanInteract (true);
	}

	protected override void CastFirstSpell () {
		skills [2].Execute (transform);
		playerAgent.isStopped = false;
		worldInteraction.SetCanInteract (true);
	}

	protected override void CastSecondSpell () {
		skills [3].Execute (transform);
		playerAgent.isStopped = false;
		worldInteraction.SetCanInteract (true);
	}

	protected override void CastThirdSpell () {
		skills [4].Execute (transform);
		playerAgent.isStopped = false;
		worldInteraction.SetCanInteract (true);
	}

	private void RestartPlayerAgent () {
		playerAgent.isStopped = false;
	}


		
}
