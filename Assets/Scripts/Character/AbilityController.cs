
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetworkLibrary;
using NetworkLibrary.MessageElements;

/// ----------------------------------------------
/// Class: 	AbilityController - A script to provide the logic to use abilities
/// 
/// PROGRAM: SKOM
///
/// FUNCTIONS:	void Start()
///				void Awake()
///             public void UseAbility(AbilityType abilityId, float x, float z)
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
public class AbilityController : MonoBehaviour
{

    public GameObject Projectile;

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
    ///             Currently unused.
    /// ----------------------------------------------
    void Start()
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
    /// NOTES:		MonoBehaviour function. Called every frame.
    ///             Currently unused.
    /// ----------------------------------------------
    void Update()
    {
        
    }

    /// ----------------------------------------------
    /// FUNCTION:	UseAbility
    /// 
    /// DATE:		March 14th, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Simon Wu
    /// 
    /// PROGRAMMER:	Simon Wu, Cameron Roberts
    /// 
    /// INTERFACE: 	public void UseAbility(AbilityType abilityId, float x, float z)
    ///                 AbilityType abilityId: The ability to be used
    ///                 float x: The x coordinate to use for the ability's location
    ///                 float z: The z coordinate to use for the ability's location
    /// 
    /// RETURNS: 	void
    /// 
    /// NOTES:		Contains logic to use various abilities based on the abilityId passed to
    ///             the function.
    /// ----------------------------------------------
    public void UseAbility(AbilityType abilityId, float x, float z)
    {
        var projectile = Instantiate(Projectile, transform.position + new Vector3(0, 10, 0), Quaternion.identity);
        Physics.IgnoreCollision(projectile.GetComponent<Collider>(), GetComponent<Collider>());
        Vector3 targetLocation = new Vector3(x, 0, z);
        projectile.GetComponent<Rigidbody>().velocity = (targetLocation - transform.position).normalized;
        projectile.GetComponent<Shot>().creatorId = gameObject.GetComponent<Character>().ActorId;
    }

}