using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawner : MonoBehaviour
{
    public static PlayerSpawner Instance;
    
    [SerializeField] PlayerListUI ui;

    [SerializeField] GameObject defaultSoldier;

    Dictionary<string, GameObject> playerList = new Dictionary<string, GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        ChatCommands.OnPlayerJoin += SpawnPlayer;
        ChatCommands.OnPlayerLeave += RemovePlayer;
    }
    private void OnDisable()
    {
        ChatCommands.OnPlayerJoin -= SpawnPlayer;
        ChatCommands.OnPlayerLeave -= RemovePlayer;
    }

    private void SpawnPlayer(string chatName)
    {
        if (!AmIPlaying(chatName))
        {
            GameObject newSoldier = Instantiate(defaultSoldier, Vector3.zero, Quaternion.identity);
            newSoldier.name = chatName;
            newSoldier.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = chatName;
            InfoCardHandler.ins.MakePlayerInfoCard(newSoldier);
            AddPlayer(chatName, newSoldier);
        }
    }
    private void AddPlayer(string chatName, GameObject player)
    {
        if (!AmIPlaying(chatName))
        {
            playerList.Add(chatName, player);
            ui.UpdateUI(new List<string>(playerList.Keys));
        }
    }
    private void RemovePlayer(string chatName)
    {
        if (AmIPlaying(chatName))
        {
            Destroy(playerList[chatName]);
            playerList.Remove(chatName);
            ui.UpdateUI(new List<string>(playerList.Keys));
        }
    }

    bool AmIPlaying(string name)
    {
        return playerList.ContainsKey(name);
    }
    public void ChangeUnit(GameObject oldUnit, GameObject newUnit)
    {
        //string chatName = oldUnit.name;
        //Stats unitStats = oldUnit.GetComponent<Stats>();
        //Transform unitTransform = oldUnit.transform;
        //GameObject newUnitInst = Instantiate(newUnit, unitTransform.position, unitTransform.rotation);
        //newUnitInst.GetComponent<AI>().stats = unitStats;
        //RemovePlayer(chatName);
        //AddPlayer(chatName, newUnitInst);
        throw new NotImplementedException();
    }
    public List<GameObject> GetAlliedUnits()
    {
        return new List<GameObject>(playerList.Values);
    }
}
