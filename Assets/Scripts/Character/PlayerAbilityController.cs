using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetworkLibrary;
using NetworkLibrary.MessageElements;

/// ----------------------------------------------
/// Class: 	PlayerAbilityController - A script to provide the logic for player
///                                   initiated abilities.
///
/// PROGRAM: SKOM
///
/// FUNCTIONS:	void Start()
///				void Update()
///
/// DATE: 		March 14th, 2019
///
/// REVISIONS:  April 6th, 2019 -- Ian Lo
///             - Updated to allow for external assignment of abilities
///
/// DESIGNER: 	Simon Wu, Cameron Roberts
///
/// PROGRAMMER: Simon Wu, Cameron Roberts, Ian Lo
///
/// NOTES:
/// ----------------------------------------------
public class PlayerAbilityController : AbilityController
{
    private const float SERVER_TICK_RATE_PER_SECOND = 33f / 1000f;
    private AbilityType[] abilities;
    private string[] buttonNames;
    public float[] Cooldowns {get;set;}
    public float[] MaxCooldowns {get;set;}
	private const short MAX_NUMBER_OF_ABILILITES = 4;
	private short abilityCount = 0;

    private bool moveToAreaTarget;
    private bool moveToActorTarget;
    private bool autoAttacking;
    private AbilityType storedAbility;
    Vector3 storedTargetLocation;
    GameObject storedTargetActor;
    GameObject autoAttackTarget;
    float autoCooldown;
    float maxAutoCooldown = 30 * SERVER_TICK_RATE_PER_SECOND;

    private float followRecalculateTimer;

    /// ----------------------------------------------
    /// FUNCTION:	Start
    ///
    /// DATE:		March 14th, 2019
    ///
	/// REVISIONS:  April 6th, 2019 -- Ian Lo
	///             -Cooldowns have been Externalized.
	///             -Skills are now dynamically set.
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
    protected override void Start()
    {
        base.Start();
		if (abilities == null)
		{
			abilities = new AbilityType[MAX_NUMBER_OF_ABILILITES];
		} 
        buttonNames = new String[] {"Ability1", "Ability2", "Ability3", "Ability4"};
		makeCooldown ();
    }

    /// ----------------------------------------------
    /// FUNCTION:	Update
    ///
    /// DATE:		March 14th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts, Simon Wu
    ///
    /// PROGRAMMER:	Cameron Roberts, Simon Wu
    ///
    /// INTERFACE: 	void Update()
    ///
    /// RETURNS: 	void
    ///
    /// NOTES:		MonoBehaviour function. Called every frame.
    ///             Checks if a key is pressed and queues
    ///             a reliable Update element based on the
    ///             ability used.
    /// ----------------------------------------------
    void Update()
    {
        // Reduce cooldowns
        autoCooldown -= Time.deltaTime;
        for (int i = 0; i < Cooldowns.Length; i++)
        {
            Cooldowns[i] -= Time.deltaTime;
            if(Cooldowns[i] < 0){
                Cooldowns[i] = 0;
            }

        }

        // Check for keypresses and use the associated ability
		for (int i = 0; i < abilityCount; i++)
        {
            if(Input.GetButtonDown(buttonNames[i])) {
                if(Cooldowns[i] != 0){
                    return;
                }
                if(InitiateAbilityUse(abilities[i])){
                }
            }
        }
        // Logic to move to an area and use an ability
        if(moveToAreaTarget){
            if(InitiateAreaAbilityUse(storedAbility, storedTargetLocation)){
                GetComponent<PlayerMovement>().Stop();
                CancelMoveToTarget();
            }
        }
        // Logic to move to target and use an ability
        if(moveToActorTarget){
            followRecalculateTimer += Time.deltaTime;
            if(followRecalculateTimer > 1){
                followRecalculateTimer -= 0.25f;
                GetComponent<PlayerMovement>().SetTargetPosition(storedTargetActor.transform.position);
            }
            if(InitiateTargetedAbilityUse(storedAbility, storedTargetActor)){
                GetComponent<PlayerMovement>().Stop();
                CancelMoveToTarget();
                followRecalculateTimer = 0;
            }
        }
        // Cancel autoattack if the target is dead
        if(autoAttacking && autoAttackTarget.transform.position.x == -10){
            autoAttacking = false;
        }
        // Logic to auto attack
        if(autoAttacking){
            followRecalculateTimer += Time.deltaTime;
            if(followRecalculateTimer > 1){
                followRecalculateTimer -= 0.25f;
                if(Vector3.Distance(transform.position, autoAttackTarget.transform.position)>15){
                    GetComponent<PlayerMovement>().SetTargetPosition(autoAttackTarget.transform.position);
                }
            }
            if(!(Vector3.Distance(transform.position, autoAttackTarget.transform.position)>15)){
                    GetComponent<PlayerMovement>().Stop();
                    if(autoCooldown <= 0) {
                        InitiateTargetedAbilityUse(AbilityType.AutoAttack, autoAttackTarget);
                }
            }
        }
    }

