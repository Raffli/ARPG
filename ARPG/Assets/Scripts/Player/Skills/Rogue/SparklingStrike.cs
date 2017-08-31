using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class SparklingStrike : Skill {


	private SwordAttack rightSwordAttack;
	private GameObject rightSword;

	public override void SetProperties (Player player, GameObject sword) {
		this.player = player;
		rightSword = sword;
		rightSwordAttack = rightSword.GetComponent<SwordAttack> ();
		skillName = "Sparkling Strike";
		skillDescription = "You hit your enemy with a powerful strike.";
		skillIcon =  Resources.Load<Sprite> ("UI/Icons/sparklingStrike");
		skillSlot = 5;
		scale = 0.3f;
		manaCost = 0;
		cooldownLeft = 0f;
		onCooldown = false;
		ModifyProperties ();
	}

	protected override void ModifyProperties () {
		baseDamage = Mathf.RoundToInt ((player.dexterity.GetValue () + player.damage.GetValue ()) * scale);
		damage = baseDamage;
		cooldown = 0.8f * (1f - player.cooldownReduction.GetValue ()/100f);
	}
		
	public override void Execute () {
        Debug.Log("execute sparkling strike");
		if (player.GetCritted ()) {
			damage = Mathf.RoundToInt (damage * player.critDamage);
		}
        rightSwordAttack.SetLightDamage (damage);
        rightSwordAttack.SetAttack(true, false);
	}

}
