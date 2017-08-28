using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Skill : NetworkBehaviour {

	public string skillName { get; set; }
	public string skillDescription { get; set; }
	public Sprite skillIcon { get; set; }
	public int skillSlot { get; set; }
	public int manaCost { get; set; }
	public int baseDamage { get; set; }
	public int damage { get; set; }
	public float cooldown { get; set; }
	public float cooldownLeft { get; set; }
	public bool onCooldown { get; set; }
	public float duration { get; set; }
	public float scale { get; set; }

	protected Player player;

    public virtual void SetProperties(Player player) {}
    public virtual void SetProperties(Player player, GameObject sword) {}
	public virtual void SetProperties(Player player, GameObject leftSword, GameObject rightSword) {}

    public virtual void Execute() {}
    public virtual void Execute(GameObject spellOrigin) {}
    public virtual void Execute(Vector3 targetPoint) {}
    public virtual void Execute(GameObject enemy, GameObject spellOrigin) {}

	protected virtual void ModifyProperties () {} // for recalculating the values before attacking

	protected virtual void Update () {
		if (onCooldown) {
			cooldownLeft -= Time.deltaTime;
			HUDManager.Instance.UpdateCooldown (skillSlot, cooldownLeft, cooldown);
			if (cooldownLeft <= 0) {
				onCooldown = false;
			}
		}
	}

    public virtual void StartCooldown () {
		ModifyProperties ();
		onCooldown = true;
		cooldownLeft = cooldown;
	}

}
