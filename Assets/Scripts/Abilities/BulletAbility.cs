using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// ----------------------------------------------
/// Class: 	TestTargetedHomingAbility - A script to provide the logic to move
///                a projectile shot by the player the homes in on a target.
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
public class BulletAbility : Ability
{

    public float speed = 70f;
    [HideInInspector]
    public GameObject target;

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
    /// ----------------------------------------------
    void Start ()
    {

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
    ///             Check how far the GameObject has moved from its
    ///             starting point and delete it if it has gone too far.
    /// ----------------------------------------------
    void Update(){
        if(target!= null){
            Vector3 targetLocation = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetLocation, speed * Time.deltaTime);
        } else{
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
    /// INTERFACE: 	void OnCollisionEnter(Collider col)
    ///
    /// RETURNS: 	void
    ///
    /// NOTES:
    /// ----------------------------------------------
    void OnTriggerEnter (Collider col)
    {
        if(col.gameObject != target){
            Physics.IgnoreCollision(GetComponent<Collider>(), col);
        } else{
            SendCollision(col.gameObject.GetComponent<Actor>().ActorId);
            Destroy(gameObject);
        }
    }
}
