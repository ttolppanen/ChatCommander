using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitCommands : MonoBehaviour, ICommands
{
    [SerializeField] CommandUI ui;

    List<string> unitCommands = new List<string>();
    public static event Action<ChatCommandInfo> SendInput;

    private void Awake()
    {
        unitCommands.Add("!move");
        ui.SetUnitCommandText(unitCommands);
    }

    void ICommands.ApplyCommand(ChatCommandInfo command)
    {
        if (command.hasArgument)
        {
            SendInput?.Invoke(command);
        }
    }
    bool ICommands.IsMyCommand(string commandName)
    {
        return unitCommands.Contains(commandName);
    }
    //void MakeSandbag(ChatCommandInfo command)
    //{
    //    if (ValidDirection(command.argument))
    //    {
    //        OnBuildSandBagAction?.Invoke(command.chatName, command.argument);
    //    }
    //}
}
