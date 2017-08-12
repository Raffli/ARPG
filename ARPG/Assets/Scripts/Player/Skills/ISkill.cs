using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public interface ISkill {
	
	string skillName { get; set; }
	string skillDescription { get; set; }
	Image skillIcon { get; set; }
	int manaCost { get; set; }
	int baseDamage { get; set; }
	int damage { get; set; }
	float cooldown { get; set; }
	float cooldownLeft { get; set; }
	bool onCooldown { get; set; }

	void SetProperties ();
	void StartCooldown ();
	void Execute (NavMeshAgent playerAgent, GameObject enemy, GameObject spellOrigin);
	void Execute (Transform player);
}
