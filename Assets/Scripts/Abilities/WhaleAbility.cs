using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// ----------------------------------------------
/// Class:  WhaleAbility - A ultimate AOE heal
/// 
/// PROGRAM: COMP4981-Game
///
/// FUNCTIONS:  void Update()
///             void OnTriggerEnter()
///
/// DATE:       March 23rd, 2019
///
/// REVISIONS: 
///
/// DESIGNER:   Simon Wu
///
/// PROGRAMMER: Simon Wu
///
/// NOTES:      
/// ----------------------------------------------
public class WhaleAbility : Ability
{

    private float timer;
    private const float MAX_TIME = 0.25f;


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
    ///             Check how far the GameObject has moved from its
    ///             starting point and delete it if it has gone too far.
    /// ----------------------------------------------
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > MAX_TIME)
        {
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
    /// NOTES:      Send collision when an Ally gameObject enters the trigger area.
    ///             Prevent triggering on the same object twice.
    /// ----------------------------------------------
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == creator.tag)
        {
            SendCollision(col.gameObject.GetComponent<Actor>().ActorId);
            Physics.IgnoreCollision(GetComponent<Collider>(), col.gameObject.GetComponent<Collider>());
        }
        else
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), col.gameObject.GetComponent<Collider>());
        }
    }
}