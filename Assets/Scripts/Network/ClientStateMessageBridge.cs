using System;
using NetworkLibrary;
using UnityEngine;
using System.Collections.Generic;

public class ClientStateMessageBridge : IStateMessageBridge
{
	private GameObjectController objectController;
	private const float POSITION_TOLERANCE = 8f;
	public ClientStateMessageBridge (GameObjectController objectController)
	{
		this.objectController = objectController;
	}
	public void UpdateActorPosition (int actorId, float x, float z){
		try{
			GameObject actor = objectController.GameActors[actorId];
			Vector3 targetPosition = new Vector3(x,actor.transform.position.y,z);
			actor.transform.position = targetPosition;
		} catch	(KeyNotFoundException e){

		}
	}

	public void UpdateActorHealth (int actorId, int newHealth){
		try{
			objectController.GameActors[actorId].GetComponent<Character>().Status.HP = newHealth;
			Debug.Log("Update health of actor " + actorId + " to " +newHealth);

		} catch	(KeyNotFoundException e){

		}
	}

	public void UseTargetedAbility (int actorId, AbilityType abilityId, int targetId){

	}

	public void UseAreaAbility (int actorId, AbilityType abilityId, float x, float z){
        objectController.GameActors[actorId].GetComponent<CharacterAbility>().UseAbility(abilityId, x, z);
    }

	public void ProcessCollision(AbilityType abilityId, int actorHitId, int actorCastId){

	}

	public void SpawnActor(ActorType actorType, int ActorId, float x, float z){
		Debug.Log("Spawn actor " + ActorId + " " + actorType);
		if(actorType == ActorType.AlliedPlayer && ActorId == ConnectionManager.Instance.ClientId){
			objectController.InstantiateObject(ActorType.Player, new Vector3(x,0,z), ActorId);
		} else {
			objectController.InstantiateObject(actorType, new Vector3(x,0,z), ActorId);
		}
	}

	public void SetActorMovement(int actorId, float x, float z, float targetX, float targetZ){
		Debug.Log("Setting actor movement of " + actorId + " position + "+ x+","+z +" target"+targetX+","+targetZ);

		if(actorId == ConnectionManager.Instance.ClientId){
			try {	
				GameObject actor = objectController.GameActors[actorId];
				if(Math.Abs(actor.transform.position.x - x) > POSITION_TOLERANCE || Math.Abs(actor.transform.position.z - z) > POSITION_TOLERANCE){
					Vector3 targetPosition = new Vector3(x,actor.transform.position.y,z);
					actor.transform.position = targetPosition;
				}
			} catch	(KeyNotFoundException e){
				//TODO Error handling
			}
		} else {
			try {	
				GameObject actor = objectController.GameActors[actorId];
				if(Math.Abs(actor.transform.position.x - x) > POSITION_TOLERANCE || Math.Abs(actor.transform.position.z - z) > POSITION_TOLERANCE){
					Vector3 targetPosition = new Vector3(x,actor.transform.position.y,z);
					actor.transform.position = targetPosition;
				}
				actor.GetComponent<AIMovement>().SetTargetPosition(new Vector3(x, 0, z));
			} catch	(KeyNotFoundException e){
				//TODO Error handling
			}
		}
		
	}

	public void SetReady(int clientId, bool ready){

	}

	public void StartGame(int playerNum){
		Debug.Log("StartGame");
		ConnectionManager.Instance.PlayerNum = playerNum;
		ConnectionManager.Instance.gameStarted = true;
	}

}


