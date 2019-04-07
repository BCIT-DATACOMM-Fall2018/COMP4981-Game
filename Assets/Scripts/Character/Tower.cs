using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// ---------------------------------------------- 
/// Class:        Tower
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
/// DESIGNER:     Keishi Asai, Cameron Roberts
///
/// PROGRAMMER:   Cameron Roberts
///
/// NOTES:
/// Tower class.
/// ---------------------------------------------- 
public class Tower : Actor
{
    protected override void Start(){
        base.Start();
        alive = true;
    }
    protected override void  Update()
    {
        Debug.Log(Status.HP + " " + alive);
        if(Status.HP == 0 && alive){
            Die();
        }
        cUI.UpdateCharacterUI(Status);
    }
    public override void Die(){
        if(alive){
            Debug.Log("DIE DIE DIE");
            alive = false;
            transform.position = new Vector3(-10, 0, -10);
        }
    }
}