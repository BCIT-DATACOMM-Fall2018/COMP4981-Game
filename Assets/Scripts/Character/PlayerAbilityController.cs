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
        int actorId = gameObject.GetComponent<Actor>().ActorId;
        AbilityInfo abilityInfo = AbilityInfo.InfoArray[(int)abilityId];
        if(abilityInfo.IsArea){
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (GameObject.Find("Terrain").GetComponent<Collider>().Raycast (ray, out hit, Mathf.Infinity)) {
                ConnectionManager.Instance.QueueReliableElement(new AreaAbilityElement(actorId, abilityId, hit.point.x, hit.point.z));
                return true;
            }
        } else if (abilityInfo.IsTargeted){
            RaycastHit hit;
            int layerMask = 1 << 10;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask: layerMask))
            {
                GameObject hitTarget = hit.transform.gameObject;
                if(hitTarget.tag == gameObject.tag && !abilityInfo.AllyTargetAllowed){
                    return false;
                }
                if(hitTarget.tag != gameObject.tag && !abilityInfo.EnemyTargetAllowed){
                    return false;
                }
                int hitActorId = hitTarget.GetComponent<Actor>().ActorId;
                ConnectionManager.Instance.QueueReliableElement(new TargetedAbilityElement(actorId, abilityId, hitActorId));
                return true;
            }
        } else if (abilityInfo.IsSelf){
            ConnectionManager.Instance.QueueReliableElement(new TargetedAbilityElement(actorId, abilityId, actorId));
            return true;
        }
        return false;
    }
}