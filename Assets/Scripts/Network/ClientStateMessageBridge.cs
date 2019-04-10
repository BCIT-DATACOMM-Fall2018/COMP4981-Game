using System;
using NetworkLibrary;
using NetworkLibrary.MessageElements;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

/// ----------------------------------------------
/// Class:  	ClientStateMessageBridge - An class to allow message elements
/// 								  	   to update the game state.
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
/// DATE: 		March 14th, 2019
///
/// REVISIONS:  April 3rd, 2019 -- Rhys Snaydon
///             - defined prototypes for abilities.
///             April 6th, 2019 -- Ian Lo
///             - added ability function implementation
///             April 7th, 2019 -- Cameron Roberts
///             - added experience and team display funcionality
///
/// DESIGNER: 	Cameron Roberts
///
/// PROGRAMMER: Cameron Roberts
///
/// NOTES:		The purpose of this class is to provide an interface through
///				which the server can alter the game state.
/// ----------------------------------------------
public class ClientStateMessageBridge : IStateMessageBridge
{
	private GameObjectController objectController;
	private const float POSITION_TOLERANCE = 10f;

	/// ----------------------------------------------
	/// CONSTRUCTOR: ClientStateMessageBridge
	///
	/// DATE: 		March 14th, 2019
	///
	/// REVISIONS:
	///
	/// DESIGNER:	Cameron Roberts
	///
	/// PROGRAMMER:	Cameron Roberts
	///
	/// INTERFACE: 	public ClientStateMessageBridge (GameObjectController objectController)
	///					GameObjectController objectController: A GameObjectController through
	///										which the bridge can create and modify GameObjects.
	///
	/// NOTES:
	/// ----------------------------------------------
	public ClientStateMessageBridge (GameObjectController objectController)
	{
		this.objectController = objectController;
	}

	/// ----------------------------------------------
    /// FUNCTION:	UpdateActorPosition
    ///
    /// DATE:		March 14th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	Cameron Roberts
    ///
    /// INTERFACE: 	public void UpdateActorPosition (int actorId, float x, float z)
	///					int actorId: The actor id of the GameObject to change the position of
	///					float x: The x position to move the actor to
	///					float z: The z position to move the actor to
    ///
    /// NOTES:		A function to change the position of an actor
    /// ----------------------------------------------
	public void UpdateActorPosition (int actorId, float x, float z){
		try{
			GameObject actor = objectController.GameActors[actorId];
			Vector3 targetPosition = new Vector3(x,actor.transform.position.y,z);
			actor.transform.position = targetPosition;
		} catch	(KeyNotFoundException e){

		}
	}

	/// ----------------------------------------------
    /// FUNCTION:	UpdateActorHealth
    ///
    /// DATE:		March 14th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	Cameron Roberts
    ///
    /// INTERFACE: 		public void UpdateActorHealth (int actorId, int newHealth)
	///					int actorId: The actor id of the GameObject to change the health of
	///					int newHealth: The actors new health value
    ///
    /// NOTES:		A function to change the health of an actor
    /// ----------------------------------------------
	public void UpdateActorHealth (int actorId, int newHealth){
		try{
			objectController.GameActors[actorId].GetComponent<Actor>().Status.HP = newHealth;

		} catch	(KeyNotFoundException e){

		}
	}

	/// ----------------------------------------------
    /// FUNCTION:	UseTargetAbility
    ///
    /// DATE:		March 14th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:
    ///
    /// INTERFACE: 	UseTargetedAbility (int actorId, AbilityType abilityId, int targetId)
	///					int actorId: The actor id of the GameObject to instruct to use an ability
	///					AbiltyType abilityId: The id of the ability to use
	///					int targetId: The actor to use the ability on
    ///
    /// NOTES:		A function to instruct an actor to use an ability on another actor.
    /// ----------------------------------------------
	public void UseTargetedAbility (int actorId, AbilityType abilityId, int targetId, int collisionId){
        objectController.GameActors[actorId].GetComponent<AbilityController>().UseTargetedAbility(abilityId, objectController.GameActors[targetId], collisionId);
	}

