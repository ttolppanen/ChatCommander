using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.ComponentModel;
using System.Net.Sockets;
using UnityEngine.UI;

public class ChatReader : MonoBehaviour
{
    public static ChatReader ins;

    TcpClient twitchClient;
    StreamReader reader;
    StreamWriter writer;

    string username;
    string password;
    string channelName;

    public GameObject soldier;
    public Text chatBox;
    public Text playerListText;
    public Text chatCommandTexts;
    public Text gameCommandTexts;
    Dictionary<string, GameObject> playerList = new Dictionary<string, GameObject>();
    Dictionary<string, System.Action<ChatCommand>> chatCommands = new Dictionary<string, System.Action<ChatCommand>>();
    Dictionary<string, System.Action<ChatCommand>> gameCommands = new Dictionary<string, System.Action<ChatCommand>>();
    Dictionary<string, System.Action> voteCommands = new Dictionary<string, System.Action>();
    VoteSystem voteSystem;
    public Text voteText;
    public Text voteTimeText;
    public int voteMaxTime;
    float voteStartTime;
    private void Awake()
    {
        if (ins == null)
        {
            ins = this;
        }
        else
        {
            Destroy(gameObject);
        }
        ReadChannelData();
    }
    private void Start()
    {
        Connect();
        voteStartTime = Time.time;
        voteSystem = new VoteSystem(voteText, voteTimeText);
        chatCommands.Add("!join", JoinGame);
        chatCommands.Add("!leave", LeaveGame);
        chatCommands.Add("!vote", Vote);
        gameCommands.Add("!move", GiveMoveCommand);
        gameCommands.Add("!sandbag", MakeSandbag);
        AddVote("engineer", BuyEngineer);
        AddVote("medic", BuyMedic);
        AddVote("resource", BuyResources);
        AddVote("gun", BuyGun);
        AddVote("skip", SkipVoting);
        SetCommandTexts();
    }
    private void Update()
    {
        if (!twitchClient.Connected)
        {
            Connect();
        }
        ReadChat();
        HandleVoting();
    }
    #region ChatReading
    private void Connect()
    {
        twitchClient = new TcpClient("irc.chat.twitch.tv", 6667);
        reader = new StreamReader(twitchClient.GetStream());
        writer = new StreamWriter(twitchClient.GetStream());
        writer.WriteLine("PASS " + password);
        writer.WriteLine("NICK " + username);
        writer.WriteLine("USER " + username + " 8 *:" + username);
        writer.WriteLine("JOIN #" + channelName);
        writer.Flush();
    }
    void ReadChat()
    {
        if(twitchClient.Available > 0)
        {
            string message = reader.ReadLine();
            if (message.Contains("PRIVMSG"))
            {
                ChatCommand command = new ChatCommand(message);
                if (command.isValidCommand)
                {
                    if (chatCommands.ContainsKey(command.command))
                    {
                        chatCommands[command.command](command);
                    }
                    if (playerList.ContainsKey(command.chatName) && gameCommands.ContainsKey(command.command))
                    {
                        gameCommands[command.command](command);
                    }
                }
                else
                {
                    string wholeMessage = string.Format("{0}: {1}", command.chatName, command.message);
                    chatBox.text += "\n" + wholeMessage;
                }
            }
            if (message.Contains("PING"))
            {
                writer.WriteLine("PONG :tmi.twitch.tv");
                writer.Flush();
            }
        }
    }
    void ReadChannelData()
    {
        string text = File.ReadAllText(Application.persistentDataPath + "\\streamInfo.txt");
        string[] words = text.Split(' ');
        username = words[0];
        password = words[1];
        channelName = words[2];
    }
    #endregion
    #region HandlePlayerList
    public void AddPlayer(string chatName, GameObject newSoldier)
    {
        GM.ins.allies.Add(newSoldier);
        newSoldier.name = chatName;
        newSoldier.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = chatName;
        playerList.Add(chatName, newSoldier);
        InfoCardHandler.ins.MakePlayerInfoCard(newSoldier);
        UpdatePlayerList();
    }
    void JoinGame(ChatCommand command)
    {
        if (!playerList.ContainsKey(command.chatName) && !command.hasArgument)
        {
            AddPlayer(command.chatName, Instantiate(soldier, Vector3.zero, Quaternion.identity));
        }
    }
    public void RemovePlayer(string chatName)
    {
        GM.ins.allies.Remove(playerList[chatName]);
        Destroy(playerList[chatName]);
        playerList.Remove(chatName);
        UpdatePlayerList();
    }
    void LeaveGame(ChatCommand command)
    {
        if (playerList.ContainsKey(command.chatName) && !command.hasArgument)
        {
            RemovePlayer(command.chatName);
        }
    }
    void UpdatePlayerList()
    {
        string names = "";
        foreach(string playerName in playerList.Keys)
        {
            names += playerName + "\n";
        }
        playerListText.text = names;
    }
    #endregion
    #region GameCommands
    void GiveMoveCommand(ChatCommand command)
    {
        if (command.hasArgument)
        {
            if (UF.ValidDirection(command.argument))
            {
                Transform playerTransform = playerList[command.chatName].transform;
                playerTransform.GetComponent<AI>().SetMovePoint((Vector2)playerTransform.position + UF.DirectionToVector(command.argument));
            }
            else
            {
                string[] coordinates = command.argument.Split(',');
                Vector2 point = new Vector2(float.Parse(coordinates[0]), float.Parse(coordinates[1]));
                playerList[command.chatName].GetComponent<AI>().SetMovePoint(point);
            }
        }
    }
    void MakeSandbag(ChatCommand command)
    {
        if (command.hasArgument && UF.ValidDirection(command.argument))
        {
            if (playerList[command.chatName].GetComponent<UnitAI>().unitType == UnitTypes.engineer)
            {
                playerList[command.chatName].GetComponent<Engineer>().BuildSandBags(command.argument);
            }
        }
    }
    #endregion
    #region Voting
    void Vote(ChatCommand command)
    {
        if (voteSystem.votesDic.ContainsKey(command.argument))
        {
            voteSystem.Vote(command.chatName, command.argument);
        }
    }
    void HandleVoting()
    {
        float timeSinceLastVote = Time.time - voteStartTime;
        voteSystem.UpdateVoteTime(timeSinceLastVote);
        if (timeSinceLastVote >= voteMaxTime)
        {
            voteStartTime = Time.time;
            string voteResult = voteSystem.VoteResult();
            voteCommands[voteResult]();
        }
    }
    void AddVote(string voteName, System.Action vote)
    {
        voteSystem.AddVote(voteName);
        voteCommands.Add(voteName, vote);
    }
    void SkipVoting()
    {
    }
    void BuyGun()
    {
        MoneySystem.ins.BuyGunBox();
    }
    void BuyResources()
    {
        MoneySystem.ins.BuyResourceBox();
    }
    void BuyEngineer()
    {
        MoneySystem.ins.BuyUnitBox(UnitTypes.engineer);
    }
    void BuyMedic()
    {
        MoneySystem.ins.BuyUnitBox(UnitTypes.medic);
    }
    #endregion
    void SetCommandTexts()
    {
        string commands = "";
        foreach(string command in chatCommands.Keys)
        {
            commands += command + "\n";
        }
        chatCommandTexts.text = commands;
        commands = "";
        foreach (string command in gameCommands.Keys)
        {
            commands += command + "\n";
        }
        gameCommandTexts.text = commands;
    }
    public void ChangeUnit(GameObject oldUnit, GameObject newUnit)
    {
        string chatName = oldUnit.name;
        Stats unitStats = oldUnit.GetComponent<AI>().stats;
        Transform unitTransform = oldUnit.transform;
        GameObject newUnitInst = Instantiate(newUnit, unitTransform.position, unitTransform.rotation);
        newUnitInst.GetComponent<AI>().stats = unitStats;
        RemovePlayer(chatName);
        AddPlayer(chatName, newUnitInst);
    }
}
