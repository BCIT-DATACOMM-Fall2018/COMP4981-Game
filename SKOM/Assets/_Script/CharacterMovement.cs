using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    NavMeshAgent agent;
    private Vector2 mouseOver;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(!Camera.main)
        {
            Debug.Log("No Main camera");
            return;
        }

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Debug.Log(Input.mousePosition);
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, LayerMask.GetMask("Field")))
            {
                agent.destination = hit.point;
            }
        }
    }
}