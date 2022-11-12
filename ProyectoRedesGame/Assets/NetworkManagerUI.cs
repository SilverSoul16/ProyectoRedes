using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button serverButton;
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;

    public string hostIpAddress = "127.0.0.1";

    private void Awake()
    {
        serverButton.onClick.AddListener(() => {
            NetworkManager.Singleton.StartServer();
        });
        hostButton.onClick.AddListener(() => {
            NetworkManager.Singleton.StartHost();
        });
        clientButton.onClick.AddListener(() => {
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(
                hostIpAddress,  // The IP address is a string
                (ushort)7777 // The port number is an unsigned short
            );
            NetworkManager.Singleton.StartClient();
        });
    }

    public void ChangeInputIpAddress(string input) {
        this.hostIpAddress = input;
    }
}
