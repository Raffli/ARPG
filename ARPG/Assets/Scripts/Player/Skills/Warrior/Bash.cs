using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Bash : Skill {

	private SwordAttack swordAttack;
	private GameObject sword;

	public override void SetProperties (Player player, GameObject sword) {
		this.player = player;
		this.sword = sword;
		swordAttack = sword.GetComponent<SwordAttack> ();
		skillName = "Bash";
		skillDescription = "You slice through your enemy with full power.";
		skillIcon = Resources.Load <Sprite> ("UI/Icons/bash");
		skillSlot = 5;
		manaCost = 0;
		scale = 1f;
		cooldownLeft = 0f;
		onCooldown = false;
		ModifyProperties ();
	}

	protected override void ModifyProperties ()
	{
		baseDamage = Mathf.RoundToInt((player.strength.GetValue() + player.damage.GetValue()) * scale);
		damage = baseDamage;
		cooldown = 1f * (1 - player.cooldownReduction.GetValue ()/100);
	}

	public override void Execute () {
		ModifyProperties ();
		if (player.GetCritted ()) {
			damage = Mathf.RoundToInt (damage * player.critDamage);
		}
		swordAttack.SetLightDamage (damage);
		swordAttack.SetAttack(true, false);
	}

}
