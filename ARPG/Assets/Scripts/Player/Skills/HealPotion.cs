using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class HealPotion : NetworkBehaviour {

    private int healAmount;

    public Image icon { get; set; }
    public string description { get; set; }
    public int price { get; set; }
    public float cooldown { get; set; }
    public float cooldownLeft { get; set; }
    public bool onCooldown { get; set; }

    private GameObject healPotionEffect;

    [Command]
    public void CmdSetProperties() {
        healAmount = 50;
        price = 50;
        icon = (Image)Resources.Load("UI/healPotion");
        description = "Potion with a great healing effect.";
        cooldown = 5f;
        cooldownLeft = 0f;
        onCooldown = false;
    }

    void Update() {
        if (onCooldown) {
            cooldownLeft -= Time.deltaTime;
            if (cooldownLeft <= 0) {
                onCooldown = false;
            }
        }
    }

    [Command]
    public void CmdUseEffect() {
       // healPotionEffect = player.transform.Find("HealthPotionEffect").gameObject;
        //healPotionEffect.SetActive(true);
    }

	public void Use (Player player) {
		onCooldown = true;
		player.currentHealth += healAmount;
		if (player.currentHealth > player.maxHealth) {
			player.currentHealth = player.maxHealth;
		}
        CmdUseEffect();

    }

}
