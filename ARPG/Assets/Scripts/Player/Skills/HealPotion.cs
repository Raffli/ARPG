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

    public void SetProperties() {
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
			HUDManager.Instance.UpdateCooldown(3, cooldownLeft, cooldown);
            if (cooldownLeft <= 0) {
                onCooldown = false;
            }
        }
    }

    [Command]
    public void CmdUseEffect() {
        GameObject healEffect = (GameObject)Resources.Load("Skills/HealthPotionEffect");
        GameObject obj = Instantiate(healEffect, GetComponent<NetworkTransform>().gameObject.transform.position, GetComponent<NetworkTransform>().gameObject.transform.rotation, GetComponent<NetworkTransform>().gameObject.transform);
        NetworkServer.Spawn(obj);
    }

	public void Use (Player player) {
		onCooldown = true;
		cooldownLeft = cooldown;
		player.Heal (healAmount);
        CmdUseEffect();
    }
}
