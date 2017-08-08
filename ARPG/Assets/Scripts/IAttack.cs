using UnityEngine;

public interface IAttack {

	void AttackPrimary (GameObject enemy);
	void AttackSecondary ();
	void UseFirstSpell ();
	void UseSecondSpell ();
	void UseThirdSpell ();
}
