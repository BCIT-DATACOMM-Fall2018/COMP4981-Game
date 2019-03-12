using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    NavMeshAgent agent;

    public Vector3 TargetPosition {get; private set;}
    private Vector3 lookAtTarget;
    Quaternion playerRot;
    bool moving = false;

    public Animator animator;
    public float rotSpeed;

    RaycastHit hit;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
 
    }

    void Update()
    {
        Debug.Log("Player current pos: " + transform.position.x + " " + transform.position.z);

        if (Input.GetMouseButtonDown(1))
        {
            SetTargetPosition();
        }
        if(moving)
        {
            Move();
            animator.SetFloat("Speed", 1);
        } else
        {
            animator.SetFloat("Speed", 0);
        }

    }

    void SetTargetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000))
        {
            TargetPosition = hit.point;
            lookAtTarget = new Vector3(TargetPosition.x - transform.position.x, transform.position.y, TargetPosition.z - transform.position.z);
            playerRot = Quaternion.LookRotation(lookAtTarget);
            moving = true;
        }
    }

    void Move()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, playerRot, rotSpeed * Time.deltaTime);

        agent.SetDestination(TargetPosition);

        if (transform.position.x == TargetPosition.x)
        {
            
            moving = false;
        }
    }
}