using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// ----------------------------------------------
/// Class: 	PlayerMovement - A script to provide the logic move actors not controlled
///                      by the player
/// 
/// PROGRAM: SKOM
///
/// FUNCTIONS:	void Start()
///				void Awake()
///             public void SetTargetPosition()
///             void Move()
///
/// DATE: 		March 14th, 2019
///
/// REVISIONS: 
///
/// DESIGNER: 	Phat Le, Cameron Roberts
///
/// PROGRAMMER: Phat Le, Cameron Roberts
///
/// NOTES:		
/// ----------------------------------------------
public class PlayerMovement : ActorMovement
{
    private GameObject terrain;

    private bool attackMove;
    
    /// ----------------------------------------------
    /// FUNCTION:	Start
    /// 
    /// DATE:		March 14th, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Phat Le, Cameron Roberts
    /// 
    /// PROGRAMMER:	Phat Le, Cameron Roberts
    /// 
    /// INTERFACE: 	void Start()
    /// 
    /// RETURNS: 	void
    /// 
    /// NOTES:		MonoBehaviour function.
    ///             Called before the first Update().
    /// ----------------------------------------------
    void Start()
    {
        base.Start();
        terrain = GameObject.Find("Terrain");
    }

    /// ----------------------------------------------
    /// FUNCTION:	Update
    /// 
    /// DATE:		March 14th, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Phat Le, Cameron Roberts
    /// 
    /// PROGRAMMER:	Phat Le, Cameron Roberts
    /// 
    /// INTERFACE: 	void Update()
    /// 
    /// RETURNS: 	void
    /// 
    /// NOTES:		MonoBehaviour function. Called every frame.
    ///             Starts or stops the GameObjects animation based
    ///             on if the object is currently moving. Checks if
    ///             the player right clicks and calls SetTargetPosition.
    /// ----------------------------------------------
    void Update()
    {
        if (Input.GetButtonDown("AttackMove")){
            attackMove = true;
            Debug.Log("prepare attack move");
        }
        if (Input.GetMouseButtonDown(0)){
            if(attackMove){
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (terrain.GetComponent<Collider>().Raycast (ray, out hit, Mathf.Infinity)) {
                    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                    float shortestDistance = 0;
                    GameObject closestEnemy = null;
                    int i = 0;
                    for (; i < enemies.Length; i++)
                    {
                        if(enemies[i].transform.position.x != -10){
                            closestEnemy = enemies[i];
                            shortestDistance = (enemies[i].transform.position - hit.point).sqrMagnitude;
                            break;
                        }
                    }
                    for (; i < enemies.Length; i++)
                    {
                        float distance = (enemies[i].transform.position - hit.point).sqrMagnitude;
                        if(distance < shortestDistance){
                            distance = shortestDistance;
                            closestEnemy = enemies[i];
                        }
                    }
                    if(closestEnemy != null) {
                        GetComponent<PlayerAbilityController>().CancelMoveToTarget();
                        GetComponent<PlayerAbilityController>().AutoAttack(closestEnemy);
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            attackMove = false;
            RaycastHit hit;
            int layerMask = 1 << 10;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask: layerMask))
            {
                GameObject hitTarget = hit.transform.gameObject;
                if(hitTarget.tag != gameObject.tag){
                    Debug.Log("time to auto");
                    GetComponent<PlayerAbilityController>().CancelMoveToTarget();
                    GetComponent<PlayerAbilityController>().AutoAttack(hitTarget);
                }
            } else{ 
                if (terrain.GetComponent<Collider>().Raycast (ray, out hit, Mathf.Infinity)) {
                    SetTargetPosition(hit.point);
                    GetComponent<PlayerAbilityController>().CancelMoveToTarget();
                }
            }
        }
        if(Input.GetButtonDown("Stop")) {
            Stop();
            GetComponent<PlayerAbilityController>().CancelMoveToTarget();
        }

        base.Update();
    }

}