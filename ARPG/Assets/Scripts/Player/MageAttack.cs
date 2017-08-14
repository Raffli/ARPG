﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MageAttack : Attack {
	
	public GameObject spellOrigin;

	public override void EquipSkill () {
		skills [0] = gameObject.AddComponent <Fireball> () as Fireball;
		skills [0].SetProperties ();
		skills [1] = gameObject.AddComponent <GroundBreaker> () as GroundBreaker;
		skills [1].SetProperties ();
		skills [2] = gameObject.AddComponent <Shield> () as Shield;
		skills [2].SetProperties (player);
		skills [3] = gameObject.AddComponent <WaterCircle> () as WaterCircle;
		skills [3].SetProperties (player);
		skills [4] = gameObject.AddComponent <ShockWave> () as ShockWave;
		skills [4].SetProperties (player);
	}

	protected override void CastPrimaryAttack () {
		skills [0].Execute (playerAgent, enemy, spellOrigin);
		worldInteraction.SetCanInteract (true);
	}

	protected override void CastSecondaryAttack () {
		Debug.Log ("cast is at " + castPosition);
		skills [1].Execute (playerAgent, castPosition);
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


		
}
