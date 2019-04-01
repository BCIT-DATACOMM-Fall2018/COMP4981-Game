using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// ----------------------------------------------
/// Class:  WhaleAbility - A ultimate AOE heal (15%)
/// 
/// PROGRAM: COMP4981-Game
///
/// FUNCTIONS:  void Start()
///             void Update()
///             void OnCollisionEnter()
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
    private const float MAX_TIME = 40f;

    /// ----------------------------------------------
    /// FUNCTION:   Start
    /// 
    /// DATE:       March 23rd, 2019
    /// 
    /// REVISIONS:  
    /// 
    /// DESIGNER:   Simon Wu
    /// 
    /// PROGRAMMER: Simon Wu
    /// 
    /// INTERFACE:  void Start()
    /// 
    /// RETURNS:    void
    /// 
    /// NOTES:      MonoBehaviour function.
    ///             Called before the first Update().
    /// ----------------------------------------------
    void Start()
    {

    }

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
    /// FUNCTION:   OnCollisionEnter
    /// 
    /// DATE:       March 14th, 2019
    /// 
    /// REVISIONS:  
    /// 
    /// DESIGNER:   Simon Wu
    /// 
    /// PROGRAMMER: Simon Wu
    /// 
    /// INTERFACE:  void OnCollisionEnter(Collision col)
    /// 
    /// RETURNS:    void 
    /// 
    /// NOTES:      
    /// ----------------------------------------------
    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Collision with area of effect");
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