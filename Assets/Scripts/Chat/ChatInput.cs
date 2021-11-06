using UnityEngine;
using System.IO;
using System.Net.Sockets;
using System;

public class ChatInput : MonoBehaviour
{
    public static event Action<ChatCommandInfo> OnChatCommandAction;
    public static event Action<string> OnChatMessageAction;

    TcpClient twitchClient;
    StreamReader reader;
    StreamWriter writer;

    StreamerInfo streamerInfo;

    private void Awake()
    {
        ReadChannelData();
    }
    private void Start()
    {
        Connect();
    }

    private void Update()
    {
        if (!twitchClient.Connected)
        {
            print("NOH");
            Connect();
        }
        ReadChat();
    }

    private void Connect()
    {
        twitchClient = new TcpClient("irc.chat.twitch.tv", 6667);
        reader = new StreamReader(twitchClient.GetStream());
        writer = new StreamWriter(twitchClient.GetStream());
        writer.WriteLine("PASS " + streamerInfo.password);
        writer.WriteLine("NICK " + streamerInfo.username);
        writer.WriteLine("USER " + streamerInfo.username + " 8 *:" + streamerInfo.username);
        writer.WriteLine("JOIN #" + streamerInfo.channelName);
        writer.Flush();
    }
    void ReadChat()
    {
        if (twitchClient.Available > 0)
        {
            string message = reader.ReadLine();
            if (message.Contains("PRIVMSG"))
            {
                ChatCommandInfo command = new ChatCommandInfo(message);
                if (command.isValidCommand)
                {
                    OnChatCommandAction?.Invoke(command);
                }
                else
                {
                    string wholeMessage = string.Format("{0}: {1}", command.chatName, command.message);
                    OnChatMessageAction?.Invoke(wholeMessage);
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
        string username = words[0];
        string password = words[1];
        string channelName = words[2];
        streamerInfo = new StreamerInfo(username, password, channelName);
    }
}

public struct StreamerInfo
{
    public string username { get; }
    public string password { get; }
    public string channelName { get; }
    public StreamerInfo(string username, string password, string channelName)
    {
        this.username = username;
        this.password = password;
        this.channelName = channelName;
    }
}
