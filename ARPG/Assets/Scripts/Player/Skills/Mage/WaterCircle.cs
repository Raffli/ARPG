using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class WaterCircle : Skill {

	private GameObject waterCircle;

	public override void SetProperties (Player player) {
		waterCircle = player.transform.Find ("WaterCircle").gameObject;
		skillName = "Water Circle";
		skillDescription = "You get sourrounded by water that deals damage to everything it comes in contact with.";
		skillIcon = (Sprite) Resources.Load ("UI/waterCircle");
		manaCost = 25;
		baseDamage = 10;
		damage = baseDamage;
		cooldown = 5f;
		cooldownLeft = 0f;
		onCooldown = false;
	}

	public override void Execute () {
		waterCircle.SetActive (true);
		waterCircle.GetComponent<WaterCircleBehaviour> ().SetDamage (damage);
	}
}
