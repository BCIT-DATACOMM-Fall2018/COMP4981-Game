using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Net;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using NetworkLibrary;
using NetworkLibrary.MessageElements;

//remove
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    private ConcurrentQueue<UpdateElement> ElementQueue {get; set;}
    private List<LobbyStatusElement.PlayerInfo> ConnectedPlayers;
    private LobbyStateMessageBridge StateBridge;
    public bool ReadyState{get;set;}
    public int ClientId {get; set;}
    public int Team {get; set;}

    private float UpdateTime = 0.1f;
    private float Timer = 0;
    public Text TeamAText;
    public Text IdleText;
    public Text TeamBText;
    public Text ReadyText;

    /// ----------------------------------------------
    /// FUNCTION:	LobbyNetworking
    /// 
    /// DATE:		April 1, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Rhys Snaydon
    /// 
    /// PROGRAMMER: Rhys Snaydon
    /// 
    /// INTERFACE: 	void Start()
    /// 
    /// NOTES:		Monobehaviour start method. Called on instantiation.
    /// ----------------------------------------------
    void Start()
    {
        ElementQueue = new ConcurrentQueue<UpdateElement>();
        ConnectedPlayers = new List<LobbyStatusElement.PlayerInfo>();
        StateBridge = new LobbyStateMessageBridge(ConnectedPlayers);

        ConnectionManager.Instance.InitConnection("127.0.0.1");

        ReadyState = false;
        ClientId = ConnectionManager.Instance.ClientId;
        Team = 0;

        ConnectionManager.Instance.StartLobbyNetworking(StateBridge, ElementQueue);
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;

        if (Timer > UpdateTime)
        {
            Timer = 0;
            List<UpdateElement> readyList = new List<UpdateElement>();
            readyList.Add(new ReadyElement(ReadyState, ClientId, Team));
            ConnectionManager.Instance.SendLobbyHeartbeat(readyList);
        }
        

        UpdateElement LobbyStatusUpdate;
        while(ElementQueue.TryDequeue(out LobbyStatusUpdate)){
            LobbyStatusUpdate.UpdateState(StateBridge);
        }

        UpdateTexts();
    }

    public void Select(GameObject TeamPanel)
    {
        Debug.Log("Team Selected" + TeamPanel.name);

        if (TeamPanel.name == "Idle")
        {
            Team = 0;
            return;
        }
        if (TeamPanel.name == "TeamA")
        {
            Team = 1;
            return;
        }
        if (TeamPanel.name == "TeamB")
        {
            Team = 2;
            return;
        }
    }

    public void ToggleReady()
    {
        ReadyState = !ReadyState;
    }

    private void UpdateTexts()
    {
        ClearTexts();

        if (ReadyState)
        {
            ReadyText.text = "Unready";
        } 
        else
        {
            ReadyText.text = "Ready";
        }

        foreach (var Player in ConnectedPlayers)
        {
            switch(Player.Team)
            {
                case 1:
                    TeamAText.text += Player.Name;
                    TeamAText.text += "\n";
                break;
                case 2:
                    TeamBText.text += Player.Name;
                    TeamBText.text += "\n";
                break;
                case 0:
                    IdleText.text += Player.Name;
                    IdleText.text += "\n";
                break;
                default:
                break;
            }
        }

    }

    private void ClearTexts()
    {
        TeamAText.text = "";
        TeamBText.text = "";
        IdleText.text = "";
    }

    // public void selectTeamA()
    // {
    //     a1.text = "Player 1";
    //     a2.text = "";
    //     a3.text = "";

    // }

    // public void selectTeamB()
    // {
    //     a1.text = "";
    //     a2.text = "";
    //     a3.text = "Player 1";

    // }

    // public void selectTeamIdle()
    // {
    //     a1.text = "";
    //     a2.text = "Player 1";
    //     a3.text = "";

    // }
}
