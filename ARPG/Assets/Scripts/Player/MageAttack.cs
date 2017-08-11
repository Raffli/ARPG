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

	}

	protected override void CastFirstSpell () {
		
	}

	protected override void CastSecondSpell () {
		
	}

	protected override void CastThirdSpell () {
		shockWave.SetActive (true);
		shockWave.GetComponent<ShockWave> ().SetDamage (secondaryDamage);
		worldInteraction.SetCanInteract (true);

	}

	private void RestartPlayerAgent () {
		playerAgent.isStopped = false;
	}


		
}
