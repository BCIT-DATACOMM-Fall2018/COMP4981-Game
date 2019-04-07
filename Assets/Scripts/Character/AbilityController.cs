
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
/// DESIGNER: 	Cameron Roberts, Simon Wu
///
/// PROGRAMMER: Cameron Roberts, Simon Wu
///
/// NOTES:
/// ----------------------------------------------
public class AbilityController : MonoBehaviour
{

    private GameObject testProjectile;
    private GameObject testHomingProjectile;
    private GameObject testAreaOfEffect;
    private GameObject wallAbilityObject;
    private GameObject bulletAbility;
    private GameObject dart;
    private GameObject fireball;
    private GameObject weebOut;
    private GameObject whale;
    private GameObject towerShot;
    private GameObject gungnir;
    private GameObject slash;
    private GameObject healEffect;
    private GameObject pewPew;



    /// ----------------------------------------------
    /// FUNCTION:	Start
    ///
    /// DATE:		March 14th, 2019
    ///
    /// REVISIONS:	March 24th, 2019 - JK - added Wall ability Object
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	Cameron Roberts, Simon Wu
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
        wallAbilityObject = Resources.Load<GameObject>("Ability/WallAbilityObject");
        bulletAbility = Resources.Load<GameObject>("Ability/BulletAbility");
        dart = Resources.Load<GameObject>("Ability/Dart");
        fireball = Resources.Load<GameObject>("Ability/Fireball");
        weebOut = Resources.Load<GameObject>("Ability/WeebOut");
        whale = Resources.Load<GameObject>("Ability/Whale");
        towerShot = Resources.Load<GameObject>("Ability/TowerShot");
        gungnir = Resources.Load<GameObject>("Ability/Gungnir");
        slash = Resources.Load<GameObject>("Ability/Slash");
        healEffect = Resources.Load<GameObject>("Ability/HealEffect");
        pewPew = Resources.Load<GameObject>("Ability/PewPew");

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
        if(AbilityInfo.InfoArray[(int)abilityId].Range != 0){
            transform.LookAt(new Vector3(x, 0, z));
        }

