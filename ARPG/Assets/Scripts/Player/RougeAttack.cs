using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RougeAttack : Attack {

	public GameObject leftSword;
	public GameObject rightSword;

	private SwordAttack leftSwordAttack;
	private SwordAttack rightSwordAttack;


    public override void RpcEquipSkill() {
		leftSwordAttack = leftSword.GetComponent<SwordAttack> ();
		rightSwordAttack = rightSword.GetComponent<SwordAttack> ();

		skills [0] = gameObject.AddComponent <SparklingStrike> () as SparklingStrike;
		skills [0].SetProperties (rightSword);
		skills [1] = gameObject.AddComponent <TwinBlades> () as TwinBlades;
		skills [1].SetProperties (leftSword,rightSword);
		skills [2] = gameObject.AddComponent <PoisonousBlade> () as PoisonousBlade;
		skills [2].SetProperties (player);
		skills [3] = gameObject.AddComponent <Stealth> () as Stealth;
		skills [3].SetProperties (player);
		skills [4] = gameObject.AddComponent <SweepingLotus> () as SweepingLotus;
		skills [4].SetProperties (player);
	}

    public override void CmdEquipSkill()
    {
        leftSwordAttack = leftSword.GetComponent<SwordAttack>();
        rightSwordAttack = rightSword.GetComponent<SwordAttack>();

        skills[0] = gameObject.AddComponent<SparklingStrike>() as SparklingStrike;
        skills[0].SetProperties(rightSword);
        skills[1] = gameObject.AddComponent<TwinBlades>() as TwinBlades;
        skills[1].SetProperties(leftSword, rightSword);
        skills[2] = gameObject.AddComponent<PoisonousBlade>() as PoisonousBlade;
        skills[2].SetProperties(player);
        skills[3] = gameObject.AddComponent<Stealth>() as Stealth;
        skills[3].SetProperties(player);
        skills[4] = gameObject.AddComponent<SweepingLotus>() as SweepingLotus;
        skills[4].SetProperties(player);
    }

    protected override void RpcCastPrimaryAttack() {
		skills [0].Execute ();
		playerAgent.isStopped = false;
		worldInteraction.SetCanInteract (true);
	}

	protected override void RpcCastSecondaryAttack() {
		skills [1].Execute ();
		playerAgent.isStopped = false;
		worldInteraction.SetCanInteract (true);
	}

	protected override void RpcCastFirstSpell() {
		skills [2].Execute ();
		playerAgent.isStopped = false;
		worldInteraction.SetCanInteract (true);
	}

	protected override void RpcCastSecondSpell() {
		skills [3].Execute ();
		playerAgent.isStopped = false;
		worldInteraction.SetCanInteract (true);
	}

	protected override void RpcCastThirdSpell() {
		skills [4].Execute ();
		playerAgent.isStopped = false;
		worldInteraction.SetCanInteract (true);
	}

    protected override void CmdCastPrimaryAttack()
    {
        skills[0].Execute();
        playerAgent.isStopped = false;
        worldInteraction.SetCanInteract(true);
    }

    protected override void CmdCastSecondaryAttack()
    {
        skills[1].Execute();
        playerAgent.isStopped = false;
        worldInteraction.SetCanInteract(true);
    }

    protected override void CmdCastFirstSpell()
    {
        skills[2].Execute();
        playerAgent.isStopped = false;
        worldInteraction.SetCanInteract(true);
    }

    protected override void CmdCastSecondSpell()
    {
        skills[3].Execute();
        playerAgent.isStopped = false;
        worldInteraction.SetCanInteract(true);
    }

    protected override void CmdCastThirdSpell()
    {
        skills[4].Execute();
        playerAgent.isStopped = false;
        worldInteraction.SetCanInteract(true);
    }

    private void RestartPlayerAgent() {
		playerAgent.isStopped = false;
	}

	private void DisableSwords() {
		leftSwordAttack.DisableSword ();
		rightSwordAttack.DisableSword ();
	}
		
}
