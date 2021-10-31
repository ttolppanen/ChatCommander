using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatCommand
{
    public string chatName;
    public string command;
    public string argument;
    public string message;
    public bool hasArgument;
    public bool isValidCommand;

    public ChatCommand(string message)
    {
        int splitPoint = message.IndexOf("!", 1);
        string chatName = message.Substring(0, splitPoint);
        this.chatName = chatName.Substring(1);
        splitPoint = message.IndexOf(":", 1);
        this.message = message.Substring(splitPoint + 1);
        string[] messageParsed = this.message.Split(' ');
        if (messageParsed.Length >= 3 || messageParsed.Length == 0)
        {
            isValidCommand = false;
            command = "1337";
            argument = "69";
        }
        else if (messageParsed.Length == 2)
        {
            isValidCommand = true;
            hasArgument = true;
            command = messageParsed[0];
            argument = messageParsed[1];
        }
        else
        {
            isValidCommand = true;
            hasArgument = false;
            command = messageParsed[0];
            argument = "69";
        }
    }
}
