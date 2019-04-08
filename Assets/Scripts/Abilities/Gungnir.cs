using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// ----------------------------------------------
/// Class: 	Gungnir - A script to provide the logic for the Gungnir ability
///
/// PROGRAM: SKOM
///
/// FUNCTIONS:	void Start()
///             void Update()
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
public class Gungnir : Ability
{

    public float speed = 50f;
    private Vector3 start;

    /// ----------------------------------------------
    /// FUNCTION:	Start
    ///
    /// DATE:		April 3rd, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Ben Zhang
    ///
    /// PROGRAMMER:	Ben Zhang
    ///
    /// INTERFACE: 	void Start()
    ///
    /// RETURNS: 	void
    ///
    /// NOTES:		MonoBehaviour function.
    ///             Called before the first Update().
    ///             Set the projectile moving forward.
    /// ----------------------------------------------
    void Start ()
    {
        GetComponent<Rigidbody>().velocity *= speed;
        start = transform.position;
    }

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
    ///             Check how far the GameObject has moved from its
    ///             starting point and delete it if it has gone too far.
    /// ----------------------------------------------
    void Update(){
        if(Vector3.Distance(start, transform.position) > 500){
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
    /// NOTES:      When a gameobject from another team collides with this
    ///             GameObject send a collision event to the server. 
    /// ----------------------------------------------
    void OnTriggerEnter (Collider col)
    {
        if(col.gameObject.tag == creator.tag){
            Physics.IgnoreCollision(col, GetComponent<Collider>());
        } else{
            SendCollision(col.gameObject.GetComponent<Actor>().ActorId);
        }
    }
}
