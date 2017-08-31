using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ManaPotion : NetworkBehaviour
{

    private int manaAmount;

    public Image icon { get; set; }
    public string description { get; set; }
    public int price { get; set; }
    public float cooldown { get; set; }
    public float cooldownLeft { get; set; }
    public bool onCooldown { get; set; }
	public int skillSlot { get; set; }
	private Player player;

	public void SetProperties(Player player)
    {
		this.player = player;
        price = 50;
        icon = (Image)Resources.Load("UI/manaPotion");
        description = "Potion with refunding magical energy.";
        cooldown = 5f;
        cooldownLeft = 0f;
        onCooldown = false;
		skillSlot = 4;
		ModifyProperty ();
    }

	private void ModifyProperty () {
		manaAmount = Mathf.RoundToInt (player.mana.GetValue () * 0.5f);
	}

    void Update()
    {
        if (onCooldown)
        {
            cooldownLeft -= Time.deltaTime;
			HUDManager.Instance.UpdateCooldown(skillSlot, cooldownLeft, cooldown);
            if (cooldownLeft <= 0)
            {
                onCooldown = false;
            }
        }
    }

    [Command]
    public void CmdUseEffect()
    {
        GameObject manaEffect = (GameObject)Resources.Load("Skills/ManaPotionEffect");
        GameObject obj = Instantiate(manaEffect, GetComponent<NetworkTransform>().gameObject.transform.position, GetComponent<NetworkTransform>().gameObject.transform.rotation, GetComponent<NetworkTransform>().gameObject.transform);
        NetworkServer.Spawn(obj);
        player.IncreaseMana(manaAmount);

    }

    public void Use()
    {
		ModifyProperty ();
        onCooldown = true;
        cooldownLeft = cooldown;
        player.IncreaseMana(manaAmount);
        CmdUseEffect();
    }
}
