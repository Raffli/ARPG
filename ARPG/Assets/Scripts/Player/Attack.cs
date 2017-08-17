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
		healPotion.CmdSetProperties ();
		manaPotion = gameObject.AddComponent<ManaPotion> () as ManaPotion;
		manaPotion.CmdSetProperties ();
		skills = new Skill[5]; 
	}
	
	void FixedUpdate () {
        if (Input.GetButtonDown("Fire2"))
        {
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
            print("Jump");
            EquipSkill();
        }
	}

    public void SetSkill (Skill skill, int index) {
		skills [index] = skill;
	}



    [Command]
    public void CmdAttackPrimary(GameObject enemy)
    {
        if (skills[0] != null && !skills[0].onCooldown && (player.currentMana - skills[0].manaCost) >= 0)
        {
            player.currentMana -= skills[0].manaCost;
            this.enemy = enemy;
            worldInteraction.CmdSetCanInteract(false);
            skills[0].CmdStartCooldown();
            if (animator)
            {
                animator.SetTrigger("attackedPrimary"); // Animator calls CastPrimaryAttack
            }
        }
    }

    [Command]
    public void CmdAttackSecondary()
    {
        print("SecondaryAttack");
        print(skills[1]);
        print(skills[1].onCooldown);
        print(player.currentMana);
        print(skills[1].manaCost);
        if (skills[1] != null && !skills[1].onCooldown && (player.currentMana - skills[1].manaCost) >= 0)
        {
            print("im if");
            player.currentMana -= skills[1].manaCost;
            worldInteraction.CmdSetCanInteract(false);
            playerAgent.isStopped = true;
            skills[1].CmdStartCooldown();
            if (animator)
            {
                print("animator set trigger");
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
            worldInteraction.CmdSetCanInteract(false);
            playerAgent.isStopped = true;
            skills[2].CmdStartCooldown();
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
            worldInteraction.CmdSetCanInteract(false);
            playerAgent.isStopped = true;
            skills[3].CmdStartCooldown();
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
            worldInteraction.CmdSetCanInteract(false);
            playerAgent.isStopped = true;
            skills[4].CmdStartCooldown();
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

    public virtual void EquipSkill() { }
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
