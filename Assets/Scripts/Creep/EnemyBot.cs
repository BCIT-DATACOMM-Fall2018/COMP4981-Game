using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBot : Enemy
{
    private void Start()
    {
        target = WavePointBot.waypoint;
        health = startHealth;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "WavePointBot")
        {
            target = WavePoint.waypoint;
        }
        Debug.Log(gameObject.name + " has collided with " + other.gameObject.name);
    }
}
