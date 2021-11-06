using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoteCommands : MonoBehaviour
{
    VoteSystem voteSystem;
    public Text voteText;
    public Text voteTimeText;
    public int voteMaxTime;
    float voteStartTime;

    Dictionary<string, System.Action> voteCommands = new Dictionary<string, System.Action>();

    private void OnEnable()
    {
        ChatCommands.OnVote += Vote;
    }
    private void OnDisable()
    {
        ChatCommands.OnVote -= Vote;
    }

    private void Start()
    {
        voteCommands.Add("engineer", BuyEngineer);
        voteCommands.Add("medic", BuyMedic);
        voteCommands.Add("resource", BuyResources);
        voteCommands.Add("gun", BuyGun);
        voteCommands.Add("skip", SkipVoting);
        voteStartTime = Time.time;
        List<string> votes = new List<string>(voteCommands.Keys);
        voteSystem = new VoteSystem(voteText, voteTimeText, votes);
    }

    private void Update()
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

    private void Vote(string chatName, string argument)
    {
        if (voteSystem.IsValidVote(argument))
        {
            voteSystem.Vote(chatName, argument);
        }
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
    void SkipVoting()
    {
    }
}
