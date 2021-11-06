using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandUI : MonoBehaviour
{
    [SerializeField] ICommands[] commands;
    [SerializeField] Text chatCommandText;
    [SerializeField] Text unitCommandText;

    public void SetChatCommandText(List<string> commands)
    {
        string uiText = chatCommandText.text;
        foreach (string command in commands)
        {
            uiText += command + "\n";
        }
        chatCommandText.text = uiText;
    }
    public void SetUnitCommandText(List<string> commands)
    {
        string uiText = unitCommandText.text;
        foreach (string command in commands)
        {
            uiText += command + "\n";
        }
        unitCommandText.text = uiText;
    }
}