	/// ----------------------------------------------
    /// FUNCTION:	UseAreaAbility
    ///
    /// DATE:		March 14th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	Simon Wu
    ///
    /// INTERFACE: 	public void UseAreaAbility (int actorId, AbilityType abilityId, float x, float z)
	///					int actorId: The actor id of the GameObject to instruct to use an ability
	///					AbiltyType abilityId: The id of the ability to use
	///					float x: The x position to use the ability on
	///					float z: The z position to use the ability on
    ///
    /// NOTES:		A function to instruct an actor to use an ability on a location.
    /// ----------------------------------------------
	public void UseAreaAbility (int actorId, AbilityType abilityId, float x, float z, int collisionId){
        objectController.GameActors[actorId].GetComponent<AbilityController>().UseAreaAbility(abilityId, x, z, collisionId);
    }

	/// ----------------------------------------------
    /// FUNCTION:	ProcessCollision
    ///
    /// DATE:		March 14th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:
    ///
    /// INTERFACE: 	public void ProcessCollision(AbilityType abilityId, int actorHitId, int actorCastId)
    ///
    /// NOTES:		Function not intended to be used on the client.
    /// ----------------------------------------------
	public void ProcessCollision(AbilityType abilityId, int actorHitId, int actorCastId, int collisionId){

	}

	/// ----------------------------------------------
    /// FUNCTION:	SpawnActor
    ///
    /// DATE:		March 14th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	Cameron Roberts
    ///
    /// INTERFACE: 	public void SpawnActor(ActorType actorType, int ActorId, int team, float x, float z)
	///					ActorType actorType: The type of actor to spawn
	///					int actorId: An id to assign the newly created actor
    ///                 int team: The team the actor should be on
	///					float x: The x position to spawn the actor in
	///					float z: The z position to spawn the actor in
    ///
    /// NOTES:		A function to spawn new actors
    /// ----------------------------------------------
	public void SpawnActor(ActorType actorType, int actorId, int team, float x, float z){
        {
			objectController.InstantiateObject(actorType, new Vector3(x,0,z), actorId, team);
        }
	}

	/// ----------------------------------------------
    /// FUNCTION:	SetActorMovement
    ///
    /// DATE:		March 14th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	Cameron Roberts
    ///
    /// INTERFACE: 	public void SpawnActor(ActorType actorType, int ActorId, float x, float z)
	///					int actorId: An id to assign the newly created actor
	///					float x: The x position the actor should currently be at
	///					float z: The z position the actor should currently be at
	///					float targetX: The x position the actor is moving to
	///					float targetZ: The z position the actor is moving to
    ///
    /// NOTES:		A function to control the movement of actors based on their current
	///				and target position. Behaviour differs if the actor is the player character.
    /// ----------------------------------------------
	public void SetActorMovement(int actorId, float x, float z, float targetX, float targetZ){
        GameObject actor = objectController.GameActors[actorId];
        bool enableAgent = false;
        if(x == -10 && z == -10 && targetX == -10 && targetZ == -10){
            actor.GetComponent<Actor>().Die();
            actor.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        } else {
            enableAgent = true;
        }
        if(actor.GetComponent<Actor>().banished){
            actor.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        }
		if(actorId == ConnectionManager.Instance.ClientId){
			try {
				if(Math.Abs(actor.transform.position.x - x) > POSITION_TOLERANCE || Math.Abs(actor.transform.position.z - z) > POSITION_TOLERANCE){
					Vector3 targetPosition = new Vector3(x,actor.transform.position.y,z);
					actor.transform.position = targetPosition;
				}
                if(enableAgent){
                    actor.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
                }
			} catch	(KeyNotFoundException e){
			}
            if(actor.GetComponent<Actor>().banished){
                actor.GetComponent<PlayerAbilityController>().CancelMoveToTarget();
            }
		} else {
			try {
				if(Math.Abs(actor.transform.position.x - x) > POSITION_TOLERANCE || Math.Abs(actor.transform.position.z - z) > POSITION_TOLERANCE){
                    Vector3 targetPosition = new Vector3(x,actor.transform.position.y,z);
                    actor.transform.position = targetPosition;
                }
                if(enableAgent){
                    actor.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
                }
                actor.GetComponent<ActorMovement>().SetTargetPosition(new Vector3(x, 0, z));
			} catch	(KeyNotFoundException e){
			}
		}
        if(actor.GetComponent<Actor>().banished){
            actor.GetComponent<Actor>().banished = false;
            actor.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
        }

	}

