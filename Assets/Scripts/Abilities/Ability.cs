using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetworkLibrary;
using NetworkLibrary.MessageElements;

/// ----------------------------------------------
/// Class: 	Ability - A script to provide the structure of an ability
/// 
/// PROGRAM: SKOM
///
/// FUNCTIONS:	void Update()
///             void OnTriggerEnter()
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
public abstract class Ability : MonoBehaviour
{
    [HideInInspector]
    public GameObject creator;

    [HideInInspector]
    public AbilityType abilityId;

    [HideInInspector]
    public int collisionId;

    /// ----------------------------------------------
    /// FUNCTION:	SendCollision
    ///
    /// DATE:		March 27th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	Cameron Roberts
    ///
    /// INTERFACE: 	protected void SendCollision(int actorId)
    ///                 int actorId: The actorId of the actor that the collision
    ///                              occured with
    ///
    /// RETURNS: 	void
    ///
    /// NOTES:		Queue a reliable element to be sent to the server 
    ///             notifying them of a collision
    /// ----------------------------------------------
    protected void SendCollision(int actorId){
        Debug.Log("Sending collision to the server. Info: AbilityId=" + abilityId + ", actorId=" + actorId + ", creatorId=" + creator.GetComponent<Actor>().ActorId + ", collisionId=" + collisionId);
        ConnectionManager.Instance.QueueReliableElement(new CollisionElement(abilityId, actorId, creator.GetComponent<Actor>().ActorId, collisionId));
    }
}
