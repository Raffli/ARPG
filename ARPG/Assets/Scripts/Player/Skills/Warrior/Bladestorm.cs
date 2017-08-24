using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class Bladestorm : Skill {

	private SwordAttack swordAttack;
	private GameObject sword;

	public override void SetProperties (Player player, GameObject sword) {
		this.player = player;
		this.sword = sword;
		swordAttack = sword.GetComponent<SwordAttack> ();
		skillName = "Bladestorm";
		skillDescription = "You rotate around yourself slicing through every enemy around you.";
		skillIcon =  Resources.Load<Sprite> ("UI/Icons/bladestorm");
		skillSlot = 6;
		manaCost = 0;
		scale = 0.8f;
		cooldownLeft = 0f;
		onCooldown = false;
		ModifyProperties ();
	}

	protected override void ModifyProperties ()
	{
		baseDamage = Mathf.RoundToInt((player.strength.GetValue() + player.damage.GetValue()) * scale);
		damage = baseDamage;
		cooldown = 4f * (1 - player.cooldownReduction.GetValue ()/100);
	}

	public override void Execute () {
		ModifyProperties ();
		if (player.GetCritted ()) {
			damage = Mathf.RoundToInt (damage * player.critDamage);
		}
		swordAttack.SetHeavyDamage (damage);
		swordAttack.SetAttack(false, true);
	}
}
