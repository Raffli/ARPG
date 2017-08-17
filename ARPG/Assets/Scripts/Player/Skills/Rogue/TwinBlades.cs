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

	public override void SetProperties (GameObject leftSword, GameObject rightSword) {
		this.leftSword = leftSword;
		leftSwordAttack = leftSword.GetComponent<SwordAttack>();
		this.rightSword = rightSword;
		rightSwordAttack = rightSword.GetComponent<SwordAttack>();
		skillName = "Twin Blades";
		skillDescription = "You hit your enemy with two lightning fast blades.";
		skillIcon = (Sprite) Resources.Load ("UI/twinBlades");
		manaCost = 0;
		baseDamage = 30;
		damage = baseDamage;
		cooldown = 1.3f;
		cooldownLeft = 0f;
		onCooldown = false;
	}
		
	public override void CmdExecute () {
		rightSwordAttack.SetHeavyDamage (baseDamage);
		rightSwordAttack.SetAttack(false, true);
        leftSwordAttack.SetHeavyDamage(baseDamage);
        leftSwordAttack.SetAttack(false, true);
    }
}
