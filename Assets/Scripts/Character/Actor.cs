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

    private bool alive;

    [HideInInspector]
    public GameObject deathObject;

    // Start is called before the first frame update
    void Start()
    {
        cUI = GetComponent<ActorUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x != -10){
            alive = true;
        }
        cUI.UpdateCharacterUI(Status);
    }

    public void Die(){
        if(alive){
            GameObject clone = Instantiate(deathObject, transform.position, transform.rotation);
            //TODO make the created object animate
            clone.GetComponent<Animator>().SetInteger("die", new System.Random().Next(1,3));
            Destroy(clone, 3);
            alive = false;
        }
    }
}