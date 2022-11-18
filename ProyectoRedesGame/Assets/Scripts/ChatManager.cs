using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.Netcode;

public class ChatManager : NetworkBehaviour
{
    public Text chatText;
    Stack<string> chat = new Stack<string>();
    int maxMsg = 30;
    public static ChatManager instance;
    public InputField inputField;

    void Start()
    {
        instance = this;
        
    }
    
    public void AddChat(string v, ulong id)
    {
        chat.Push(id + "> " + v);
        if (chat.Count > maxMsg) chat.Pop();
        chatText.text = string.Join("\n", chat);
    }

}
