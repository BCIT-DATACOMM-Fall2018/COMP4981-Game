using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// ----------------------------------------------
/// Class:  WeebOutAbility - A basic small AOE attack
/// 
/// PROGRAM: COMP4981-Game
///
/// FUNCTIONS:  void Start()
///             void Update()
///             void OnCollisionEnter()
///
/// DATE:       March 23rd, 2019
///
/// REVISIONS:  April 7th, 2019
///                 - Reduce MAX_TIME
///
/// DESIGNER:   Simon Wu
///
/// PROGRAMMER: Simon Wu
///
/// NOTES:      
/// ----------------------------------------------
public class WeebOutAbility : Ability
{

    private float timer;
    private const float MAX_TIME =0.2f;


    /// ----------------------------------------------
    /// FUNCTION:   Update
    /// 
    /// DATE:       March 23rd, 2019
    /// 
    /// REVISIONS:  
    /// 
    /// DESIGNER:   Simon Wu
    /// 
    /// PROGRAMMER: Simon Wu
    /// 
    /// INTERFACE:  void Update()
    /// 
    /// RETURNS:    void
    /// 
    /// NOTES:      MonoBehaviour function. Called at a fixed interval.
    ///             Destroy the gameObject after MAX_TIME has passed.
    /// ----------------------------------------------
    void Update(){
        timer += Time.deltaTime;
        if(timer > MAX_TIME){
            Destroy(gameObject);
        }
    }

    /// ----------------------------------------------
    /// FUNCTION:   OnTriggerEnter
    /// 
    /// DATE:       March 14th, 2019
    /// 
    /// REVISIONS:  
    /// 
    /// DESIGNER:   Simon Wu
    /// 
    /// PROGRAMMER: Simon Wu
    /// 
    /// INTERFACE:  void OnTriggerEnter(Collider col)
    /// 
    /// RETURNS:    void 
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