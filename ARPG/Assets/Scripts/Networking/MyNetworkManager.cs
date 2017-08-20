using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.UI;

public class MyNetworkManager : NetworkManager
{

    public Button rougeButton;
    public Button mageButton;
    public Button warriorButton;

    int avatarIndex = 0;

    public Canvas characterSelectionCanvas;

    void Start()
    {
        rougeButton.onClick.AddListener(delegate { AvatarPicker(rougeButton.name); });
        mageButton.onClick.AddListener(delegate { AvatarPicker(mageButton.name); });
        warriorButton.onClick.AddListener(delegate { AvatarPicker(warriorButton.name); });
    }

    void AvatarPicker(string buttonName)
    {
        switch (buttonName)
        {
            case "Rouge":
                avatarIndex = 0;
                break;
            case "Warrior":
                avatarIndex = 1;
                break;
            case "Mage":
                avatarIndex = 2;
                break;
        }
        playerPrefab = spawnPrefabs[avatarIndex];
    }

    public override void OnClientConnect(NetworkConnection conn)
    {

        characterSelectionCanvas.enabled = false;
        IntegerMessage msg = new IntegerMessage(avatarIndex);

        if (!clientLoadedScene)
        {
            ClientScene.Ready(conn);
            if (autoCreatePlayer)
            {
                ClientScene.AddPlayer(conn, 0, msg);
            }
        }

    }


    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        int id = 0;

        if (extraMessageReader != null)
        {
            IntegerMessage i = extraMessageReader.ReadMessage<IntegerMessage>();
            id = i.value;
        }

        GameObject playerPrefab = spawnPrefabs[id];
        GameObject player;
        Transform startPos = GetStartPosition();
        if (startPos != null)
        {
            player = (GameObject)Instantiate(playerPrefab, startPos.position, startPos.rotation);
        }
        else
        {
            player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        }

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

    public void SwitchScene(string sceneName) {
        ServerChangeScene(sceneName);
    }

}
