using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetworkLibrary;
using NetworkLibrary.MessageElements;

/// ----------------------------------------------
/// Class: 	Ability - A script to provide the structure of an ability
/// 
/// PROGRAM: NetworkLibrary
///
/// FUNCTIONS:	void Start()
///				void Update()
///             void OnCollisionEnter()
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

    protected void SendCollision(int actorId){
        Debug.Log("Sending collision to the server. Info: AbilityId=" + abilityId + ", actorId=" + actorId + ", creatorId=" + creator.GetComponent<Actor>().ActorId + ", collisionId=" + collisionId);
        ConnectionManager.Instance.QueueReliableElement(new CollisionElement(abilityId, actorId, creator.GetComponent<Actor>().ActorId, collisionId));
    }
}