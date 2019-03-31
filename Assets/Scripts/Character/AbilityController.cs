
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
    private GameObject testAreaOfEffect;
    private GameObject fireball;

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
        testAreaOfEffect = Resources.Load<GameObject>("Ability/TestAreaOfEffect");
        fireball = Resources.Load<GameObject>("Ability/Fireball");
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
    public virtual void UseAreaAbility(AbilityType abilityId, float x, float z, int collisionId)
    {
        transform.LookAt(new Vector3(x, 0, z));

        switch (abilityId)
        {
            case AbilityType.TestProjectile:
                AbilityTestProjectile(x, z, collisionId);
                break;
            case AbilityType.TestAreaOfEffect:
                AbilityTestAreaOfEffect(x, z, collisionId);
                break;
            case AbilityType.Fireball:
                AbilityFireball(x, z, collisionId);
                break;
            default:
                Debug.Log("Attempted to use unrecognized ability: " + abilityId);
                break;
        }
    }

    /// ----------------------------------------------
    /// FUNCTION:	UserTargetedAbility
    ///
    /// DATE:		March 15th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Simon Wu
    ///
    /// PROGRAMMER:	Simon Wu, Cameron Roberts
    ///
    /// INTERFACE: 	public void UseTargetedAbility(AbilityType abilityId, int targetId)
    ///                 AbilityType abilityId: The ability to be used
    ///                 int targetId: The actor id of the target GameObject
    ///
    /// RETURNS: 	void
    ///
    /// NOTES:		Contains logic to use various abilities based on the abilityId passed to
    ///             the function.
    /// ----------------------------------------------
    public virtual void UseTargetedAbility(AbilityType abilityId, GameObject target, int collisionId){
        if(target.transform.position.x != -10){
            transform.LookAt(target.transform.position);
        }
        switch (abilityId)
        {
            case AbilityType.AutoAttack:
                AbilityAutoAttack(target, collisionId);
                break;
            case AbilityType.TestTargeted:
                AbilityTestTargeted(target, collisionId);
                break;
            case AbilityType.TestTargetedHoming:
                AbilityTestTargetedHoming(target, collisionId);
                break;
            case AbilityType.UwuImScared:
                AbilityUwuImScared();
                break;
            default:
                Debug.Log("Attempted to use unrecognized ability: " + abilityId);
                break;
        }
    }

    private void AbilityTestProjectile(float x, float z, int collisionId){
        // Instantiate projectile
        var projectile = Instantiate(testProjectile, transform.position + new Vector3(0, 5, 0), Quaternion.identity);

        // Set the projectiles velocity the direction of the target location
        Vector3 targetLocation = new Vector3(x, 0, z);
        projectile.GetComponent<Rigidbody>().velocity = (targetLocation - transform.position).normalized;

        // Set its creator id and ability type which will be used later for collisions
        projectile.GetComponent<Ability>().creator = gameObject;
        projectile.GetComponent<Ability>().abilityId = AbilityType.TestProjectile;
        projectile.GetComponent<Ability>().collisionId = collisionId;
        GetComponent<Animator>().SetTrigger("attack4");


    }

    private void AbilityTestAreaOfEffect(float x, float z, int collisionId){
        // Instantiate projectile
        var area = Instantiate(testAreaOfEffect, new Vector3(x, 0.01f, z), Quaternion.identity);

        // Set its creator id and ability type which will be used later for collisions
        area.GetComponent<Ability>().creator = gameObject;;
        area.GetComponent<Ability>().abilityId = AbilityType.TestAreaOfEffect;
        area.GetComponent<Ability>().collisionId = collisionId;
        GetComponent<Animator>().SetTrigger("attack2");

    }

    private void AbilityFireball(float x, float z, int collisionId){
        // Instantiate projectile
        var area = Instantiate(fireball, new Vector3(x, 0.01f, z), Quaternion.identity);

        // Set its creator id and ability type which will be used later for collisions
        area.GetComponent<Ability>().creator = gameObject;
        area.GetComponent<Ability>().abilityId = AbilityType.Fireball;
        area.GetComponent<Ability>().collisionId = collisionId;
        GetComponent<Animator>().SetTrigger("attack2");

    }

    private void AbilityTestTargeted(GameObject target, int collisionId){
        // TODO Play some sort of animation. No collsion is needed as the
        // abilities effect is instantly applied by the server
        GetComponent<Animator>().SetTrigger("attack3");
        int targetId = target.GetComponent<Actor>().ActorId;
        int casterId = gameObject.GetComponent<Actor>().ActorId;
        StartCoroutine(SendCollisionElement(new CollisionElement(AbilityType.TestTargeted, targetId, casterId, collisionId), 0.5f));
    }

    private void AbilityTestTargetedHoming(GameObject target, int collisionId){
        // Instantiate projectile
        var projectile = Instantiate(testHomingProjectile, transform.position + new Vector3(0, 5, 0), Quaternion.identity);

        // Set the projectiles target
        projectile.GetComponent<TestTargetedHomingAbility>().target = target;

        // Set its creator id and ability type which will be used later for collisions
        projectile.GetComponent<Ability>().GetComponent<Ability>().creator = gameObject;
        projectile.GetComponent<Ability>().abilityId = AbilityType.TestTargetedHoming;
        projectile.GetComponent<Ability>().collisionId = collisionId;
        GetComponent<Animator>().SetTrigger("attack4");

    }

    private void AbilityUwuImScared(){
        // not sure what else to do here
        // play some sort of animation?
    }

    private void AbilityAutoAttack(GameObject target, int collisionId){
        // TODO Play some sort of animation. No collsion is needed as the
        // abilities effect is instantly applied by the server
        GetComponent<Animator>().SetTrigger("attack1");
        int targetId = target.GetComponent<Actor>().ActorId;
        int casterId = gameObject.GetComponent<Actor>().ActorId;
        StartCoroutine(SendCollisionElement(new CollisionElement(AbilityType.AutoAttack, targetId, casterId, collisionId), 0.25f));
        Debug.Log("Play autoattack animation");
    }

    IEnumerator SendCollisionElement(CollisionElement collisionElement, float delayTime)  {
        yield return new WaitForSeconds(delayTime);
        ConnectionManager.Instance.QueueReliableElement(collisionElement);
    }
}
