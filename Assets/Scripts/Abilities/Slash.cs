using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// ----------------------------------------------
/// Class: 	Slash - A script to provide the logic for the Slash ability
///
/// PROGRAM: SKOM
///
/// FUNCTIONS:	void Update()
///             void OnTriggerEnter()
///
/// DATE: 		March April 3rd, 2019
///
/// REVISIONS:  
///
/// DESIGNER: 	Ben Zhang
///
/// PROGRAMMER: Ben Zhang
///
/// NOTES:
/// ----------------------------------------------
public class Slash : Ability
{

    private float timer;
    private const float MAX_TIME = 1f;

    /// ----------------------------------------------
    /// FUNCTION:	Update
    ///
    /// DATE:		April 3rd, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Ben Zhang
    ///
    /// PROGRAMMER:	Ben Zhang
    ///
    /// INTERFACE: 	void Update()
    ///
    /// RETURNS: 	void
    ///
    /// NOTES:		MonoBehaviour function. Called at a fixed interval.
    ///             Destroy the GameObject if it has lived past MAX_TIME.
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
    /// DATE:		April 3rd, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Ben Zhang
    ///
    /// PROGRAMMER:	Ben Zhang
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
