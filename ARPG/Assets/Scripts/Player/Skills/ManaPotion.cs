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

    public void SetProperties()
    {
        manaAmount = 50;
        price = 50;
        icon = (Image)Resources.Load("UI/manaPotion");
        description = "Potion with refunding magical energy.";
        cooldown = 5f;
        cooldownLeft = 0f;
        onCooldown = false;
    }

    void Update()
    {
        if (onCooldown)
        {
            cooldownLeft -= Time.deltaTime;
            HUDManager.Instance.UpdateCooldown(4, cooldownLeft, cooldown);
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
    }

    public void Use(Player player)
    {
        onCooldown = true;
        cooldownLeft = cooldown;
        player.IncreaseMana(manaAmount);
        CmdUseEffect();
    }
}