    /// ----------------------------------------------
    /// FUNCTION:	CancelMoveToTarget
    ///
    /// DATE:		March 20th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	Cameron Roberts
    ///
    /// INTERFACE: 	public void CancelMoveToTarget()
    ///
    /// RETURNS: 	void
    ///
    /// NOTES:		Cancels the players move to target
    /// ----------------------------------------------
    public void CancelMoveToTarget(){
        moveToAreaTarget = false;
        moveToActorTarget = false;
        autoAttacking = false;
    }

    /// ----------------------------------------------
    /// FUNCTION:	UseAreaAbility
    ///
    /// DATE:		March 14th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	Cameron Roberts
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
    ///             the function. Puts the used ability on cooldown
    /// ----------------------------------------------
    public override void UseAreaAbility(AbilityType abilityId, float x, float z, int collisionId)
    {
        base.UseAreaAbility(abilityId, x, z, collisionId);
        PutAbilityOnCooldown(abilityId);
    }

    /// ----------------------------------------------
    /// FUNCTION:	UserTargetedAbility
    ///
    /// DATE:		March 15th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	Cameron Roberts
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
    ///             the function. Puts the used ability on cooldown.
    /// ----------------------------------------------
    public override void UseTargetedAbility(AbilityType abilityId, GameObject target, int collisionId){
        base.UseTargetedAbility(abilityId, target, collisionId);
        PutAbilityOnCooldown(abilityId);
    }


    /// ----------------------------------------------
    /// FUNCTION:	PutAbilityOnCooldown
    ///
    /// DATE:		March 16th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	Cameron Roberts
    ///
    /// INTERFACE: 	bool PutAbilityOnCooldown(AbilityType abilityId)
    ///                 AbilityType abilityId: The ability to be put on cooldown
    ///                 
    ///
    /// RETURNS: 	void
    ///
    /// NOTES:		Puts the passed ability on cooldown
    /// ----------------------------------------------
    bool PutAbilityOnCooldown(AbilityType abilityId){
        if(abilityId == AbilityType.AutoAttack){
            autoCooldown = maxAutoCooldown;
            return true;
        }
        int index = Array.IndexOf(abilities, abilityId);
        if(index != -1){
            Cooldowns[index] = MaxCooldowns[index];
            return true;
        }
        return false;
    }

