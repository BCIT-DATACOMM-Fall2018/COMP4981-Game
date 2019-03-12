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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "WavePointTop") {
           target = WavePoint.waypoint;
        }
        //Debug.Log(gameObject.name + " has collided with " + other.gameObject.name);
    }
}
