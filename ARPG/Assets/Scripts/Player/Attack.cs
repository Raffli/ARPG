using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class Attack : NetworkBehaviour
{

	private Animator animator;

	protected NavMeshAgent playerAgent;
	protected WorldInteraction worldInteraction;
	protected Player player;

	protected GameObject enemy;

	protected bool primaryOnCooldown;
	protected bool secondaryOnCooldown;
	protected bool firstSpellOnCooldown;
	protected bool secondSpellOnCooldown;
	protected bool thirdSpellOnCooldown;
	protected bool healPotionOnCooldown;
	protected bool manaPotionOnCooldown;

	protected Vector3 castPosition;

	protected Skill [] skills;
	protected HealPotion healPotion;
	protected ManaPotion manaPotion;

	void Start () {
		animator = GetComponent<Animator>();
		playerAgent = GetComponent<NavMeshAgent> ();
		player = GetComponent <Player> ();
		worldInteraction = GetComponent<WorldInteraction> ();
		healPotion = gameObject.AddComponent<HealPotion> () as HealPotion;
		healPotion.SetProperties ();
		manaPotion = gameObject.AddComponent<ManaPotion> () as ManaPotion;
		manaPotion.SetProperties ();
		skills = new Skill[5]; 
	}
	
	void FixedUpdate () {
        if (!isLocalPlayer)
        {
            return;
        }
        if (isServer)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                Debug.Log(Input.mousePosition);
                castPosition = Input.mousePosition;
                RpcAttackSecondary();
            }
            else if (Input.GetButtonDown("Spell1"))
            {
                RpcUseFirstSpell();
            }
            else if (Input.GetButtonDown("Spell2"))
            {
                RpcUseSecondSpell();
            }
            else if (Input.GetButtonDown("Spell3"))
            {
                RpcUseThirdSpell();
            }
            else if (Input.GetButtonDown("HealPotion"))
            {
                RpcUseHealPotion();
            }
            else if (Input.GetButtonDown("ManaPotion"))
            {
                RpcUseManaPotion();
            }
            else if (Input.GetButtonDown("Jump"))
            {
                RpcEquipSkill();
            }
        }
        else {
            if (Input.GetButtonDown("Fire2"))
            {
                Debug.Log(Input.mousePosition);
                castPosition = Input.mousePosition;
                CmdAttackSecondary();
            }
            else if (Input.GetButtonDown("Spell1"))
            {
                CmdUseFirstSpell();
            }
            else if (Input.GetButtonDown("Spell2"))
            {
                CmdUseSecondSpell();
            }
            else if (Input.GetButtonDown("Spell3"))
            {
                CmdUseThirdSpell();
            }
            else if (Input.GetButtonDown("HealPotion"))
            {
                CmdUseHealPotion();
            }
            else if (Input.GetButtonDown("ManaPotion"))
            {
                CmdUseManaPotion();
            }
            else if (Input.GetButtonDown("Jump"))
            {
                CmdEquipSkill();
            }
        }
	}

    public void SetSkill (Skill skill, int index) {
		skills [index] = skill;
	}

    [ClientRpc]
    public void RpcAttackPrimary(GameObject enemy) {
		if (skills [0] != null && !skills[0].onCooldown && (player.currentMana - skills [0].manaCost) >= 0) {
			player.currentMana -= skills [0].manaCost;
			this.enemy = enemy;
			worldInteraction.SetCanInteract (false);
			skills [0].StartCooldown ();
			if (animator) {
				animator.SetTrigger ("attackedPrimary"); // Animator calls CastPrimaryAttack
			}
		}
	}

    [ClientRpc]
    public void RpcAttackSecondary() {
		if (skills [1] != null && !skills[1].onCooldown && (player.currentMana - skills [1].manaCost) >= 0) {
			player.currentMana -= skills [1].manaCost;
			worldInteraction.SetCanInteract (false);
			playerAgent.isStopped = true;
			skills [1].StartCooldown ();
			if (animator) {
				animator.SetTrigger ("attackedSecondary"); // Animator calls CastSecondaryAttack
			}
		}
	}

    [ClientRpc]
    public void RpcUseFirstSpell() {
        print("use first spell");
		if (skills [2] != null && !skills[2].onCooldown && (player.currentMana - skills [2].manaCost) >= 0) {
			player.currentMana -= skills [2].manaCost;
			worldInteraction.SetCanInteract (false);
			playerAgent.isStopped = true;
			skills [2].StartCooldown ();
			if (animator) {
				animator.SetTrigger ("usedFirstSpell"); // Animator calls CastFirstSpell
			}
		}
	}

    [ClientRpc]
    public void RpcUseSecondSpell() {
		if (skills [3] != null && !skills[3].onCooldown && (player.currentMana - skills [3].manaCost) >= 0) {
			player.currentMana -= skills [3].manaCost;
			worldInteraction.SetCanInteract (false);
			playerAgent.isStopped = true;
			skills [3].StartCooldown ();
			if (animator) {
				animator.SetTrigger ("usedSecondSpell"); // Animator calls CastSecondSpell
			}
		}
	}

    [ClientRpc]
    public void RpcUseThirdSpell() {
		if (skills [4] != null && !skills[4].onCooldown && (player.currentMana - skills [4].manaCost) >= 0) {
			player.currentMana -= skills [4].manaCost;
			worldInteraction.SetCanInteract (false);
			playerAgent.isStopped = true;
			skills [4].StartCooldown ();
			if (animator) {
				animator.SetTrigger ("usedThirdSpell"); // Animator calls CastThirdSpell
			}
		}
	}

    [ClientRpc]
    public void RpcUseHealPotion() {
		if (!healPotion.onCooldown) {
			healPotion.Use (player);
		}
	}

    [ClientRpc]
    public void RpcUseManaPotion() {
		if (!manaPotion.onCooldown) {
			manaPotion.Use (player);
		}
	}

    [ClientRpc]
    public virtual void RpcEquipSkill() {}
    [ClientRpc]
    protected virtual void RpcCastPrimaryAttack() {}
    [ClientRpc]
    protected virtual void RpcCastSecondaryAttack() {}
    [ClientRpc]
    protected virtual void RpcCastFirstSpell () {}
    [ClientRpc]
    protected virtual void RpcCastSecondSpell() {}
    [ClientRpc]
    protected virtual void RpcCastThirdSpell() {}


    [Command]
    public void CmdAttackPrimary(GameObject enemy)
    {
        if (skills[0] != null && !skills[0].onCooldown && (player.currentMana - skills[0].manaCost) >= 0)
        {
            player.currentMana -= skills[0].manaCost;
            this.enemy = enemy;
            worldInteraction.SetCanInteract(false);
            skills[0].StartCooldown();
            if (animator)
            {
                animator.SetTrigger("attackedPrimary"); // Animator calls CastPrimaryAttack
            }
        }
    }

    [Command]
    public void CmdAttackSecondary()
    {
        if (skills[1] != null && !skills[1].onCooldown && (player.currentMana - skills[1].manaCost) >= 0)
        {
            player.currentMana -= skills[1].manaCost;
            worldInteraction.SetCanInteract(false);
            playerAgent.isStopped = true;
            skills[1].StartCooldown();
            if (animator)
            {
                animator.SetTrigger("attackedSecondary"); // Animator calls CastSecondaryAttack
            }
        }
    }

    [Command]
    public void CmdUseFirstSpell()
    {
        print("use first spell");
        if (skills[2] != null && !skills[2].onCooldown && (player.currentMana - skills[2].manaCost) >= 0)
        {
            player.currentMana -= skills[2].manaCost;
            worldInteraction.SetCanInteract(false);
            playerAgent.isStopped = true;
            skills[2].StartCooldown();
            if (animator)
            {
                animator.SetTrigger("usedFirstSpell"); // Animator calls CastFirstSpell
            }
        }
    }

    [Command]
    public void CmdUseSecondSpell()
    {
        if (skills[3] != null && !skills[3].onCooldown && (player.currentMana - skills[3].manaCost) >= 0)
        {
            player.currentMana -= skills[3].manaCost;
            worldInteraction.SetCanInteract(false);
            playerAgent.isStopped = true;
            skills[3].StartCooldown();
            if (animator)
            {
                animator.SetTrigger("usedSecondSpell"); // Animator calls CastSecondSpell
            }
        }
    }

    [Command]
    public void CmdUseThirdSpell()
    {
        if (skills[4] != null && !skills[4].onCooldown && (player.currentMana - skills[4].manaCost) >= 0)
        {
            player.currentMana -= skills[4].manaCost;
            worldInteraction.SetCanInteract(false);
            playerAgent.isStopped = true;
            skills[4].StartCooldown();
            if (animator)
            {
                animator.SetTrigger("usedThirdSpell"); // Animator calls CastThirdSpell
            }
        }
    }

    [Command]
    public void CmdUseHealPotion()
    {
        if (!healPotion.onCooldown)
        {
            healPotion.Use(player);
        }
    }

    [Command]
    public void CmdUseManaPotion()
    {
        if (!manaPotion.onCooldown)
        {
            manaPotion.Use(player);
        }
    }

    [Command]
    public virtual void CmdEquipSkill() { }
    [Command]
    protected virtual void CmdCastPrimaryAttack() { }
    [Command]
    protected virtual void CmdCastSecondaryAttack() { }
    [Command]
    protected virtual void CmdCastFirstSpell() { }
    [Command]
    protected virtual void CmdCastSecondSpell() { }
    [Command]
    protected virtual void CmdCastThirdSpell() { }

}
