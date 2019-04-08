using System;
using NetworkLibrary;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using NetworkLibrary;
using NetworkLibrary.MessageElements;

/// ----------------------------------------------
/// Class:  	LobbyStateMessageBridge - An class to allow message elements
/// 								  	   to update the lobby state.
///
/// PROGRAM: SKOM
///
/// CONSTRUCTORS:	public ClientStateMessageBridge (GameObjectController objectController)
///
/// FUNCTIONS:	public void UpdateActorPosition (int actorId, float x, float z)
///             public void UpdateActorHealth (int actorId, int newHealth)
///             public void UseTargetedAbility (int actorId, AbilityType abilityId, int targetId, int collisionId)
///         	public void UseAreaAbility (int actorId, AbilityType abilityId, float x, float z, int collisionId){
///         	public void ProcessCollision(AbilityType abilityId, int actorHitId, int actorCastId, int collisionId){
///         	public void SpawnActor(ActorType actorType, int actorId, int team, float x, float z){
///         	public void SetActorMovement(int actorId, float x, float z, float targetX, float targetZ){
///         	public void SetReady(int clientId, bool ready, int team){
///         	public void StartGame(int playerNum){
///             public void SetLobbyStatus(List<LobbyStatusElement.PlayerInfo> playerInfo){
///             public void UpdateActorSpeed(int ActorId, int Speed)
///             public void UpdateAbilityAssignment(int actorId, int abilityId)
///         	public void UpdateActorExperience(int actorId, int newExp) {
///             public void UpdateLifeCount (List<RemainingLivesElement.LivesInfo> livesInfo){
///
///
/// DATE: 		April 1st, 2019
///
/// REVISIONS:  
///
/// DESIGNER: 	Rhys Snaydon
///
/// PROGRAMMER: Rhys Snaydon
///
/// NOTES:		The purpose of this class is to provide an interface through
///				which the server can alter the game state.
/// ----------------------------------------------
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

	/// ----------------------------------------------
    /// FUNCTION:	SetActorMovement
    ///
    /// DATE:		March 14th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Rhys Snaydon
    ///
    /// PROGRAMMER:	Rhys Snaydon
    ///
    /// INTERFACE: 	public void StartGame(int playerNum)
    ///
    /// NOTES:		Sets the number of players in the game and
    ///             goes to the Game scene
    /// ----------------------------------------------
	public void StartGame(int playerNum)
    {
        ConnectionManager.Instance.ExitLobbyState(ConnectedPlayers.Count);
        SceneManager.LoadScene("Game");
    }

    /// ----------------------------------------------
    /// FUNCTION:	SetActorMovement
    ///
    /// DATE:		April 1st, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Rhys Snaydon
    ///
    /// PROGRAMMER:	Rhys Snaydon
    ///
    /// INTERFACE: 	public void StartGame(int playerNum)
    ///
    /// NOTES:		Sets the player info for use in the lobby.
    /// ----------------------------------------------
    public void SetLobbyStatus(List<LobbyStatusElement.PlayerInfo> playerInfo)
    {
        ConnectionManager.Instance.playerInfo = playerInfo;
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

    public void UpdateActorExperience(int ActorId, int Experience)
    {

    }

    public void UpdateActorSpeed(int ActorId, int Speed)
    {

    }

    public void UpdateAbilityAssignment(int whatever, int fuckit)
    {

    }

    public void UpdateLifeCount (List<RemainingLivesElement.LivesInfo> livesInfo){

    }
}
