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
        if (!isLocalPlayer) {
            return;
        }
        if (Input.GetButtonDown("Fire2"))
        {
            castPosition = Input.mousePosition;
            AttackSecondary();
        }
        else if (Input.GetButtonDown("Spell1"))
        {
            print(isLocalPlayer);

            UseFirstSpell();
        }
        else if (Input.GetButtonDown("Spell2"))
        {
            print(isLocalPlayer);

            UseSecondSpell();
        }
        else if (Input.GetButtonDown("Spell3"))
        {
            print(isLocalPlayer);

            UseThirdSpell();
        }
        else if (Input.GetButtonDown("HealPotion"))
        {
            print(isLocalPlayer);

            UseHealPotion();
        }
        else if (Input.GetButtonDown("ManaPotion"))
        {
            print(isLocalPlayer);

            UseManaPotion();
        }
        else if (Input.GetButtonDown("Jump"))
        {
            print(isLocalPlayer);
            EquipSkill();
        }
	}

    public void SetSkill (Skill skill, int index) {
		skills [index] = skill;
	}



    public void AttackPrimary(GameObject enemy)
    {
        if (skills[0] != null && !skills[0].onCooldown && (player.currentMana - skills[0].manaCost) >= 0)
        {
            player.currentMana -= skills[0].manaCost;
            this.enemy = enemy;
            worldInteraction.CmdSetCanInteract(false);
            skills[0].StartCooldown();
            if (animator)
            {
                animator.SetTrigger("attackedPrimary"); // Animator calls CastPrimaryAttack
            }
        }
    }

    public void AttackSecondary()
    {

        if (skills[1] != null && !skills[1].onCooldown && (player.currentMana - skills[1].manaCost) >= 0)
        {
            player.currentMana -= skills[1].manaCost;
            worldInteraction.CmdSetCanInteract(false);
            playerAgent.isStopped = true;
            skills[1].StartCooldown();
            if (animator)
            {
                print("isServer" + isServer);
                print("islocalplayer" + isLocalPlayer);
                animator.SetBool("AttackedSecondary",true); // Animator calls CastSecondaryAttack
            }
        }
    }

    public void UseFirstSpell()
    {
        if (skills[2] != null && !skills[2].onCooldown && (player.currentMana - skills[2].manaCost) >= 0)
        {
            player.currentMana -= skills[2].manaCost;
            worldInteraction.CmdSetCanInteract(false);
            playerAgent.isStopped = true;
            skills[2].StartCooldown();
            if (animator)
            {
                animator.SetTrigger("usedFirstSpell"); // Animator calls CastFirstSpell
            }
        }
    }

    public void UseSecondSpell()
    {
        if (skills[3] != null && !skills[3].onCooldown && (player.currentMana - skills[3].manaCost) >= 0)
        {
            player.currentMana -= skills[3].manaCost;
            worldInteraction.CmdSetCanInteract(false);
            playerAgent.isStopped = true;
            skills[3].StartCooldown();
            if (animator)
            {
                animator.SetTrigger("usedSecondSpell"); // Animator calls CastSecondSpell
            }
        }
    }

    public void UseThirdSpell()
    {
        if (skills[4] != null && !skills[4].onCooldown && (player.currentMana - skills[4].manaCost) >= 0)
        {
            player.currentMana -= skills[4].manaCost;
            worldInteraction.CmdSetCanInteract(false);
            playerAgent.isStopped = true;
            skills[4].StartCooldown();
            if (animator)
            {
                animator.SetTrigger("usedThirdSpell"); // Animator calls CastThirdSpell
            }
        }
    }

    public void UseHealPotion()
    {
        if (!healPotion.onCooldown)
        {
            healPotion.Use(player);
        }
    }

    public void UseManaPotion()
    {
        if (!manaPotion.onCooldown)
        {
            manaPotion.Use(player);
        }
    }

    public virtual void EquipSkill() { }
    protected virtual void CastPrimaryAttack() { }
    protected virtual void CastSecondaryAttack() { }
    protected virtual void CastFirstSpell() { }
    protected virtual void CastSecondSpell() { }
    protected virtual void CastThirdSpell() { }

}
