using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlliedUnitInputReveiver : MonoBehaviour
{
    AI ai;

    private void OnEnable()
    {
        UnitCommands.SendInput += ReceiveInput;
    }
    private void OnDisable()
    {
        UnitCommands.SendInput -= ReceiveInput;
    }
    private void Awake()
    {
        ai = GetComponent<AI>();
    }

    private void ReceiveInput(ChatCommandInfo command)
    {
        if (command.chatName != gameObject.name) return;

        if (!command.hasArgument) return;
        switch (command.commandName)
        {
            case "!move":
                if (ValidDirection(command.argument))
                {
                    Vector2 dir = DirectionToVector(command.argument);
                    MoveToDir(dir);
                }
                else
                {
                    string[] coordinates = command.argument.Split(',');
                    Vector2 point = new Vector2(float.Parse(coordinates[0]), float.Parse(coordinates[1]));
                    MoveToPoint(point);
                }
                break;
        }
    }

    private void MoveToDir(Vector2 dir)
    {
        Vector2 point = (Vector2)transform.position + dir;
        if (GM.ins.IsInsideMap(point))
        {
            ai.StartMoving((Vector2)transform.position + dir);
        }
    }
    private void MoveToPoint(Vector2 point)
    {
        if (GM.ins.IsInsideMap(point))
        {
            ai.StartMoving(point);
        }
    }

    bool ValidDirection(string dir)
    {
        return dir == "right" || dir == "up" || dir == "left" || dir == "down";
    }
    Vector2 DirectionToVector(string dir)
    {
        switch (dir)
        {
            case "right":
                return Vector2.right;
            case "up":
                return Vector2.up;
            case "left":
                return Vector2.left;
        }
        return Vector2.down;
    }
}
