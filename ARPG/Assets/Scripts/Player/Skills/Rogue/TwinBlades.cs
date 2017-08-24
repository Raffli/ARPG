using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class TwinBlades : Skill {

	private SwordAttack leftSwordAttack;
	private GameObject leftSword;
	private SwordAttack rightSwordAttack;
	private GameObject rightSword;

	public override void SetProperties (Player player, GameObject leftSword, GameObject rightSword) {
		this.player = player;
		this.leftSword = leftSword;
		leftSwordAttack = leftSword.GetComponent<SwordAttack>();
		this.rightSword = rightSword;
		rightSwordAttack = rightSword.GetComponent<SwordAttack>();
		skillName = "Twin Blades";
		skillDescription = "You hit your enemy with two lightning fast blades.";
		skillIcon =  Resources.Load<Sprite> ("UI/Icons/twinBlades");
		skillSlot = 6;
		scale = 0.6f;
		manaCost = 0;
		cooldownLeft = 0f;
		onCooldown = false;
		ModifyProperties ();
	}

	protected override void ModifyProperties ()
	{
		baseDamage = Mathf.RoundToInt((player.dexterity.GetValue() + player.damage.GetValue()) * scale);
		damage = baseDamage;
		cooldown = 1.3f * (1 - player.cooldownReduction.GetValue ()/100);
	}

	public override void Execute () {
		ModifyProperties ();
		if (player.GetCritted ()) {
			damage = Mathf.RoundToInt (damage * player.critDamage);
		}
		rightSwordAttack.SetHeavyDamage (damage);
		rightSwordAttack.SetAttack(false, true);
        leftSwordAttack.SetHeavyDamage(damage);
        leftSwordAttack.SetAttack(false, true);
    }
}
