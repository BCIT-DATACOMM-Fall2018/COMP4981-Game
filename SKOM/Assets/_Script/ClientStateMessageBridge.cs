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

	public void UseActorAbility (int actorId, int abilityId, int targetId, int x, int y){

	}

}


