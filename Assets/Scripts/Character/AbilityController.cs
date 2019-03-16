
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

    private GameObject testProjectile;
    private GameObject testHomingProjectile;

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
    ///             Loads resources.
    /// ----------------------------------------------
    protected virtual void Start()
    {
        testProjectile = Resources.Load<GameObject>("Ability/TestProjectile");
        testHomingProjectile = Resources.Load<GameObject>("Ability/TestHomingProjectile");
    }


    /// ----------------------------------------------
    /// FUNCTION:	UseAreaAbility
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
    public void UseAreaAbility(AbilityType abilityId, float x, float z)
    {
        switch (abilityId)
        {
            case AbilityType.TestProjectile:
                AbilityTestProjectile(x, z);
                break;
            case AbilityType.TestAreaOfEffect:
                AbilityTestAreaOfEffect(x, z);
                break;
            default:
                Debug.Log("Attempted to use unrecognized ability: " + abilityId);
                break;
        }
    }

    /// ----------------------------------------------
    /// FUNCTION:	UseAreaAbility
    /// 
    /// DATE:		March 15th, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Simon Wu
    /// 
    /// PROGRAMMER:	Simon Wu, Cameron Roberts
    /// 
    /// INTERFACE: 	public void UseAbility(AbilityType abilityId, float x, float z)
    ///                 AbilityType abilityId: The ability to be used
    ///                 int targetId: The actor id of the target GameObject
    /// 
    /// RETURNS: 	void
    /// 
    /// NOTES:		Contains logic to use various abilities based on the abilityId passed to
    ///             the function.
    /// ----------------------------------------------
    public void UseTargetedAbility(AbilityType abilityId, int targetId){
        switch (abilityId)
        {
            case AbilityType.TestTargeted:
                AbilityTestTargeted(targetId);
                break;
            case AbilityType.TestTargetedHoming:
                AbilityTestTargetedHoming(targetId);
                break;
            default:
                Debug.Log("Attempted to use unrecognized ability: " + abilityId);
                break;
        }
    }

    private void AbilityTestProjectile(float x, float z){
        // Instantiate projectile
        var projectile = Instantiate(testProjectile, transform.position + new Vector3(0, 5, 0), Quaternion.identity);
        // Set it to ignore collisions with its creator
        Physics.IgnoreCollision(projectile.GetComponent<Collider>(), GetComponent<Collider>());
        
        // Set the projectiles velocity the direction of the target location
        Vector3 targetLocation = new Vector3(x, 0, z);
        projectile.GetComponent<Rigidbody>().velocity = (targetLocation - transform.position).normalized;
        
        // Set its creator id which will be used later for collisions
        projectile.GetComponent<Ability>().creatorId = gameObject.GetComponent<Actor>().ActorId;
        projectile.GetComponent<Ability>().abilityId = AbilityType.TestProjectile;

    }

    private void AbilityTestAreaOfEffect(float x, float z){

    }

    private void AbilityTestTargeted(int targetId){

    }

    private void AbilityTestTargetedHoming(int targetId){
        // Instantiate projectile
        var projectile = Instantiate(testHomingProjectile, transform.position + new Vector3(0, 5, 0), Quaternion.identity);
        // Set it to ignore collisions with its creator
        Physics.IgnoreCollision(projectile.GetComponent<Collider>(), GetComponent<Collider>());
        
        // Set the projectiles target
        projectile.GetComponent<TestTargetedHomingAbility>().target = GameObject.Find("GameController").GetComponent<GameObjectController>().GameActors[targetId];
        
        // Set its creator id which will be used later for collisions
        projectile.GetComponent<Ability>().creatorId = gameObject.GetComponent<Actor>().ActorId;
        projectile.GetComponent<Ability>().abilityId = AbilityType.TestProjectile;

    }
}