using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    NavMeshAgent agent;

    public Vector3 TargetPosition;
    private Vector3 lookAtTarget;
    Quaternion playerRot;
    bool moving = false;

    public Animator animator;
    public float rotSpeed;
    public GameObject terrain;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
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