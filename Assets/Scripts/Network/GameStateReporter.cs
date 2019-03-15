using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetworkLibrary;
using NetworkLibrary.MessageElements;

public class GameStateReporter : MonoBehaviour
{

    private GameObjectController objectController;


    // Start is called before the first frame update
    void Start()
    {
        objectController = GetComponent<GameObjectController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        List<UpdateElement> gameState = new List<UpdateElement>();
        try{
            GameObject player = objectController.GameActors[ConnectionManager.Instance.ClientId];
            Vector3 playerTargetPosition = player.GetComponent<UnityEngine.AI.NavMeshAgent>().steeringTarget;
            gameState.Add(new PositionElement(ConnectionManager.Instance.ClientId, playerTargetPosition.x, playerTargetPosition.z));
            ConnectionManager.Instance.QueueReliableElement(new HealthElement(0,1));
            ConnectionManager.Instance.SendStatePacket(gameState);
            Debug.Log("Sent packet to server");

        } catch(Exception e){
            gameState.Add(new PositionElement(ConnectionManager.Instance.ClientId, 0, 0));
            ConnectionManager.Instance.SendStatePacket(gameState);
            Debug.Log("Sent  emptypacket to server");
        }
    }
}
