using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreepMovementTop : MonoBehaviour
{
    GameObject towerTop1;
    GameObject towerTop2;

    Transform enemyBase;
    NavMeshAgent nav;

    //Tags of towers and enemy base
    private readonly string towerTop1Tag = "Tower-Top-1";
    private readonly string towerTop2Tag = "Tower-Top-2";

    private readonly string enemyBaseTag = "Enemy-Base";

    void Awake()
    {
        if (GameObject.FindWithTag(towerTop1Tag) != null)
            towerTop1 = GameObject.FindGameObjectWithTag(towerTop1Tag);

        if (GameObject.FindWithTag(towerTop2Tag) != null)
            towerTop2 = GameObject.FindGameObjectWithTag(towerTop2Tag);

        enemyBase = GameObject.FindGameObjectWithTag(enemyBaseTag).transform;
        
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (GameObject.FindWithTag(towerTop1Tag) != null)
            nav.SetDestination(towerTop1.transform.position);
        else if (GameObject.FindWithTag(towerTop2Tag) != null)
            nav.SetDestination(towerTop2.transform.position);
        else
            nav.SetDestination(enemyBase.position);
    }

    // Unity calls OnTriggerEnter when an object first touches a trigger collider
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyBaseTag)) //If reach the enemy base, destroy self
            Destroy(this.gameObject);
        else
        {
            TowerManager towerManagerScript = other.GetComponent<TowerManager>();
            towerManagerScript.health -= 5;
        }
    }


}
