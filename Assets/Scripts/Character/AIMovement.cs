using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// ----------------------------------------------
/// Class: 	AIMovement - A script to provide the logic move actors not controlled
///                      by the player
/// 
/// PROGRAM: SKOM
///
/// FUNCTIONS:	void Start()
///				void Awake()
///             public void SetTargetPosition(Vector3 target)
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
public class AIMovement : MonoBehaviour
{
    NavMeshAgent agent;
    public Vector3 targetPosition;
    public Animator animator;
    public bool Moving;


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
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.updateRotation = true;

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
    ///             on if the object is currently moving.
    /// ----------------------------------------------
    void Update()
    {
        if(Moving)
        {
            Move();
            animator.SetFloat("inputV", 1);
        } else
        {
            animator.SetFloat("inputV", 0);
        }
    }

    /// ----------------------------------------------
    /// FUNCTION:	SetTargetPosition
    /// 
    /// DATE:		March 14th, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Phat Le, Cameron Roberts
    /// 
    /// PROGRAMMER:	Phat Le, Cameron Roberts
    /// 
    /// INTERFACE: 	public void SetTargetPosition(Vector3 target)
    ///                 Vector3 target: The location to set as the destination
    /// 
    /// RETURNS: 	void
    /// 
    /// NOTES:		Sets the GameObject's NavMeshAgent's destination
    ///             to the given vector.
    /// ----------------------------------------------
    public void SetTargetPosition(Vector3 target)
    {
        if(target.x == transform.position.x && target.z == transform.position.z){
            return;
        }
        targetPosition = target;
        Moving = true;
        agent.SetDestination(targetPosition);

    }

    /// ----------------------------------------------
    /// FUNCTION:	Move
    /// 
    /// DATE:		March 14th, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Phat Le, Cameron Roberts
    /// 
    /// PROGRAMMER:	Phat Le, Cameron Roberts
    /// 
    /// INTERFACE: 	void Move
    /// 
    /// RETURNS: 	void
    /// 
    /// NOTES:		Checks if the GameObject has reached its
    ///             destination and sets Moving to false if it has.
    /// ----------------------------------------------
    void Move()
    {
       if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    Moving = false;
                }
            }
        }
    }
}