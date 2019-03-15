using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    NavMeshAgent agent;

    public Vector3 targetPosition;
    private Vector3 lookAtTarget;
    public Quaternion rot;

    public Animator animator;
    public float rotSpeed;

    public bool Moving;

    RaycastHit hit;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.updateRotation = true;

    }

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

    public void SetTargetPosition(Vector3 target)
    {
        if(target.x == transform.position.x && target.z == transform.position.z){
            return;
        }
        targetPosition = target;
        Moving = true;
        agent.SetDestination(targetPosition);

    }

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