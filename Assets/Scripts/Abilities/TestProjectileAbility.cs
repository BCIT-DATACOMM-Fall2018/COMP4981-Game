using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// ----------------------------------------------
/// Class: 	TestProjectileAbility - A script to provide the logic to move
///                a projectile shot by the player.
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
public class TestProjectileAbility : Ability
{

    public float speed = 50f;
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
        GetComponent<Rigidbody>().velocity *= speed;
        start = transform.position;
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
        if(Vector3.Distance(start, transform.position) > 50){
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
		Debug.Log("TEST PROJECTILE ABILITY HIT SMTH");
        if(col.gameObject.tag == creator.tag){
            Physics.IgnoreCollision(col, GetComponent<Collider>());

        } else{
			
            SendCollision(col.gameObject.GetComponent<Actor>().ActorId);
            Destroy(gameObject);
        }
    }
}