using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class ChatPlayer : NetworkBehaviour
{
    public InputField inputField;

    public override void OnNetworkSpawn()
    {
        if (IsLocalPlayer){
            base.OnNetworkSpawn();
            AddChatServerRpc("Player " + OwnerClientId + " joined the chat");
            inputField = ChatManager.instance.inputField;
            inputField.onSubmit.AddListener(SendMessageFromUI);
        }
    }

    public override void OnNetworkDespawn()
    {
        if (IsLocalPlayer){
            base.OnNetworkDespawn();
            AddChatServerRpc("Player " + OwnerClientId + " left the chat");
        }
    }

    public void SendMessageFromUI(string msg)
    {
        Debug.Log(msg);
        inputField.text = "";
        AddChatServerRpc(msg);
        //inputField.Select();
    }

    [ServerRpc(RequireOwnership = false)]
    void AddChatServerRpc(string v)
    {
        AddChatClientRpc(v);
    }

    [ClientRpc]
    void AddChatClientRpc(string v)
    {
        ChatManager.instance.AddChat(v, OwnerClientId);
    }

}
