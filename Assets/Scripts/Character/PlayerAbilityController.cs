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
/// REVISIONS: 
///
/// DESIGNER: 	Simon Wu, Cameron Roberts
///
/// PROGRAMMER: Simon Wu, Cameron Roberts
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

    private bool moveToAreaTarget;
    private bool moveToActorTarget;
    private AbilityType storedAbility;
    Vector3 storedTargetLocation;
    GameObject storedTargetActor;
    private float recalculateTimer;

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
    protected override void Start()
    {
        base.Start();

        abilities = new AbilityType[] {AbilityType.TestProjectile, AbilityType.TestTargeted, AbilityType.TestTargetedHoming, AbilityType.TestAreaOfEffect};
        buttonNames = new String[] {"Ability1", "Ability2", "Ability3", "Ability4"};
        Cooldowns = new float[4];
        MaxCooldowns = new float[4];

        for (int i = 0; i < abilities.Length; i++)
        {
            MaxCooldowns[i] = (float)AbilityInfo.InfoArray[(int)abilities[i]].Cooldown * SERVER_TICK_RATE_PER_SECOND;
        }
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
        for (int i = 0; i < Cooldowns.Length; i++)
        {
            Cooldowns[i] -= Time.deltaTime;
            if(Cooldowns[i] < 0){
                Cooldowns[i] = 0;
            }

        }
        for (int i = 0; i < buttonNames.Length; i++)
        {
            if(Input.GetButtonDown(buttonNames[i])) {
                if(Cooldowns[i] != 0){
                    Debug.Log("Ability on cooldown");
                    return;
                }
                if(InitiateAbilityUse(abilities[i])){
                    Debug.Log("Used ability");
                } else {
                    Debug.Log("Invalid ability use");
                }
            }
        }
        if(moveToAreaTarget){
            Debug.Log("yay dood");
            if(InitiateAreaAbilityUse(storedAbility, storedTargetLocation)){
                GetComponent<PlayerMovement>().Stop();
                CancelMoveToTarget();
            }
        }
        if(moveToActorTarget){
            recalculateTimer += Time.deltaTime;
            if(recalculateTimer > 1){
                recalculateTimer -= 0.25f;
                GetComponent<PlayerMovement>().SetTargetPosition(storedTargetActor.transform.position);
            }
            if(InitiateTargetedAbilityUse(storedAbility, storedTargetActor)){
                GetComponent<PlayerMovement>().Stop();
                CancelMoveToTarget();
                recalculateTimer = 0;
            }
        }
    }

    public void CancelMoveToTarget(){
        moveToAreaTarget = false;
        moveToActorTarget = false;
    }
    

    public override void UseAreaAbility(AbilityType abilityId, float x, float z, int collisionId)
    {
        base.UseAreaAbility(abilityId, x, z, collisionId);
        PutAbilityOnCooldown(abilityId);
    }

    public override void UseTargetedAbility(AbilityType abilityId, GameObject target, int collisionId){
        base.UseTargetedAbility(abilityId, target, collisionId);
        PutAbilityOnCooldown(abilityId);
    }



    bool PutAbilityOnCooldown(AbilityType abilityId){
        int index = Array.IndexOf(abilities, abilityId);
        if(index != -1){
            Cooldowns[index] = MaxCooldowns[index];
            return true;
        }
        return false;
    }

    bool InitiateAbilityUse(AbilityType abilityId){
        AbilityInfo abilityInfo = AbilityInfo.InfoArray[(int)abilityId];
        if(abilityInfo.IsArea){
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

    bool InitiateAreaAbilityUse(AbilityType abilityId, Vector3 target){
        int actorId = gameObject.GetComponent<Actor>().ActorId;

        AbilityInfo abilityInfo = AbilityInfo.InfoArray[(int)abilityId];
        
        if(Vector3.Distance(transform.position, target)>abilityInfo.Range && abilityInfo.Range != 0){
            Debug.Log("Distance " + Vector3.Distance(transform.position, target) + " > Range " + abilityInfo.Range);
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

    bool InitiateSelfAbilityUse(AbilityType abilityId){
        int actorId = gameObject.GetComponent<Actor>().ActorId;

        ConnectionManager.Instance.QueueReliableElement(new TargetedAbilityElement(actorId, abilityId, actorId));
        return true;
    }
}