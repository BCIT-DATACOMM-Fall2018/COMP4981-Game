using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// ----------------------------------------------
/// Class: 	Bullet - A script to provide the logic for the Dart ability
///
/// PROGRAM: SKOM
///
/// FUNCTIONS:	void Start()
///             void Update()
///             void OnTriggerEnter()
///
/// DATE: 		March 27th, 2019
///
/// REVISIONS:
///
/// DESIGNER: 	Cameron Roberts
///
/// PROGRAMMER: keishi Asai
///
/// NOTES:
/// ----------------------------------------------
public class Dart : Ability
{

    public float speed = 180f;
    private Vector3 start;

    /// ----------------------------------------------
    /// FUNCTION:	Start
    ///
    /// DATE:		March 14th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	keishi Asai
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
    /// DATE:		March 27th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	keishi Asai
    ///
    /// INTERFACE: 	void Update()
    ///
    /// RETURNS: 	void
    ///
    /// NOTES:		MonoBehaviour function. Called at a fixed interval.
    ///             Destroy the object once its gone a certain distance.
    /// ----------------------------------------------
    void Update(){
        if(Vector3.Distance(start, transform.position) > 45){
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
    /// PROGRAMMER:	keishi Asai
    ///
    /// INTERFACE: 	void OnTriggerEnter(Collider col)
    ///
    /// RETURNS: 	void
    ///
    /// NOTES: Send a collision when the dart hits and destroy the dart.
    /// ----------------------------------------------
    void OnTriggerEnter (Collider col)
    {
        if(col.gameObject.tag == creator.tag){
            Physics.IgnoreCollision(col, GetComponent<Collider>());

        } else{
            SendCollision(col.gameObject.GetComponent<Actor>().ActorId);
            Destroy(gameObject);
        }
    }
}
