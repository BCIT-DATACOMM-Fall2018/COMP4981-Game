
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
    private struct AbilityInfo{
        public bool IsArea;
        public bool IsTargeted;
        public bool IsSelf;
        public bool AllyTargetAllowed;
        public bool EnemyTargetAllowed;

        public AbilityInfo(bool isArea = false, bool isTargeted = false, bool isSelf = false, bool allyTargetAllowed = false, bool enemyTargetAllowed = false){
            IsArea = isArea;
            IsTargeted = isTargeted;
            IsSelf = isSelf;
            AllyTargetAllowed = allyTargetAllowed;
            EnemyTargetAllowed = enemyTargetAllowed;
        }
    }

    private AbilityInfo[] abilityInfoArray = {
        // TestProjectile
        new AbilityInfo(isArea: true),
        // TestTargeted
        new AbilityInfo(isTargeted: true, allyTargetAllowed: true, enemyTargetAllowed: true),
        // TestHomingTargeted
        new AbilityInfo(isTargeted: true, enemyTargetAllowed:true),
        // TestAreaOfEffect
        new AbilityInfo(isArea: true)
    };

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
        if(Input.GetButtonDown("Ability1")) {
            InitiateAbilityUse(AbilityType.TestProjectile);
        } else if (Input.GetButtonDown("Ability2")){
            InitiateAbilityUse(AbilityType.TestTargeted);
        } else if (Input.GetButtonDown("Ability3")){
            InitiateAbilityUse(AbilityType.TestTargetedHoming);
        } else if (Input.GetButtonDown("Ability4")){
            InitiateAbilityUse(AbilityType.TestAreaOfEffect);
        }
    }

    void InitiateAbilityUse(AbilityType abilityId){
        int actorId = gameObject.GetComponent<Actor>().ActorId;
        AbilityInfo abilityInfo = abilityInfoArray[(int)abilityId];
        if(abilityInfo.IsArea){
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (GameObject.Find("Terrain").GetComponent<Collider>().Raycast (ray, out hit, Mathf.Infinity)) {
                ConnectionManager.Instance.QueueReliableElement(new AreaAbilityElement(actorId, abilityId, hit.point.x, hit.point.z));
            }
        } else if (abilityInfo.IsTargeted){
            RaycastHit hit;
            int layerMask = 1 << 10;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask: layerMask))
            {
                GameObject hitTarget = hit.transform.gameObject;
                int hitActorId = hitTarget.GetComponent<Actor>().ActorId;
                //TODO Check if the target is an ally or an enemy and if the ability is allowed to target them
                ConnectionManager.Instance.QueueReliableElement(new TargetedAbilityElement(actorId, abilityId, hitActorId));

            }
        } else if (abilityInfo.IsSelf){
            ConnectionManager.Instance.QueueReliableElement(new TargetedAbilityElement(actorId, abilityId, actorId));
        }
    }
}