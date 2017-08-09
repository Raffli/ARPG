using UnityEngine;

public interface IPlayerAttack {

	void AttackPrimary (GameObject enemy);
	void AttackSecondary ();
	void UseFirstSpell ();
	void UseSecondSpell ();
	void UseThirdSpell ();
	void UseHealPotion ();
	void UseManaPotion ();
}
