using System;
using NetworkLibrary;
using UnityEngine;
using System.Collections.Generic;

public class LobbyStateMessageBridge : IStateMessageBridge
{

    //throw exception
    public void UpdateActorPosition(int actorId, float x, float z)
    {

    }

    //throw exception
    public void UpdateActorHealth (int actorId, int newHealth)
    {

    }

    //throw exception
	public void UseTargetedAbility (int actorId, AbilityType abilityId, int targetId, int collisionId)
    {

    }

    //throw exception
	public void UseAreaAbility (int actorId, AbilityType abilityId, float x, float z, int collisionId)
    {

    }

    //throw exception
	public void ProcessCollision(AbilityType abilityId, int actorHitId, int actorCastId, int collisionId)
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

    }

    //throw exception
	public void SetLobbyStatus(List<LobbyStatusElement.PlayerInfo> playerInfo)
    {

    }

    //throw exception
    public void EndGame(int winningTeam)
    {
        
    }
}
