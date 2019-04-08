using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// ----------------------------------------------
/// Class: 	Bullet - A script to provide the logic for the Bullet ability
///
/// PROGRAM: SKOM
///
/// FUNCTIONS:	void Update()
///             void OnTriggerEnter()
///
/// DATE: 		March 27th, 2019
///
/// REVISIONS:
///
/// DESIGNER: 	Cameron Roberts
///
/// PROGRAMMER: Phat Le
///
/// NOTES:
/// ----------------------------------------------
public class BulletAbility : Ability
{

    public float speed = 70f;
    [HideInInspector]
    public GameObject target;


    /// ----------------------------------------------
    /// FUNCTION:	Update
    ///
    /// DATE:		March 27th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	Phat Le
    ///
    /// INTERFACE: 	void Update()
    ///
    /// RETURNS: 	void
    ///
    /// NOTES:		MonoBehaviour function. Called at a fixed interval.
    ///             Move towards the target at a fixed speed.
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
    /// DATE:		March 27th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	Phat Le
    ///
    /// INTERFACE: 	void OnTriggerEnter(Collider col)
    ///
    /// RETURNS: 	void
    ///
    /// NOTES: Send a collision when the bullet hits and destroy the bullet.
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
