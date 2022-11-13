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
    public string clientIpAddress = "127.0.0.1";

    private void Awake()
    {
        serverButton.onClick.AddListener(() => {
            NetworkManager.Singleton.StartServer();
        });
        hostButton.onClick.AddListener(() => {
            SetHost();
            NetworkManager.Singleton.StartHost();
        });
        clientButton.onClick.AddListener(() => {
            SetConnection();
            NetworkManager.Singleton.StartClient();
        });
    }

    public void ChangeInputHost(string input) {
        this.hostIpAddress = input;
    }

    public void ChangeInputIpAddress(string input) {
        this.clientIpAddress = input;
    }


    private void SetHost() {
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(
            hostIpAddress,  // The IP address is a string
            (ushort)7777 // The port number is an unsigned short
        );
    }

    private void SetConnection() {
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(
            clientIpAddress,  // The IP address is a string
            (ushort)7777 // The port number is an unsigned short
        );
    }
}
