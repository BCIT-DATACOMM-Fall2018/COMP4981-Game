using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// ----------------------------------------------
/// Class: 	Wall - A script to provide the logic for Wall ability
/// 
/// PROGRAM:    Some Kind of MOBA
///
/// FUNCTIONS:	void Update()
///
/// DATE: 		March 25th, 2019
///
/// REVISIONS: 
///
/// DESIGNER: 	Jason Kim
///
/// PROGRAMMER: Jason Kim
///
/// NOTES:		
/// ----------------------------------------------
public class Wall : Ability
{
    private float timer;
    private const float MAX_TIME = 10f;

    /// ----------------------------------------------
    /// FUNCTION:	Update
    /// 
    /// DATE:		March 25th, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Jason Kim
    /// 
    /// PROGRAMMER:	Jason Kim
    /// 
    /// INTERFACE: 	void Update()
    /// 
    /// RETURNS: 	void
    /// 
    /// NOTES:		MonoBehaviour function. Called at a fixed interval.
    ///             Check how long the object was alive and delete after
    ///             MAX_TIME
    /// ----------------------------------------------
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > MAX_TIME)
        {
            Destroy(gameObject);
        }
    }
}