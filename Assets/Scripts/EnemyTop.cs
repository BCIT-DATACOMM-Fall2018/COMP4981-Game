using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTop : Enemy
{
    private void Start()
    {
        target = WavePointTop.waypoint;
        health = startHealth;
    }

    void OnCollisionEnter(Collision other)
    {
        target = WavePoint.waypoint;
        Debug.Log(gameObject.name + "has collided ")
    }
}
