using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        // TODO: call client wrapper to init hp
        status.hp = 100;
    }

    private void addHP(float heal)
    {
        // TODO: call client wrapper
        status.hp += heal;
    }

    private void subtractHP(float damage)
    {
        // TODO: call client wrapper
        status.hp -= damage;
    }

}