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
using UnityEngine.SceneManagement;

//remove
using UnityEngine.SceneManagement;

/// ----------------------------------------------
/// Class:  	    LobbyManager - A class that manages the
///                                Lobby Scene
///
/// PROGRAM:        SKOM
///
/// CONSTRUCTORS:   N/A
///
/// FUNCTIONS:  	
///
/// DATE: 	    	April 1st, 2019
///
/// REVISIONS:
///
/// DESIGNER:   	Rhys Snaydon
///
/// PROGRAMMER:     Rhys Snaydon
///
/// NOTES:
/// ----------------------------------------------
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
    /// FUNCTION:	Start
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

        //ConnectionManager.Instance.InitConnection("127.0.0.1");

        ReadyState = false;
        ClientId = ConnectionManager.Instance.ClientId;
        Team = 0;

        ConnectionManager.Instance.StartLobbyNetworking(StateBridge, ElementQueue);
    }

    /// ----------------------------------------------
    /// FUNCTION:	Update
    /// 
    /// DATE:		April 1, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Rhys Snaydon
    /// 
    /// PROGRAMMER: Rhys Snaydon
    /// 
    /// INTERFACE: 	void Update()
    /// 
    /// NOTES:		Monobehaviour update method. Called once per frame.
    /// ----------------------------------------------
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

    /// ----------------------------------------------
    /// FUNCTION:	Select
    /// 
    /// DATE:		April 1, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Rhys Snaydon
    /// 
    /// PROGRAMMER: Rhys Snaydon
    /// 
    /// INTERFACE: 	public void Select(GameObject TeamPanel)
    /// 
    /// NOTES:		Called when the user clicks on a panel
    ///             to change their team.
    /// ----------------------------------------------
    public void Select(GameObject TeamPanel)
    {
        Debug.Log("Team Selected" + TeamPanel.name);

        if (TeamPanel.name == "Idle")
        {
            Team = 0;
            ConnectionManager.Instance.Team = 0;
            return;
        }
        if (TeamPanel.name == "TeamA")
        {
            Team = 1;
            ConnectionManager.Instance.Team = 1;
            return;
        }
        if (TeamPanel.name == "TeamB")
        {
            Team = 2;
            ConnectionManager.Instance.Team = 2;
            return;
        }
    }

    /// ----------------------------------------------
    /// FUNCTION:	ToggleReady
    /// 
    /// DATE:		April 1, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Rhys Snaydon
    /// 
    /// PROGRAMMER: Rhys Snaydon
    /// 
    /// INTERFACE: 	public void ToggleReady()
    /// 
    /// NOTES:		Toggles the players ready status
    /// ----------------------------------------------
    public void ToggleReady()
    {
        ReadyState = !ReadyState;
    }

    /// ----------------------------------------------
    /// FUNCTION:	UpdateTexts
    /// 
    /// DATE:		April 1, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Rhys Snaydon
    /// 
    /// PROGRAMMER: Rhys Snaydon
    /// 
    /// INTERFACE: 	private void UpdateTexts()
    /// 
    /// NOTES:		Updates the lobby text based on ready status
    ///             and information from server
    /// ----------------------------------------------
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

    /// ----------------------------------------------
    /// FUNCTION:	ClearTexts
    /// 
    /// DATE:		April 1, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Rhys Snaydon
    /// 
    /// PROGRAMMER: Rhys Snaydon
    /// 
    /// INTERFACE: 	private void UpdateTexts()
    /// 
    /// NOTES:		Clears the lobby texts
    /// ----------------------------------------------
    private void ClearTexts()
    {
        TeamAText.text = "";
        TeamBText.text = "";
        IdleText.text = "";
    }

    /// ----------------------------------------------
    /// FUNCTION:	Logout
    /// 
    /// DATE:		April 1, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Cameron Roberts
    /// 
    /// PROGRAMMER: Cameron Roberts
    /// 
    /// INTERFACE: 	public void LogOut()
    /// 
    /// NOTES:		Exits the lobby and returns to the login scene
    /// ----------------------------------------------
    public void LogOut()
    {
        ConnectionManager.Instance.ExitLobbyToLogin();
    }
}
