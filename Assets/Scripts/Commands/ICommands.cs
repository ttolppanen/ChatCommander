using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ICommands
{
    void ApplyCommand(ChatCommandInfo command);
    bool IsMyCommand(string commandName);
}
