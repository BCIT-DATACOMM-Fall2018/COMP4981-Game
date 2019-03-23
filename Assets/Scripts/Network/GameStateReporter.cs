using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetworkLibrary;
using NetworkLibrary.MessageElements;

/// ----------------------------------------------
/// Class: 	GameStateController - A script to report game state to the
///                                server.
/// 
/// PROGRAM: SKOM
///
/// FUNCTIONS:	void Start()
///				void FixedUpdate()
/// 
/// DATE: 		March 14th, 2019
///
/// REVISIONS: 
///
/// DESIGNER: 	Cameron Roberts
///
/// PROGRAMMER: Cameron Roberts
///
/// NOTES:		
/// ----------------------------------------------
public class GameStateReporter : MonoBehaviour
{

    private GameObjectController objectController;


    /// ----------------------------------------------
    /// FUNCTION:	Start
    /// 
    /// DATE:		March 14th, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Cameron Roberts
    /// 
    /// PROGRAMMER:	Cameron Roberts
    /// 
    /// INTERFACE: 	void Start()
    /// 
    /// RETURNS: 	void
    /// 
    /// NOTES:		MonoBehaviour function.
    ///             Called before the first Update().
    ///             Stores a reference to the GameObjectController
    ///             to be used later.
    /// ----------------------------------------------
    void Start()
    {
        objectController = GetComponent<GameObjectController>();
    }

    /// ----------------------------------------------
    /// FUNCTION:	Update
    /// 
    /// DATE:		March 14th, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Cameron Roberts
    /// 
    /// PROGRAMMER:	Cameron Roberts
    /// 
    /// INTERFACE: 	void Update()
    /// 
    /// RETURNS: 	void
    /// 
    /// NOTES:		MonoBehaviour function. Called at a fixed interval.
    ///             Gathers elements that reflect the game state and call
    ///             SendStatePacket to send them to the server.
    /// ----------------------------------------------
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
