using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// ---------------------------------------------- 
/// Class:        ClientWrapper
///
/// PROGRAM:      Some Kind Of MOBA
///
/// FUNCTIONS:    public void SetId(int id)
///               public int GetId()
///               public void SetHealth(int health)
///               public int GetHealth()
/// 
/// DATE:         February 6th, 2019
///
/// REVISIONS:    
///
/// DESIGNER:     Dasha Strigoun
///
/// PROGRAMMER:   Dasha Strigoun
///
/// NOTES:        
/// ---------------------------------------------- 
public class ClientWrapper
{
    private int _Id;
    private int _Health;
    
    public void SetId(int id) {
        _Id = id;
    }

    public int GetId() {
        return _Id;
    }

    public void SetHealth(int health) {
        _Health = health;
    }

    public int GetHealth() {
        return _Health;
    }
}
