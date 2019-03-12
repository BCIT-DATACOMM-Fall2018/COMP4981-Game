using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavePointTop : MonoBehaviour
{
    public static Transform waypoint;

    void Awake()
    {
        waypoint = transform;
    }
}
