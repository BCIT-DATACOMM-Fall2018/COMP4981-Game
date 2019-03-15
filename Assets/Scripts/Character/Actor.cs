using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Status
{
    public int HP {get; set;}
}

/// ---------------------------------------------- 
/// Class:        Actor
///
/// PROGRAM:      Some Kind Of MOBA
///
/// FUNCTIONS:    public void Start()
///               public void Update()
/// 
/// DATE:         February 7th, 2019
///
/// REVISIONS:    
///
/// DESIGNER:     Keishi Asai
///
/// PROGRAMMER:   Keishi Asai
///
/// NOTES:
/// Character class.
/// ---------------------------------------------- 
public class Actor : MonoBehaviour
{
    private ActorUI cUI;
    public Status Status;
    public int ActorId {get;set;}

    // Start is called before the first frame update
    void Start()
    {
        cUI = GetComponent<ActorUI>();
    }

    // Update is called once per frame
    void Update()
    {
        cUI.UpdateCharacterUI(Status);
    }
}