using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreepMovementMid : MonoBehaviour
{
    //For pathfinding
    Transform towerMid1;
    Transform towerMid2;

    Transform enemyBase;
    NavMeshAgent nav;

    //Tags of towers and enemy base
    private readonly string towerMid1Tag = "Tower-Mid-1";
    private readonly string towerMid2Tag = "Tower-Mid-2";

    private readonly string enemyBaseTag = "Enemy-Base";

    void Awake()
    {
        if (GameObject.FindWithTag(towerMid1Tag) != null)
            towerMid1 = GameObject.FindGameObjectWithTag(towerMid1Tag).transform;

        if (GameObject.FindWithTag(towerMid2Tag) != null)
            towerMid2 = GameObject.FindGameObjectWithTag(towerMid2Tag).transform;

        enemyBase = GameObject.FindGameObjectWithTag(enemyBaseTag).transform;

        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (GameObject.FindWithTag(towerMid1Tag) != null)
            nav.SetDestination(towerMid1.position);
        else if (GameObject.FindWithTag(towerMid2Tag) != null)
            nav.SetDestination(towerMid2.position);
        else
            nav.SetDestination(enemyBase.position);
    }

    // Unity calls OnTriggerEnter when an object first touches a trigger collider
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyBaseTag)) //If reach the enemy base, destroy self
            Destroy(this.gameObject);
        else
            other.gameObject.SetActive(false);
    }
}
