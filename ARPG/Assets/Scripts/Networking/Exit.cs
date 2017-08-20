using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.UI;

public class Exit : NetworkBehaviour
{
    public MyNetworkManager manager;



    private void OnTriggerEnter(Collider other)
    {
        manager.SwitchScene("Level1");
    }
}
