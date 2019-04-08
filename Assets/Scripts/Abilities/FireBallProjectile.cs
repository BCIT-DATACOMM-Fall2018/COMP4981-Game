using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// ----------------------------------------------
/// Class: 	FireBallProjectile - A script to provide log fore the Fireball
///                              abilities projectile
/// 
/// PROGRAM: NetworkLibrary
///
/// FUNCTIONS:	void Update()
///             void OnTriggerEnter()
///
/// DATE: 		April 5th, 2019
///
/// REVISIONS: 
///
/// DESIGNER: 	Cameron Roberts
///
/// PROGRAMMER: Cameron Roberts
///
/// NOTES:		
/// ----------------------------------------------
public class FireBallProjectile : Ability
{

    public float speed = 60f;
    [HideInInspector]
    public Vector3 target;

    public GameObject aoe;

    /// ----------------------------------------------
    /// FUNCTION:	Update
    /// 
    /// DATE:		April 5th, 2019
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
    ///             Move gameObject to wards target location. Once it has
    ///             reached the target location destroy itself and create an
    ///             area of effect at the target position.
    /// ----------------------------------------------
    void Update(){
        if(target!= null){
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        } else{
            Destroy(gameObject);
        }
        if(transform.position == target){
            var effect = Instantiate(aoe, new Vector3(transform.position.x, 0.01f, transform.position.z), Quaternion.identity);

            effect.GetComponent<Ability>().creator = GetComponent<Ability>().creator;
            effect.GetComponent<Ability>().abilityId = GetComponent<Ability>().abilityId;
            effect.GetComponent<Ability>().collisionId = GetComponent<Ability>().collisionId;
            Destroy(gameObject);
        }
    }

}