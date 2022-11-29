using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

public class NetworkManagerUI : NetworkBehaviour
{
    [SerializeField] private Button serverButton;
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;
    [SerializeField] private Button startButton;

    public string clientIpAddress = "127.0.0.1";

    private void Awake()
    {
        serverButton.onClick.AddListener(() => {
            NetworkManager.Singleton.StartServer();
        });
        hostButton.onClick.AddListener(() => {
            SetHost();
            NetworkManager.Singleton.StartHost();
            startButton.onClick.AddListener(() => {
                NetworkManager.Singleton.SceneManager.LoadScene("Game", LoadSceneMode.Single);
            });
        });
        clientButton.onClick.AddListener(() => {
            SetConnection();
            NetworkManager.Singleton.StartClient();
        });
    }

    public void ChangeInputIpAddress(string input) {
        this.clientIpAddress = input;
    }

    private void SetHost() {
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(
            GetHostIPAddress(),  // The IP address is a string
            (ushort)7777 // The port number is an unsigned short
        );
    }

    private void SetConnection() {
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(
            clientIpAddress,  // The IP address is a string
            (ushort)7777 // The port number is an unsigned short
        );
    }

    private string GetHostIPAddress() {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                Debug.Log(ip.ToString());
                return ip.ToString();
            }
        }
        throw new System.Exception("No network adapters with an IPv4 address in the system!");
    }
}