        switch (abilityId)
        {
            case AbilityType.TestProjectile:
                AbilityTestProjectile(x, z, collisionId);
                break;
            case AbilityType.TestAreaOfEffect:
                AbilityTestAreaOfEffect(x, z, collisionId);
                break;
            case AbilityType.Wall:
                AbilityWall(x, z);
                break;
            case AbilityType.Dart:
                Dart(x, z, collisionId);
                break;
            case AbilityType.Fireball:
                AbilityFireball(x, z, collisionId);
                break;
            case AbilityType.WeebOut:
                AbilityWeebOut(x, z, collisionId);
                break;
            case AbilityType.Whale:
                AbilityWhale(x, z, collisionId);
                break;
            case AbilityType.Blink:
                AbilityBlink(x, z);
                break;
            case AbilityType.Gungnir:
                AbilityGungnir(x, z, collisionId);
                break;
            case AbilityType.Slash:
                AbilitySlash(x, z, collisionId);
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
    public virtual void UseTargetedAbility(AbilityType abilityId, GameObject target, int collisionId)
    {
        Debug.Log("Use ability: " + abilityId);

        if (target.transform.position.x != -10)
        {
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
            case AbilityType.Banish:
                AbilityBanish(target);
                break;
            case AbilityType.BulletAbility:
                BulletAbility(target, collisionId);
                break;
            case AbilityType.PorkChop:
                PorkChop(target, collisionId);
                break;
            case AbilityType.Purification:
                Purification(target, collisionId);
                break;
            case AbilityType.UwuImScared:
                AbilityUwuImScared(target);
                break;
            case AbilityType.PewPew:
                AbilityPewPew(target, collisionId);
                break;
            case AbilityType.Sploosh:
                AbilitySploosh(target, collisionId);
                break;
            case AbilityType.TowerAttack:
                AbilityTowerAttack(target, collisionId);
                break;
            default:
                Debug.Log("Attempted to use unrecognized ability: " + abilityId);
                break;
        }
    }

    /// ----------------------------------------------
    /// FUNCTION:	BulletAbility
    ///
    /// DATE:		March 27th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	Phat Le
    ///
    /// INTERFACE: 	public void BulletAbility(AbilityType abilityId, int targetId)
    ///                 GameObject target: Target
    ///                 int collisionId: Collision Id of the target
    ///
    /// RETURNS: 	void
    ///
    /// NOTES:		Homing Bullet Attack
    /// ----------------------------------------------
    private void BulletAbility(GameObject target, int collisionId)
    {
        // Instantiate projectile
        var projectile = Instantiate(bulletAbility, transform.position + new Vector3(0, 5, 0), Quaternion.identity);

        // Set the projectiles target
        projectile.GetComponent<BulletAbility>().target = target;

        // Set its creator id and ability type which will be used later for collisions
        projectile.GetComponent<Ability>().GetComponent<Ability>().creator = gameObject;
        projectile.GetComponent<Ability>().abilityId = AbilityType.BulletAbility;
        projectile.GetComponent<Ability>().collisionId = collisionId;
        GetComponent<Animator>().SetTrigger("attack4");

    }

    /// ----------------------------------------------
    /// FUNCTION:	PorkChop
    ///
    /// DATE:		March 27th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	Phat Le
    ///
    /// INTERFACE: 	public void PorkChop(AbilityType abilityId, int targetId)
    ///                 GameObject target: Target
    ///                 int collisionId: Collision Id of the target
    ///
    /// RETURNS: 	void
    ///
    /// NOTES:		Melee Attack
    /// ----------------------------------------------
    private void PorkChop(GameObject target, int collisionId)
    {
        // TODO Play some sort of animation. No collsion is needed as the
        // abilities effect is instantly applied by the server
        GetComponent<Animator>().SetTrigger("attack3");
        int targetId = target.GetComponent<Actor>().ActorId;
        int casterId = gameObject.GetComponent<Actor>().ActorId;
        StartCoroutine(SendCollisionElement(new CollisionElement(AbilityType.PorkChop, targetId, casterId, collisionId), 0.5f));
    }


    /// ----------------------------------------------
    /// FUNCTION:	Dart
    ///
    /// DATE:		March 27th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	keishi Asai
    ///
    /// INTERFACE: 	public void Dart(AbilityType abilityId, int targetId)
    ///                 GameObject target: Target
    ///                 int collisionId: Collision Id of the target
    ///
    /// RETURNS: 	void
    ///
    /// NOTES:		Range Damage over Time Skill
    /// ----------------------------------------------
    private void Dart(float x, float z, int collisionId)
    {
        // Instantiate projectile
        var projectile = Instantiate(dart, transform.position + new Vector3(0, 5, 0), transform.rotation * dart.transform.rotation);

        // Set the projectiles velocity the direction of the target location
        Vector3 targetLocation = new Vector3(x, 0, z);
        projectile.GetComponent<Rigidbody>().velocity = (targetLocation - transform.position).normalized;

        // Set its creator id and ability type which will be used later for collisions
        projectile.GetComponent<Ability>().creator = gameObject;
        projectile.GetComponent<Ability>().abilityId = AbilityType.Dart;
        projectile.GetComponent<Ability>().collisionId = collisionId;
        GetComponent<Animator>().SetTrigger("attack4");

    }

    /// ----------------------------------------------
    /// FUNCTION:   Purification
    ///
    /// DATE:		March 27th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	keishi Asai
    ///
    /// INTERFACE: 	public void Purification(AbilityType abilityId, int targetId)
    ///                 GameObject target: Target
    ///                 int collisionId: Collision Id of the target
    ///
    /// RETURNS: 	void
    ///
    /// NOTES:		Healing Skill
    /// ----------------------------------------------
    private void Purification(GameObject target, int collisionId)
    {
        // TODO Play some sort of animation. No collsion is needed as the
        // abilities effect is instantly applied by the server
        GetComponent<Animator>().SetTrigger("attack5");
        StartCoroutine(PurificationCoroutine(target, collisionId ,0.5f));
    }

    private IEnumerator PurificationCoroutine(GameObject target ,int collisionId, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        int targetId = target.GetComponent<Actor>().ActorId;
        int casterId = gameObject.GetComponent<Actor>().ActorId;
        StartCoroutine(RemoveObject(Instantiate(healEffect, target.transform.position, Quaternion.identity), 2f));
        StartCoroutine(SendCollisionElement(new CollisionElement(AbilityType.Purification, targetId, casterId, collisionId), 0f));
    }

    private IEnumerator RemoveObject(GameObject target, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(target);
    }




    private void AbilityTestProjectile(float x, float z, int collisionId)
    {
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

    private void AbilityTestAreaOfEffect(float x, float z, int collisionId)
    {
        // Instantiate projectile
        var area = Instantiate(testAreaOfEffect, new Vector3(x, 0.01f, z), Quaternion.identity);

        // Set its creator id and ability type which will be used later for collisions
        area.GetComponent<Ability>().creator = gameObject; ;
        area.GetComponent<Ability>().abilityId = AbilityType.TestAreaOfEffect;
        area.GetComponent<Ability>().collisionId = collisionId;
        GetComponent<Animator>().SetTrigger("attack2");

    }

    private void AbilityFireball(float x, float z, int collisionId)
    {
        // Instantiate projectile
        var projectile = Instantiate(fireball, gameObject.transform.position, Quaternion.identity);

        projectile.GetComponent<FireBallProjectile>().target = new Vector3(x, 0, z);

        // Set its creator id and ability type which will be used later for collisions
        projectile.GetComponent<Ability>().creator = gameObject;
        projectile.GetComponent<Ability>().abilityId = AbilityType.Fireball;
        projectile.GetComponent<Ability>().collisionId = collisionId;
        GetComponent<Animator>().SetTrigger("attack5");
    }
    private void AbilityWeebOut(float x, float z, int collisionId)
    {
        // Instantiate projectile
        var area = Instantiate(weebOut, new Vector3(x, 0.01f, z), Quaternion.identity);

        // Set its creator id and ability type which will be used later for collisions
        area.GetComponent<Ability>().creator = gameObject; ;
        area.GetComponent<Ability>().abilityId = AbilityType.WeebOut;
        area.GetComponent<Ability>().collisionId = collisionId;
        GetComponent<Animator>().SetTrigger("attack2");

    }
    

    private void AbilityWhale(float x, float z, int collisionId)
    {
        // Instantiate projectile
        var area = Instantiate(whale, new Vector3(x, 0.01f, z), Quaternion.identity);

        // Set its creator id and ability type which will be used later for collisions
        area.GetComponent<Ability>().creator = gameObject; ;
        area.GetComponent<Ability>().abilityId = AbilityType.Whale;
        area.GetComponent<Ability>().collisionId = collisionId;
        GetComponent<Animator>().SetTrigger("attack5");

    }

    private void AbilityTestTargeted(GameObject target, int collisionId)
    {
        // TODO Play some sort of animation. No collsion is needed as the
        // abilities effect is instantly applied by the server
        GetComponent<Animator>().SetTrigger("attack3");
        int targetId = target.GetComponent<Actor>().ActorId;
        int casterId = gameObject.GetComponent<Actor>().ActorId;
        StartCoroutine(SendCollisionElement(new CollisionElement(AbilityType.TestTargeted, targetId, casterId, collisionId), 0.5f));
    }

    private void AbilityTestTargetedHoming(GameObject target, int collisionId)
    {
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

    private void AbilityUwuImScared(GameObject target)
    {
        GetComponent<Animator>().SetTrigger("attack5");
        var mesh = target.transform.Find("Mesh");
        Component halo = mesh.GetComponent("Halo"); 
        halo.GetType().GetProperty("enabled").SetValue(halo, true, null); 
        // not sure what else to do here
        // play some sort of animation?
        StartCoroutine(RemoveInvincibilityCoroutine(halo, 3f));
    }

    private IEnumerator RemoveInvincibilityCoroutine(Component halo, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        halo.GetType().GetProperty("enabled").SetValue(halo, false, null);     
    }

    private void AbilityAutoAttack(GameObject target, int collisionId)
    {
        // TODO Play some sort of animation. No collsion is needed as the
        // abilities effect is instantly applied by the server
        GetComponent<Animator>().SetTrigger("attack1");
        int targetId = target.GetComponent<Actor>().ActorId;
        int casterId = gameObject.GetComponent<Actor>().ActorId;
        StartCoroutine(SendCollisionElement(new CollisionElement(AbilityType.AutoAttack, targetId, casterId, collisionId), 0.25f));
        Debug.Log("Play autoattack animation");
    }

    private void AbilityWall(float x, float z)
    {
        // Instantiate Wall ability object
        var area = Instantiate(wallAbilityObject, new Vector3(x, 0.01f, z), gameObject.transform.rotation);

        // Set its creator id and ability type which will be used later for collisions
        area.GetComponent<Ability>().creator = gameObject; ;
        area.GetComponent<Ability>().abilityId = AbilityType.Wall;
        GetComponent<Animator>().SetTrigger("attack2");
    }

    private void AbilityBanish(GameObject target)
    {
        GetComponent<Actor>().banished = true;
        GetComponent<PlayerAbilityController>().CancelMoveToTarget();

    }


    private void AbilityPewPew(GameObject target, int collisionId)
    {
        // Instantiate projectile
        var projectile = Instantiate(pewPew, transform.position + new Vector3(0, 5, 0), Quaternion.identity);

        // Set the projectiles target
        projectile.GetComponent<TestTargetedHomingAbility>().target = target;

        // Set its creator id and ability type which will be used later for collisions
        projectile.GetComponent<Ability>().GetComponent<Ability>().creator = gameObject;
        projectile.GetComponent<Ability>().abilityId = AbilityType.PewPew;
        projectile.GetComponent<Ability>().collisionId = collisionId;
        GetComponent<Animator>().SetTrigger("attack4");
    }

    private void AbilitySploosh(GameObject target, int collisionId)
    {
        // TODO Play some sort of animation. No collsion is needed as the
        // abilities effect is instantly applied by the server
        GetComponent<Animator>().SetTrigger("attack1");
        int targetId = target.GetComponent<Actor>().ActorId;
        int casterId = gameObject.GetComponent<Actor>().ActorId;
        StartCoroutine(SendCollisionElement(new CollisionElement(AbilityType.Sploosh, targetId, casterId, collisionId), 0.25f));
        Debug.Log("Play autoattack animation");


    }

    IEnumerator SendCollisionElement(CollisionElement collisionElement, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        ConnectionManager.Instance.QueueReliableElement(collisionElement);

    }

    private void AbilityBlink(float x, float z)
    {
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<PlayerMovement>().ForceTargetPosition(new Vector3(x, 0, z));
        GetComponent<PlayerAbilityController>().CancelMoveToTarget();
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
    }

    private void AbilityTowerAttack(GameObject target, int collisionId)
    {
        // Instantiate projectile
        var projectile = Instantiate(towerShot, transform.position + new Vector3(0, 5, 0), Quaternion.identity);

        // Set the projectiles target
        projectile.GetComponent<TowerShotAbility>().target = target;

        // Set its creator id and ability type which will be used later for collisions
        projectile.GetComponent<Ability>().GetComponent<Ability>().creator = gameObject;
        projectile.GetComponent<Ability>().abilityId = AbilityType.TowerAttack;
        projectile.GetComponent<Ability>().collisionId = collisionId;
    }

    private void AbilityGungnir(float x, float z, int collisionId)
    {
        // Instantiate projectile
        var projectile = Instantiate(gungnir, transform.position + new Vector3(0, 5, 0), transform.rotation * dart.transform.rotation);
        // Set the projectiles velocity the direction of the target location
        Vector3 targetLocation = new Vector3(x, 0, z);
        projectile.GetComponent<Rigidbody>().velocity = (targetLocation - transform.position).normalized;

        // Set its creator id and ability type which will be used later for collisions
        projectile.GetComponent<Ability>().creator = gameObject;
        projectile.GetComponent<Ability>().abilityId = AbilityType.Gungnir;
        projectile.GetComponent<Ability>().collisionId = collisionId;
        GetComponent<Animator>().SetTrigger("attack4");
    }

    private void AbilitySlash(float x, float z, int collisionId)
    {
        // Instantiate projectile
        var area = Instantiate(slash, new Vector3(x, 0.01f, z), transform.rotation * slash.transform.rotation);
        area.transform.Translate(Vector3.forward*15);
        // Set its creator id and ability type which will be used later for collisions
        area.GetComponent<Ability>().creator = gameObject; ;
        area.GetComponent<Ability>().abilityId = AbilityType.Slash;
        area.GetComponent<Ability>().collisionId = collisionId;
        GetComponent<Animator>().SetTrigger("attack2");

    }
}
