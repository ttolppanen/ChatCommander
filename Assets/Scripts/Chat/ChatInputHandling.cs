using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatInputHandling : MonoBehaviour
{
    ICommands[] commands;
    
    private void OnEnable()
    {
        ChatInput.OnChatCommandAction += ApplyCommand;
    }


    private void OnDisable()
    {
        ChatInput.OnChatCommandAction -= ApplyCommand;
    }
    private void Awake()
    {
        commands = GetComponents<ICommands>();
    }

    private void ApplyCommand(ChatCommandInfo command)
    {
        foreach (ICommands commandHandler in commands)
        {
            if (commandHandler.IsMyCommand(command.commandName))
            {
                commandHandler.ApplyCommand(command);
                return;
            }
        }

    }
}
