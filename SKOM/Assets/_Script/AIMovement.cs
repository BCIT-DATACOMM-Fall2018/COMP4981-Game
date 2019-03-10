using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    NavMeshAgent agent;

    public Vector3 targetPosition;
    private Vector3 lookAtTarget;
    Quaternion rot;

    public Animator animator;
    public float rotSpeed;

    public bool Moving;

    RaycastHit hit;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(Moving)
        {
            //Move();
            animator.SetFloat("Speed", 0.4F);
        } else
        {
            animator.SetFloat("Speed", 0);
        }
    }

    public void SetTargetPosition(Vector3 target)
    {
        targetPosition = target;
        lookAtTarget = new Vector3(targetPosition.x - transform.position.x, transform.position.y, targetPosition.z - transform.position.z);
        rot = Quaternion.LookRotation(lookAtTarget);
        Moving = true;
    }

    void Move()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotSpeed * Time.deltaTime);

        agent.SetDestination(targetPosition);

        if (transform.position.x == targetPosition.x)
        {
            
            Moving = false;
        }
    }
}