using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// ----------------------------------------------
/// Class: 	PlayerMovement - A script to provide the logic move actors not controlled
///                      by the player
/// 
/// PROGRAM: SKOM
///
/// FUNCTIONS:	void Start()
///				void Awake()
///             public void SetTargetPosition()
///             void Move()
///
/// DATE: 		March 14th, 2019
///
/// REVISIONS: 
///
/// DESIGNER: 	Phat Le, Cameron Roberts
///
/// PROGRAMMER: Phat Le, Cameron Roberts
///
/// NOTES:		
/// ----------------------------------------------
public class PlayerMovement : ActorMovement
{
    private GameObject terrain;
    
    /// ----------------------------------------------
    /// FUNCTION:	Start
    /// 
    /// DATE:		March 14th, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Phat Le, Cameron Roberts
    /// 
    /// PROGRAMMER:	Phat Le, Cameron Roberts
    /// 
    /// INTERFACE: 	void Start()
    /// 
    /// RETURNS: 	void
    /// 
    /// NOTES:		MonoBehaviour function.
    ///             Called before the first Update().
    /// ----------------------------------------------
    void Start()
    {
        base.Start();
        terrain = GameObject.Find("Terrain");
    }

    /// ----------------------------------------------
    /// FUNCTION:	Update
    /// 
    /// DATE:		March 14th, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Phat Le, Cameron Roberts
    /// 
    /// PROGRAMMER:	Phat Le, Cameron Roberts
    /// 
    /// INTERFACE: 	void Update()
    /// 
    /// RETURNS: 	void
    /// 
    /// NOTES:		MonoBehaviour function. Called every frame.
    ///             Starts or stops the GameObjects animation based
    ///             on if the object is currently moving. Checks if
    ///             the player right clicks and calls SetTargetPosition.
    /// ----------------------------------------------
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (terrain.GetComponent<Collider>().Raycast (ray, out hit, Mathf.Infinity)) {
                SetTargetPosition(hit.point);
                GetComponent<PlayerAbilityController>().CancelMoveToTarget();
            }
        }
        if(Input.GetButtonDown("Stop")) {
            Stop();
            GetComponent<PlayerAbilityController>().CancelMoveToTarget();
        }

        base.Update();
    }

}