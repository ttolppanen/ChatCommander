using System;
using System.Collections.Generic;
using UnityEngine;

public class ChatCommands : MonoBehaviour, ICommands
{
    [SerializeField] CommandUI ui;

    Dictionary<string, Action<ChatCommandInfo>> chatCommands = new Dictionary<string, Action<ChatCommandInfo>>();

    public static event Action<string> OnPlayerJoin;
    public static event Action<string> OnPlayerLeave;
    public static event Action<string, string> OnVote;

    private void Awake()
    {
        chatCommands.Add("!join", JoinGame);
        chatCommands.Add("!leave", LeaveGame);
        chatCommands.Add("!vote", Vote);
        List<string> commands = new List<string>(chatCommands.Keys);
        ui.SetChatCommandText(commands);
    }

    void ICommands.ApplyCommand(ChatCommandInfo command)
    {
        chatCommands[command.commandName](command);
    }
    bool ICommands.IsMyCommand(string commandName)
    {
        return chatCommands.ContainsKey(commandName);
    }

    void JoinGame(ChatCommandInfo command)
    {
        OnPlayerJoin?.Invoke(command.chatName);
    }
    void LeaveGame(ChatCommandInfo command)
    {
        OnPlayerLeave?.Invoke(command.chatName);
    }
    void Vote(ChatCommandInfo command)
    {
        OnVote?.Invoke(command.chatName, command.argument);
    }
}
