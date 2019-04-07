using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavePoint : MonoBehaviour
{
    public static Transform waypoint;

    void Awake()
    {
        waypoint = transform; 
    }
}
