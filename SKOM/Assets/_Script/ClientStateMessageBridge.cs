using System;
using NetworkLibrary;

public class ClientStateMessageBridge : IStateMessageBridge
{
	private GameObjectController objectController;

	public ClientStateMessageBridge (GameObjectController objectController)
	{
		this.objectController = objectController;
	}

	public void UpdateActorPosition (int actorId, double x, double y){
	}

	public void UpdateActorHealth (int actorId, int newHealth){
		objectController.GameActors[actorId].GetComponent<Character>().SubtractHP(newHealth);
	}

	public void UseTargetedAbility (int actorId, AbilityType abilityId, int targetId){

	}

	public void UseAreaAbility (int actorId, AbilityType abilityId, int x, int y){

	}

	public void ProcessCollision(AbilityType abilityId, int actorHitId, int actorCastId){

	}

	public void SpawnActor(ActorType actorType, int ActorId, int x, int y){

	}

}


