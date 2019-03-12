using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// ---------------------------------------------- 
/// Class:        FixChildRotation
///
/// PROGRAM:      Some Kind Of MOBA
///
/// FUNCTIONS:    public void Awake()
///               public void Update()
/// 
/// DATE:         March 2nd, 2019
///
/// REVISIONS:    
///
/// DESIGNER:     Keishi Asai
///
/// PROGRAMMER:   Keishi Asai
///
/// NOTES:
/// This script prevents a child object from
/// rotating together with its parent's object rotation.
/// ---------------------------------------------- 
public class FixChildRotation : MonoBehaviour
{
    Quaternion rotation;
    void Awake()
    {
        rotation = transform.rotation;
    }
    void LateUpdate()
    {
        transform.rotation = rotation;
    }
}