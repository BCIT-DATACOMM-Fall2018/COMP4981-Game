using System;
using NetworkLibrary;
using UnityEngine;
using System.Collections.Generic;

public class ClientStateMessageBridge : IStateMessageBridge
{
	private GameObjectController objectController;

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
			objectController.GameActors[actorId].GetComponent<Character>().SubtractHP(newHealth);
			Debug.Log("Update health of actor " + actorId + " by " +newHealth);

		} catch	(KeyNotFoundException e){

		}
	}

	public void UseTargetedAbility (int actorId, AbilityType abilityId, int targetId){

	}

	public void UseAreaAbility (int actorId, AbilityType abilityId, float x, float z){

	}

	public void ProcessCollision(AbilityType abilityId, int actorHitId, int actorCastId){

	}

	public void SpawnActor(ActorType actorType, int ActorId, float x, float z){
		Debug.Log("Spawn actor " + ActorId + " " + actorType);
		objectController.InstantiateObject(actorType, new Vector3(x,0,z), ActorId);
	}

	public void SetActorMovement(int actorId, float x, float z, float targetX, float targetZ){
		Debug.Log("Setting actor movement");
		try{	
			GameObject actor = objectController.GameActors[actorId];
			if(Math.Abs(actor.transform.position.x - x) > 1F || Math.Abs(actor.transform.position.z - z) > 1F){
				Vector3 targetPosition = new Vector3(x,actor.transform.position.y,z);
				actor.transform.position = targetPosition;
			}
			actor.GetComponent<AIMovement>().SetTargetPosition(new Vector3(x, 0, z));
		} catch	(KeyNotFoundException e){

		}
	}

}


