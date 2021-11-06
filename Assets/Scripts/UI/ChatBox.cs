using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatBox : MonoBehaviour
{
    [SerializeField] Text chatText;
    private void Awake()
    {
        chatText = GetComponentInChildren<Text>();
    }
    private void OnEnable()
    {
        ChatInput.OnChatMessageAction += AddMessage;
    }
    private void OnDisable()
    {
        ChatInput.OnChatMessageAction -= AddMessage;
    }
    private void AddMessage(string message)
    {
        chatText.text += "\n" + message;
    }
}
