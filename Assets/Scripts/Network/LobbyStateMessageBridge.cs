using System;
using NetworkLibrary;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using NetworkLibrary;
using NetworkLibrary.MessageElements;

public class LobbyStateMessageBridge : IStateMessageBridge
{
    private List<LobbyStatusElement.PlayerInfo> ConnectedPlayers;

    public LobbyStateMessageBridge(List<LobbyStatusElement.PlayerInfo> ConnectedList)
    {
        this.ConnectedPlayers = ConnectedList;
    }

    //throw exception
    public void UpdateActorPosition(int actorId, float x, float z)
    {

    }

    //throw exception
    public void UpdateActorHealth (int actorId, int newHealth)
    {

    }

    //throw exception
	public void UseTargetedAbility (int actorId, AbilityType abilityId, int targetId, int collissionId)
    {

    }

    //throw exception
	public void UseAreaAbility (int actorId, AbilityType abilityId, float x, float z, int collissionId)
    {

    }

    //throw exception
	public void ProcessCollision(AbilityType abilityId, int actorHitId, int actorCastId, int collissionId)
    {

    }

    //throw exception
	public void SpawnActor(ActorType actorType, int actorId, int actorTeam, float x, float z)
    {

    }

    //throw exception
	public void SetActorMovement(int actorId, float x, float z, float targetX, float targetZ)
    {

    }

    //throw exception
	public void SetReady(int clientId, bool ready, int team)
    {

    }

    //throw exception
	public void StartGame(int playerNum)
    {
        ConnectionManager.Instance.ExitLobbyState(ConnectedPlayers.Count);
        SceneManager.LoadScene("Game");
    }

    public void SetLobbyStatus(List<LobbyStatusElement.PlayerInfo> playerInfo)
    {
        // Debug.Log("Lobby Status");
        // foreach (var item in playerInfo)
        // {
        //     Debug.Log("Id " + item.Id + ", Name " + item.Name + ", Team " + item.Team + ", Ready " + item.ReadyStatus);
        // }
        ConnectedPlayers.Clear();
        foreach (var item in playerInfo)
        {
            ConnectedPlayers.Add(item);
            Debug.Log("Id " + item.Id + ", Name " + item.Name + ", Team " + item.Team + ", Ready " + item.ReadyStatus);
        }
    }

    //throw exception
    public void EndGame(int winningTeam)
    {
        
    }
}
