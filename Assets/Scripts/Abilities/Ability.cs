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
    public int creatorId;

    [HideInInspector]
    public AbilityType abilityId;

    protected void SendCollision(int actorId){
        ConnectionManager.Instance.QueueReliableElement(new CollisionElement(abilityId, actorId, creatorId));
    }
}