using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Status
{
    public int HP {get; set;}
}

/// ---------------------------------------------- 
/// Class:        Character
///
/// PROGRAM:      Some Kind Of MOBA
///
/// FUNCTIONS:    public void Start()
///               public void Update()
///               private void initStatus()
///               private void subtractHP(float damage)
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
public class Character : MonoBehaviour
{
    private CharacterUI cUI;
    public Status Status;

    // Start is called before the first frame update
    void Start()
    {
        cUI = GetComponent<CharacterUI>();
        InitStatus();
    }

    // Update is called once per frame
    void Update()
    {
        cUI.UpdateCharacterUI(Status);

        // TODO: Remove this test code after actual hp control was implmented
    }
    
    private void InitStatus()
    {
        // TODO: call client wrapper to init hp
        Status.HP = 12;
    }

    public void AddHP(int heal)
    {
        // TODO: call client wrapper
        Status.HP += heal;
    }

    public void SubtractHP(int damage)
    {
        // TODO: call client wrapper
        Status.HP -= damage;
    }

}