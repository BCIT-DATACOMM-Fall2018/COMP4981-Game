
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
///             public virtual void UseAreaAbility(AbilityType abilityId, float x, float z, int collisionId)
///             public virtual void UseTargetedAbility(AbilityType abilityId, GameObject target, int collisionId)
///             private void BulletAbility(GameObject target, int collisionId)
///             private void PorkChop(GameObject target, int collisionId)
///             private void Dart(float x, float z, int collisionId)
///             private void Purification(GameObject target, int collisionId)
///             private IEnumerator PurificationCoroutine(GameObject target ,int collisionId, float waitTime)
///             private IEnumerator RemoveObject(GameObject target, float waitTime)
///             private void AbilityTestProjectile(float x, float z, int collisionId)
///             private void AbilityTestAreaOfEffect(float x, float z, int collisionId)
///             private void AbilityFireball(float x, float z, int collisionId)
///             private void AbilityWeebOut(float x, float z, int collisionId)
///             private void AbilityWhale(float x, float z, int collisionId)
///             private void AbilityTestTargeted(GameObject target, int collisionId)
///             private void AbilityTestTargetedHoming(GameObject target, int collisionId)
///             private void AbilityUwuImScared(GameObject target)
///             private IEnumerator RemoveInvincibilityCoroutine(Component halo, float waitTime)
///             private void AbilityAutoAttack(GameObject target, int collisionId)
///             private void AbilityWall(float x, float z)
///             private void AbilityBanish(GameObject target)
///             private void AbilityPewPew(GameObject target, int collisionId)
///             private void AbilitySploosh(GameObject target, int collisionId)
///             IEnumerator SendCollisionElement(CollisionElement collisionElement, float delayTime)
///             private void AbilityBlink(float x, float z)
///             private void AbilityTowerAttack(GameObject target, int collisionId)
///             private void AbilityGungnir(float x, float z, int collisionId)
///             private void AbilitySlash(float x, float z, int collisionId)
///
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
    /// INTERFACE: 	void virtual void Start()
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
    /// INTERFACE: 	public virtual void UseAbility(AbilityType abilityId, float x, float z)
    ///                 AbilityType abilityId: The ability to be used
    ///                 float x: The x coordinate to use for the ability's location
    ///                 float z: The z coordinate to use for the ability's location
    ///                 int collisionId: The collision id to use for collisions
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
    /// INTERFACE: 	public virtual void UseTargetedAbility(AbilityType abilityId, int targetId)
    ///                 AbilityType abilityId: The ability to be used
    ///                 GameObject target: The target GameObject
    ///                 int collisionId: The collision id to use for collisions
    ///                 
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
    /// INTERFACE: 	private void BulletAbility(GameObject target, int collisionId)
    ///                 GameObject target: Target GameObject
    ///                 int collisionId: Collision id to use
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
    /// INTERFACE: 	private void PorkChop(GameObject target, int collisionId)
    ///                 GameObject target: Target GameObject
    ///                 int collisionId: Collision id to use
    ///
    /// RETURNS: 	void
    ///
    /// NOTES:		Melee Attack
    /// ----------------------------------------------
    private void PorkChop(GameObject target, int collisionId)
    {
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
    /// INTERFACE: 	private void Dart(float x, float z, int collisionId)
    ///                 float x: Target location x coordinate
    ///                 float z: Target location z coordinate
    ///                 int collisionId: Collision id to use
    ///
    /// RETURNS: 	void
    ///
    /// NOTES:		Ranged damage over Time Skill
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
    /// INTERFACE: 	private void Purification(GameObject target, int collisionId)
    ///                 GameObject target: Target GameObject
    ///                 int collisionId: Collision id to use
    ///
    /// RETURNS: 	void
    ///
    /// NOTES:		Healing Skill
    /// ----------------------------------------------
    private void Purification(GameObject target, int collisionId)
    {
        GetComponent<Animator>().SetTrigger("attack5");
        StartCoroutine(PurificationCoroutine(target, collisionId ,0.5f));
    }

    /// ----------------------------------------------
    /// FUNCTION:   PurificationCoroutine
    ///
    /// DATE:		April 7th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	Cameron Roberts
    ///
    /// INTERFACE: 	private IEnumerator PurificationCoroutine(GameObject target , int collisionId, float waitTime)
    ///                 GameObject target: Target
    ///                 int collisionId: Collision Id of the target
    ///                 float waitTime: The time to wait before starting the cooroutine
    ///
    /// RETURNS: 	void
    ///
    /// NOTES:		Used to delay the effects of the purification skill to match the animation
    /// ----------------------------------------------
    private IEnumerator PurificationCoroutine(GameObject target ,int collisionId, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        int targetId = target.GetComponent<Actor>().ActorId;
        int casterId = gameObject.GetComponent<Actor>().ActorId;
        StartCoroutine(RemoveObject(Instantiate(healEffect, target.transform.position, Quaternion.identity), 2f));
        StartCoroutine(SendCollisionElement(new CollisionElement(AbilityType.Purification, targetId, casterId, collisionId), 0f));
    }

    /// ----------------------------------------------
    /// FUNCTION:   RemoveObject
    ///
    /// DATE:		April 7th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	Cameron Roberts
    ///
    /// INTERFACE: 	private IEnumerator RemoveObject(GameObject target, float waitTime)
    ///                 GameObject target: The GameObject to remove
    ///                 float waitTime: The time to wait before starting the cooroutine
    ///
    /// RETURNS: 	void
    ///
    /// NOTES:		Used to remove a GameObject after a delay
    /// ----------------------------------------------
    private IEnumerator RemoveObject(GameObject target, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(target);
    }



    /// ----------------------------------------------
    /// FUNCTION:	AbilityTestProjectile
    ///
    /// DATE:		March 14th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	Cameron Roberts
    ///
    /// INTERFACE: 	private void AbilityTestProjectile(float x, float z, int collisionId)
    ///                 float x: Target location x coordinate
    ///                 float z: Target location z coordinate
    ///                 int collisionId: Collision id to use
    ///
    /// RETURNS: 	void
    ///
    /// NOTES:		Test projectile ability
    /// ----------------------------------------------
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
    
    /// ----------------------------------------------
    /// FUNCTION:	AbilityTestAreaOfEffect
    ///
    /// DATE:		March 14th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	Cameron Roberts
    ///
    /// INTERFACE: 	private void AbilityTestAreaOfEffect(float x, float z, int collisionId)
    ///                 float x: Target location x coordinate
    ///                 float z: Target location z coordinate
    ///                 int collisionId: Collision id to use
    ///
    /// RETURNS: 	void
    ///
    /// NOTES:		Test area of effect ability
    /// ----------------------------------------------
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

    /// ----------------------------------------------
    /// FUNCTION:	AbilityFireball
    ///
    /// DATE:		March 30th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Dasha Strigoun
    ///
    /// PROGRAMMER:	Dasha Strigoun
    ///
    /// INTERFACE: 	private void AbilityFireball(float x, float z, int collisionId)
    ///                 float x: Target location x coordinate
    ///                 float z: Target location z coordinate
    ///                 int collisionId: Collision id to use
    ///
    /// RETURNS: 	void
    ///
    /// NOTES:		Fireball ability 
    /// ----------------------------------------------
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
    /// ----------------------------------------------
    /// FUNCTION:	AbilityWeebOut
    ///
    /// DATE:		March 26th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Simon Wu
    ///
    /// PROGRAMMER:	Simon Wu
    ///
    /// INTERFACE: 	private void AbilityWeebOut(float x, float z, int collisionId)
    ///                 float x: Target location x coordinate
    ///                 float z: Target location z coordinate
    ///                 int collisionId: Collision id to use
    ///
    /// RETURNS: 	void
    ///
    /// NOTES:		Weeb out ability 
    /// ----------------------------------------------
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
    

    /// ----------------------------------------------
    /// FUNCTION:	AbilityWhale
    ///
    /// DATE:		March 26th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Simon Wu
    ///
    /// PROGRAMMER:	Simon Wu
    ///
    /// INTERFACE: 	private void AbilityWhale(float x, float z, int collisionId)
    ///                 float x: Target location x coordinate
    ///                 float z: Target location z coordinate
    ///                 int collisionId: Collision id to use
    ///
    /// RETURNS: 	void
    ///
    /// NOTES:		Whale ability. AOE Heal. 
    /// ----------------------------------------------
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

    /// ----------------------------------------------
    /// FUNCTION:   AbilityTestTargeted
    ///
    /// DATE:		March 14th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	Cameron Roberts
    ///
    /// INTERFACE: 	private void AbilityTestTargeted(GameObject target, int collisionId)
    ///                 GameObject target: Target GameObject
    ///                 int collisionId: Collision id to use
    ///
    /// RETURNS: 	void
    ///
    /// NOTES:		Test targeted ability
    /// ----------------------------------------------
    private void AbilityTestTargeted(GameObject target, int collisionId)
    {
        // TODO Play some sort of animation. No collsion is needed as the
        // abilities effect is instantly applied by the server
        GetComponent<Animator>().SetTrigger("attack3");
        int targetId = target.GetComponent<Actor>().ActorId;
        int casterId = gameObject.GetComponent<Actor>().ActorId;
        StartCoroutine(SendCollisionElement(new CollisionElement(AbilityType.TestTargeted, targetId, casterId, collisionId), 0.5f));
    }

    /// ----------------------------------------------
    /// FUNCTION:   AbilityTestTargetedHoming
    ///
    /// DATE:		March 14th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	Cameron Roberts
    ///
    /// INTERFACE: 	private void AbilityTestTargetedHoming(GameObject target, int collisionId)
    ///                 GameObject target: Target GameObject
    ///                 int collisionId: Collision id to use
    ///
    /// RETURNS: 	void
    ///
    /// NOTES:		Test targeted homing ability
    /// ----------------------------------------------
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

    /// ----------------------------------------------
    /// FUNCTION:   AbilityUwuImScared
    ///
    /// DATE:		March 30th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Dasha Strigoun
    ///
    /// PROGRAMMER:	Dasha Strigoun
    ///
    /// INTERFACE: 	private void AbilityUwuImScared(GameObject target)
    ///                 GameObject target: Target GameObject
    ///
    /// RETURNS: 	void
    ///
    /// NOTES:		UwuImScared ability
    /// ----------------------------------------------
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

    /// ----------------------------------------------
    /// FUNCTION:   RemoveInvincibilityCoroutine
    ///
    /// DATE:		April 7th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	Cameron Roberts
    ///
    /// INTERFACE: 	private IEnumerator RemoveInvincibilityCoroutine(Component halo, float waitTime)
    ///                 Component halo: Halo Component object
    ///                 float waitTime: The time to wait
    ///
    /// RETURNS: 	void
    ///
    /// NOTES:		Removes the invincibility visual granted by UwuImScared after the specified
    ///             amount of time.
    /// ----------------------------------------------
    private IEnumerator RemoveInvincibilityCoroutine(Component halo, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        halo.GetType().GetProperty("enabled").SetValue(halo, false, null);     
    }

    /// ----------------------------------------------
    /// FUNCTION:   AbilityAutoAttack
    ///
    /// DATE:		March 14th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	Cameron Roberts
    ///
    /// INTERFACE: 	private void AbilityAutoAttack(GameObject target, int collisionId)
    ///                 GameObject target: Target GameObject
    ///                 int collisionId: Collision id to use
    ///
    /// RETURNS: 	void
    ///
    /// NOTES:		Aability to use for auto attacks
    /// ----------------------------------------------
    private void AbilityAutoAttack(GameObject target, int collisionId)
    {
        GetComponent<Animator>().SetTrigger("attack1");
        int targetId = target.GetComponent<Actor>().ActorId;
        int casterId = gameObject.GetComponent<Actor>().ActorId;
        StartCoroutine(SendCollisionElement(new CollisionElement(AbilityType.AutoAttack, targetId, casterId, collisionId), 0.25f));
    }

    /// ----------------------------------------------
    /// FUNCTION:	AbilityWall
    ///
    /// DATE:		March 25th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Jason Kim
    ///
    /// PROGRAMMER:	Jason Kim
    ///
    /// INTERFACE: 	private void AbilityWall(float x, float z)
    ///                 float x: Target location x coordinate
    ///                 float z: Target location z coordinate
    ///
    /// RETURNS: 	void
    ///
    /// NOTES:		Create an impassable wall at the target location
    /// ----------------------------------------------
    private void AbilityWall(float x, float z)
    {
        // Instantiate Wall ability object
        var area = Instantiate(wallAbilityObject, new Vector3(x, 0.01f, z), gameObject.transform.rotation);

        // Set its creator id and ability type which will be used later for collisions
        area.GetComponent<Ability>().creator = gameObject; ;
        area.GetComponent<Ability>().abilityId = AbilityType.Wall;
        GetComponent<Animator>().SetTrigger("attack2");
    }

    /// ----------------------------------------------
    /// FUNCTION:	AbilityBanish
    ///
    /// DATE:		March 25th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Jason Kim
    ///
    /// PROGRAMMER:	Jason Kim
    ///
    /// INTERFACE: 	private void AbilityBanish(GameObject target)
    ///                 GameObject target: The target to be banished
    ///
    /// RETURNS: 	void
    ///
    /// NOTES:		Sets that the GameObject was banished and cancels their movement.
    /// ----------------------------------------------
    private void AbilityBanish(GameObject target)
    {
        GetComponent<Actor>().banished = true;
        GetComponent<PlayerAbilityController>().CancelMoveToTarget();

    }

    /// ----------------------------------------------
    /// FUNCTION:   AbilityPewPew
    ///
    /// DATE:		March 27th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Jenny Ly
    ///
    /// PROGRAMMER:	Jenny Ly
    ///
    /// INTERFACE: 	private void Purification(GameObject target, int collisionId)
    ///                 GameObject target: Target GameObject
    ///                 int collisionId: Collision id to use
    ///
    /// RETURNS: 	void
    ///
    /// NOTES:		Basic projectile skill
    /// ----------------------------------------------
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

    /// ----------------------------------------------
    /// FUNCTION:   AbilitySploosh
    ///
    /// DATE:		March 27th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Jenny Ly
    ///
    /// PROGRAMMER:	Jenny Ly
    ///
    /// INTERFACE: 	private void Purification(GameObject target, int collisionId)
    ///                 GameObject target: Target GameObject
    ///                 int collisionId: Collision id to use
    ///
    /// RETURNS: 	void
    ///
    /// NOTES:		Basic targeted melee skill
    /// ----------------------------------------------
    private void AbilitySploosh(GameObject target, int collisionId)
    {
        GetComponent<Animator>().SetTrigger("attack1");
        int targetId = target.GetComponent<Actor>().ActorId;
        int casterId = gameObject.GetComponent<Actor>().ActorId;
        StartCoroutine(SendCollisionElement(new CollisionElement(AbilityType.Sploosh, targetId, casterId, collisionId), 0.25f));
        Debug.Log("Play autoattack animation");
    }

    /// ----------------------------------------------
    /// FUNCTION:   AbilitySploosh
    ///
    /// DATE:		March 20th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	Cameron Roberts
    ///
    /// INTERFACE: 	IEnumerator SendCollisionElement(CollisionElement collisionElement, float delayTime)
    ///                 CollisionElement collisionElement: The collision element to send
    ///                 float delayTime: The time delay to put on sending the collision element
    ///
    /// RETURNS: 	void
    ///
    /// NOTES:		Coroutine used to dalay sending of collisions to the server for a specified amount of time
    /// ----------------------------------------------
    IEnumerator SendCollisionElement(CollisionElement collisionElement, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        ConnectionManager.Instance.QueueReliableElement(collisionElement);
    }


    /// ----------------------------------------------
    /// FUNCTION:	AbilityBlink
    ///
    /// DATE:		March 31st, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Jeff Choy
    ///
    /// PROGRAMMER:	Jeff Choy
    ///
    /// INTERFACE: 	private void AbilityBlink(float x, float z)
    ///                 float x: Target location x coordinate
    ///                 float z: Target location z coordinate
    ///
    /// RETURNS: 	void
    ///
    /// NOTES:		Move the user to the target location instantly
    /// ----------------------------------------------
    private void AbilityBlink(float x, float z)
    {
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<PlayerMovement>().ForceTargetPosition(new Vector3(x, 0, z));
        GetComponent<PlayerAbilityController>().CancelMoveToTarget();
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
    }

    /// ----------------------------------------------
    /// FUNCTION:	AbilityTowerAttack
    ///
    /// DATE:		April 5th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	Cameron Roberts
    ///
    /// INTERFACE: 	private void AbilityTowerAttack(GameObject target, int collisionId)
    ///                 GameObject target: Target GameObject
    ///                 int collisionId: Collision id to use
    ///
    /// RETURNS: 	void
    ///
    /// NOTES:		Tower attack
    /// ----------------------------------------------
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

    /// ----------------------------------------------
    /// FUNCTION:	AbilitySlash
    ///
    /// DATE:		April 7th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	Cameron Roberts
    ///
    /// INTERFACE: 	private void AbilityGungnir(float x, float z, int collisionId)
    ///                 float x: Target location x coordinate
    ///                 float z: Target location z coordinate
    ///                 int collisionId: Collision id to use
    ///
    /// RETURNS: 	void
    ///
    /// NOTES:		Gungnir ability
    /// ----------------------------------------------
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

    /// ----------------------------------------------
    /// FUNCTION:	AbilitySlash
    ///
    /// DATE:		April 7th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	Cameron Roberts
    ///
    /// INTERFACE: 	private void AbilitySlash(float x, float z, int collisionId)
    ///                 float x: Target location x coordinate
    ///                 float z: Target location z coordinate
    ///                 int collisionId: Collision id to use
    ///
    /// RETURNS: 	void
    ///
    /// NOTES:		Gungnir ability
    /// ----------------------------------------------
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
