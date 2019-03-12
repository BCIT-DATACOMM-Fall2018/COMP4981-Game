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
}
