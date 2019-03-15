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
public class CharacterMovement : MonoBehaviour
{
    NavMeshAgent agent;
    public Vector3 TargetPosition;
    bool moving = false;
    public Animator animator;
    public GameObject terrain;
    
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
        agent.updateRotation = true;
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
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Right Click");
            SetTargetPosition();
        }
        if(moving)
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
    /// INTERFACE: 	public void SetTargetPosition()
    /// 
    /// RETURNS: 	void
    /// 
    /// NOTES:		Sets the GameObject's NavMeshAgent's destination
    ///             based on the current mouse location
    /// ----------------------------------------------
    void SetTargetPosition()
    {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (terrain.GetComponent<Collider>().Raycast (ray, out hit, Mathf.Infinity)) {
            TargetPosition = hit.point;
            moving = true;
            agent.SetDestination(TargetPosition);
        }
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
                    moving = false;
                }
            }
        }
    }
}