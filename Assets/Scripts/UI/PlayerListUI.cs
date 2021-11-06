using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListUI : MonoBehaviour
{
    [SerializeField] Text playerListText;

    public void UpdateUI(List<string> playerList)
    {
        string names = "";
        foreach (string playerName in playerList)
        {
            names += playerName + "\n";
        }
        playerListText.text = names;
    }
}