	/// ----------------------------------------------
    /// FUNCTION:	SetReady
    ///
    /// DATE:		March 14th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:
    ///
    /// INTERFACE: 	public void SetReady(int clientId, bool ready, int team)
    ///
    /// NOTES:		Function not intended to be used on the client.
    /// ----------------------------------------------
	public void SetReady(int clientId, bool ready, int team){

	}

	/// ----------------------------------------------
    /// FUNCTION:	SetActorMovement
    ///
    /// DATE:		March 14th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	Cameron Roberts
    ///
    /// INTERFACE: 	public void StartGame(int playerNum)
    ///
    /// NOTES:		Unused
    /// ----------------------------------------------
	public void StartGame(int playerNum){
	}

    /// ----------------------------------------------
    /// FUNCTION:	SetLobbyStatus
    ///
    /// DATE:		April 7th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	Cameron Roberts
    ///
    /// INTERFACE: 	public void SetLobbyStatus(List<LobbyStatusElement.PlayerInfo> playerInfo)
    ///
    /// NOTES:		Unused
    /// ----------------------------------------------
    public void SetLobbyStatus(List<LobbyStatusElement.PlayerInfo> playerInfo){
        
    }

    public void EndGame(int winningTeam){
        if(ConnectionManager.Instance.Team == winningTeam){
            GameObject.Find("EndText").GetComponent<Text>().text = "You Win";
        } else {
            GameObject.Find("EndText").GetComponent<Text>().text = "You Lose";
        }
        ConnectionManager.Instance.GameOver = true;
    }

    /// ----------------------------------------------
    /// FUNCTION:	UpdateActorSpeed
    ///
    /// DATE:		April 7th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	Cameron Roberts
    ///
    /// INTERFACE: 	public void UpdateActorSpeed(int ActorId, int Speed)
    ///
    /// NOTES:		Unused
    /// ----------------------------------------------
    public void UpdateActorSpeed(int ActorId, int Speed)
    {

    }


	/// ----------------------------------------------
	/// FUNCTION:	UpdateAbilityAssignment
	///
	/// DATE:		April 3th, 2019
	///
	/// REVISIONS:  April 6th, 2019 -- Ian Lo
	///             - implemented function action.
	///
	/// DESIGNER:	Rhys Snaydon, Ian Lo
	///
	/// PROGRAMMER:	Ian Lo
	///
	/// INTERFACE: 	void UpdateAbilityAssignment(int actorId, int abilityId)
	///
	/// RETURNS: 	void
	///
	/// NOTES:		Assigns the abilityId provided from the server to the
	///             corresponding actorId in the game.
	/// ----------------------------------------------
    public void UpdateAbilityAssignment(int actorId, int abilityId)
    {
		Debug.Log ("skill added: "+ abilityId);
		objectController.GameActors[actorId].GetComponent<PlayerAbilityController>().addAbility(abilityId);
    }

    /// ----------------------------------------------
    /// FUNCTION:	UpdateActorExperience
    ///
    /// DATE:		April 7th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	Cameron Roberts
    ///
    /// INTERFACE: 	public void UpdateActorExperience(int actorId, int newExp)
    ///
    /// NOTES:		Sets the experience display UI element based on the supplied value
    ///             Only sets display for clients experience and ignores all other clients exp.
    /// ----------------------------------------------
	public void UpdateActorExperience(int actorId, int newExp) {
        if(actorId == ConnectionManager.Instance.ClientId){
            GameObject.Find("AbilityBar/Canvas/ExpBar").GetComponent<ExpBarControl>().exp = newExp;
        }
	}

    /// ----------------------------------------------
    /// FUNCTION:	UpdateLifeCount
    ///
    /// DATE:		April 6th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	Cameron Roberts
    ///
    /// INTERFACE: 	public void UpdateLifeCount (List<RemainingLivesElement.LivesInfo> livesInfo)
    ///
    /// NOTES:		Updates the life counter UI element.
    /// ----------------------------------------------
    public void UpdateLifeCount (List<RemainingLivesElement.LivesInfo> livesInfo){
        if(livesInfo.Count != 2){
            return;
        }
        var lifeCounter = GameObject.Find("GameMapPrefab/Canvas/GlobalLifeCounter");
        var text = lifeCounter.GetComponent<Text>();
        text.text = livesInfo[0].Lives + " | " + livesInfo[1].Lives;
    }
}
