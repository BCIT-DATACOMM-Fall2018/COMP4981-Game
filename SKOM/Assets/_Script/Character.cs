using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NetworkLibrary.IStateMessageBridge;

public struct Status
{
    public float hp;
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
    private Status status;

    // Start is called before the first frame update
    void Start()
    {
        cUI = GetComponent<CharacterUI>();
        initStatus();
    }

    // Update is called once per frame
    void Update()
    {
        cUI.updateCharacterUI(status);

        // TODO: Remove this test code after actual hp control was implmented
        subtractHP(0.5f);
    }
    
    private void initStatus()
    {
        status.hp = 100;

        UpdateActorHealth(1, status.hp);
    }

    private void addHP(float heal)
    {
        status.hp += heal;

        UpdateActorHealth(1, status.hp);
    }

    private void subtractHP(float damage)
    {
        status.hp -= damage;

        UpdateActorHealth(1, status.hp);
    }

    // Dasha Notes:
    //
    // Is this file going to hold all local information about the character?
    // Where will the id of the character be initialized?
    // I think the ClientWrapper C# is not needed anymore..

}