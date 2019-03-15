using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    NavMeshAgent agent;

    public Vector3 TargetPosition;
    private Vector3 nextPosition;
    Quaternion playerRot;
    bool moving = false;

    public Animator animator;
    public float rotSpeed;
    public GameObject terrain;
    
    void Start()
    {

        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = true;

        terrain = GameObject.Find("Terrain");
 
    }

    void Update()
    {
        //Debug.Log("Player current pos: " + transform.position.x + " " + transform.position.z);

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