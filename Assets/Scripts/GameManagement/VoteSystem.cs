using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoteSystem
{
    public Dictionary<string, List<string>> votesDic = new Dictionary<string, List<string>>();
    Text voteText;
    Text voteTimeText;

    public VoteSystem(Text voteText, Text voteTimeText)
    {
        this.voteText = voteText;
        this.voteTimeText = voteTimeText;
        UpdateVoteText();
    }
    public void Vote(string chatName, string vote)
    {
        foreach (List<string> votes in votesDic.Values)
        {
            votes.Remove(chatName);
        }
        votesDic[vote].Add(chatName);
        UpdateVoteText();
        UpdateVoteTime(0);
    }
    public void AddVote(string voteName)
    {
        votesDic.Add(voteName, new List<string>());
    }

    public string VoteResult()
    {
        string res = "skip";
        int resAmount = votesDic["skip"].Count;
        foreach (string vote in votesDic.Keys)
        {
            int voteAmount = votesDic[vote].Count;
            if (voteAmount > resAmount)
            {
                res = vote;
                resAmount = voteAmount;
            }
        }
        ResetVotes();
        return res;
    }
    
    public void ResetVotes()
    {
        foreach (List<string> votes in votesDic.Values)
        {
            votes.Clear();
        }
        UpdateVoteText();
    }

    void UpdateVoteText()
    {
        string text = "";
        foreach(string vote in votesDic.Keys)
        {
            text += vote + ": " + votesDic[vote].Count + "\n";
        }
        voteText.text = text;
    }
    public void UpdateVoteTime(float time)
    {
        voteTimeText.text = Mathf.RoundToInt(time).ToString();
    }
}