    /// ----------------------------------------------
    /// FUNCTION:	InitiateAbilityUse
    ///
    /// DATE:		March 16th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	Cameron Roberts
    ///
    /// INTERFACE: 	bool InitiateAbilityUse(AbilityType abilityId)
    ///                 AbilityType abilityId: The ability to be used
    ///                 
    ///
    /// RETURNS: 	void
    ///
    /// NOTES:		Contains logic to choose a target for the used ability and send
    ///             the ability use to the server
    /// ----------------------------------------------
    bool InitiateAbilityUse(AbilityType abilityId){
        AbilityInfo abilityInfo = AbilityInfo.InfoArray[(int)abilityId];
        if(abilityInfo.IsArea){
            if(abilityInfo.Range == 0){
                return InitiateAreaAbilityUse(abilityId, gameObject.transform.position);
            }
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (GameObject.Find("Terrain").GetComponent<Collider>().Raycast (ray, out hit, Mathf.Infinity)) {
                return InitiateAreaAbilityUse(abilityId, hit.point);
            }
        } else if (abilityInfo.IsTargeted){
            RaycastHit hit;
            int layerMask = 1 << 10;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask: layerMask))
            {
                GameObject hitTarget = hit.transform.gameObject;
                return InitiateTargetedAbilityUse(abilityId, hitTarget);
            }
        } else if (abilityInfo.IsSelf){
            return InitiateSelfAbilityUse(abilityId);
        }
        return false;
    }

    /// ----------------------------------------------
    /// FUNCTION:	InitiateAreaAbilityUse
    ///
    /// DATE:		March 16th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	Cameron Roberts
    ///
    /// INTERFACE: 	bool InitiateAreaAbilityUse(AbilityType abilityId, Vector3 target)
    ///                 AbilityType abilityId: The ability to be used
    ///                 Vector3 target: The location to use the ability
    ///                 
    ///
    /// RETURNS: 	void
    ///
    /// NOTES:		Contains logic validate an ability use. If the use is valid
    ///             it will be sent to the server. Otherwise the player will move to
    ///             a location they can use the ability from.
    /// ----------------------------------------------
    bool InitiateAreaAbilityUse(AbilityType abilityId, Vector3 target){
        int actorId = gameObject.GetComponent<Actor>().ActorId;

        AbilityInfo abilityInfo = AbilityInfo.InfoArray[(int)abilityId];
        
        if(Vector3.Distance(transform.position, target)>abilityInfo.Range && abilityInfo.Range != 0){
            if(!moveToAreaTarget){
                moveToAreaTarget = true;
                storedTargetLocation = target;
                storedAbility = abilityId;
                GetComponent<PlayerMovement>().SetTargetPosition(target);
            }
            return false;
        }

        ConnectionManager.Instance.QueueReliableElement(new AreaAbilityElement(actorId, abilityId, target.x, target.z));
        return true;
    }

    /// ----------------------------------------------
    /// FUNCTION:	InitiateTargetedAbilityUse
    ///
    /// DATE:		March 16th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	Cameron Roberts
    ///
    /// INTERFACE: 	bool InitiateTargetedAbilityUse(AbilityType abilityId, GameObject target)
    ///                 AbilityType abilityId: The ability to be used
    ///                 GameObject target: The target to use the ability on
    ///                 
    ///
    /// RETURNS: 	void
    ///
    /// NOTES:		Contains logic validate an ability use. If the use is valid
    ///             it will be sent to the server. Otherwise the player will move to
    ///             a location they can use the ability from.
    /// ----------------------------------------------
    bool InitiateTargetedAbilityUse(AbilityType abilityId, GameObject target){
        int actorId = gameObject.GetComponent<Actor>().ActorId;

        AbilityInfo abilityInfo = AbilityInfo.InfoArray[(int)abilityId];
        if(target.tag == gameObject.tag && !abilityInfo.AllyTargetAllowed){
            return false;
        }
        if(target.tag != gameObject.tag && !abilityInfo.EnemyTargetAllowed){
            return false;
        }

        if(Vector3.Distance(transform.position, target.transform.position)>abilityInfo.Range && abilityInfo.Range != 0){
            if(!moveToActorTarget){
                moveToActorTarget = true;
                storedTargetActor = target;
                storedAbility = abilityId;
                GetComponent<PlayerMovement>().SetTargetPosition(target.transform.position);
            }
            return false;

        }
        int hitActorId = target.GetComponent<Actor>().ActorId;
        ConnectionManager.Instance.QueueReliableElement(new TargetedAbilityElement(actorId, abilityId, hitActorId));
        return true;
    }

    /// ----------------------------------------------
    /// FUNCTION:	InitiateSelfAbilityUse
    ///
    /// DATE:		March 16th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	Cameron Roberts
    ///
    /// INTERFACE: 	bool InitiateSelfAbilityUse(AbilityType abilityId)
    ///                 AbilityType abilityId: The ability to be used
    ///                 
    ///
    /// RETURNS: 	void
    ///
    /// NOTES:		Sends a self targeted ability use to the server
    /// ----------------------------------------------
    bool InitiateSelfAbilityUse(AbilityType abilityId){
        int actorId = gameObject.GetComponent<Actor>().ActorId;

        ConnectionManager.Instance.QueueReliableElement(new TargetedAbilityElement(actorId, abilityId, actorId));
        return true;
    }

    /// ----------------------------------------------
    /// FUNCTION:	AutoAttack
    ///
    /// DATE:		March 21st, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	Cameron Roberts
    ///
    /// INTERFACE: 	public void AutoAttack(GameObject target)
    ///                 GameObject target: The actor to target for autoattacking
    ///                 
    ///
    /// RETURNS: 	void
    ///
    /// NOTES:		Starts the actor autoattacking the given target
    /// ----------------------------------------------
    public void AutoAttack(GameObject target){
        autoAttacking = true;
        autoAttackTarget = target;
        GetComponent<PlayerMovement>().SetTargetPosition(autoAttackTarget.transform.position);
    }


	/// ----------------------------------------------
	/// FUNCTION:	addAbility
	///
	/// DATE:		April 6th, 2019
	///
	/// REVISIONS:
	///
	/// DESIGNER:	Cameron Roberts, Ian Lo
	///
	/// PROGRAMMER:	Ian Lo
	///
	/// INTERFACE: 	addAbility(int Ability)
	///
	/// RETURNS: 	void
	///
	/// NOTES:		Interface to add an ability externally.
	///             Fails to add the ability if MAX_NUMBER_OF_ABILITIES
	///             is exceeded.
	/// ----------------------------------------------
	public void addAbility(int ability)
	{
		if (abilityCount < MAX_NUMBER_OF_ABILILITES)
		{
			Debug.Log ("assigning ability" + ability);
			if (abilities == null)
			{
				abilities = new AbilityType[MAX_NUMBER_OF_ABILILITES];
			}
			abilities [abilityCount] = (AbilityType)ability;

			//MaxCooldowns [abilityCount] = (float)AbilityInfo.InfoArray [(int)abilities [abilityCount]].Cooldown * SERVER_TICK_RATE_PER_SECOND;
			makeCooldown();


			GetComponent<AbilityUI>().setAbilityIcon(abilityCount, (AbilityType)ability);

			++abilityCount;
		}
	}

	/// ----------------------------------------------
	/// FUNCTION:	makeCooldown
	///
	/// DATE:		April 6th, 2019
	///
	/// REVISIONS:
	///
	/// DESIGNER:	Ian Lo
	///
	/// PROGRAMMER:	Ian Lo
	///
	/// INTERFACE: 	void makeCooldown()
	///
	/// RETURNS: 	void
	///
	/// NOTES:		Wrapper function to add cooldowns to
	///             existing skills. 
	/// ----------------------------------------------
	private void makeCooldown()
	{
		if (Cooldowns == null)
			Cooldowns = new float[MAX_NUMBER_OF_ABILILITES];
		if (MaxCooldowns == null)
			MaxCooldowns = new float[MAX_NUMBER_OF_ABILILITES];
		for (int i = 0; i < abilities.Length; i++) {
			MaxCooldowns [i] = (float)AbilityInfo.InfoArray [(int)abilities [i]].Cooldown * SERVER_TICK_RATE_PER_SECOND;
		}
	}
}
