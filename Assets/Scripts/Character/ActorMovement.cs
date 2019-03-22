using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// ----------------------------------------------
/// Class: 	ActorMovement - A script to provide the logic move actors not controlled
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
public class ActorMovement : MonoBehaviour
{
    protected NavMeshAgent agent;
    protected Vector3 targetPosition;
    protected Animator animator;
    protected bool moving;



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
    protected void Start()
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
    protected void Update()
    {
        if(moving)
        {
            Move();
            animator.SetBool("moving", true);
        } else
        {
            animator.SetBool("moving", false);
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
        moving = true;
        agent.SetDestination(target);

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
    protected void Move()
    {
       if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    moving = false;
                }
            }
        }
    }


    public void Stop(){
        moving = false;
        agent.SetDestination(transform.position);
    }
}