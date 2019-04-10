using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// ----------------------------------------------
/// Class: 	TestAreaOfEffectAbility - A script to provide the logic for an area of effect
/// 
/// PROGRAM: NetworkLibrary
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
public class TestAreaOfEffectAbility : Ability
{

    private float timer;
    private const float MAX_TIME = 5f;

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
    ///             Check how far the GameObject has moved from its
    ///             starting point and delete it if it has gone too far.
    /// ----------------------------------------------
    void Update(){
        timer += Time.deltaTime;
        if(timer > MAX_TIME){
            Destroy(gameObject);
        }
    }

    /// ----------------------------------------------
    /// FUNCTION:	OnTriggerEnter
    /// 
    /// DATE:		March 14th, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Cameron Roberts
    /// 
    /// PROGRAMMER:	Cameron Roberts
    /// 
    /// INTERFACE: 	void OnTriggerEnter(Collider col)
    /// 
    /// RETURNS: 	void 
    /// 
    /// NOTES:		
    /// ----------------------------------------------
    void OnTriggerEnter (Collider col)
    {
        if(col.gameObject.tag == creator.tag){
            Physics.IgnoreCollision(GetComponent<Collider>(), col.gameObject.GetComponent<Collider>());
        } else{
            SendCollision(col.gameObject.GetComponent<Actor>().ActorId);
            Physics.IgnoreCollision(GetComponent<Collider>(), col.gameObject.GetComponent<Collider>());
        }
    }
}