using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// ----------------------------------------------
/// Class: 	Fireball - A script to provide the logic for the Fireball AOE ability
///
/// PROGRAM: SKOM
///
/// FUNCTIONS:	void Update()
///             void OnTriggerEnter()
///
/// DATE: 		March 30th, 2019
///
/// REVISIONS:  April 7th,2019
///                 - Change MAX_TIME of Fireball
///
/// DESIGNER: 	Dasha Strigoun
///
/// PROGRAMMER: Dasha Strigoun
///
/// NOTES:
/// ----------------------------------------------
public class Fireball : Ability
{

    private float timer;
    private const float MAX_TIME = 0.2f;


    /// ----------------------------------------------
    /// FUNCTION:	Update
    /// 
    /// DATE:		March 30th, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Dasha Strigoun
    /// 
    /// PROGRAMMER:	Dasha Strigoun
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
    /// DATE:		March 30th, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Dasha Strigoun
    /// 
    /// PROGRAMMER:	Dasha Strigoun
    /// 
    /// INTERFACE: 	void OnTriggerEnter(Collider col)
    /// 
    /// RETURNS: 	void 
    /// 
    /// NOTES:      Send collision when a non Ally gameObject enters the trigger area.
    ///             Prevent triggering on the same object twice.
    /// ----------------------------------------------
    void OnTriggerEnter (Collider col)
    {
        Debug.Log("Collision with area of effect");
        if(col.gameObject.tag == creator.tag){
            Physics.IgnoreCollision(GetComponent<Collider>(), col.gameObject.GetComponent<Collider>());
        } else{
            SendCollision(col.gameObject.GetComponent<Actor>().ActorId);
            Physics.IgnoreCollision(GetComponent<Collider>(), col.gameObject.GetComponent<Collider>());
        }
    }
}