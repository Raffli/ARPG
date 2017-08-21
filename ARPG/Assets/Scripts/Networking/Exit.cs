using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.UI;

public class Exit : NetworkBehaviour
{
    public MyNetworkManager manager;
    public string levelToLoad;


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("Mage") || other.transform.tag.Equals("Warrior") || other.transform.tag.Equals("Rouge"))
        {
            manager.SwitchScene(levelToLoad);
        }
    }
}
