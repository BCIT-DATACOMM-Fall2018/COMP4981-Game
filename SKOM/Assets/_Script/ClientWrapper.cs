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
    
    /// ---------------------------------------------- 
    /// FUNCTION:    Set Id of character
    ///
    /// DATE:        February 6th, 2019
    ///        
    /// REVISION:    
    ///
    /// DESIGNER:    Dasha Strigoun
    ///
    /// PROGRAMMER:  Dasha Strigoun
    ///
    /// INTERFACE:   SetId(int id)
    ///                 - int id: new id
    ///
    /// RETURNS:     void
    ///
    /// NOTES:       
    /// ---------------------------------------------- 
    public void SetId(int id) {
        _Id = id;
    }

    /// ---------------------------------------------- 
    /// FUNCTION:    Get Id of character
    ///
    /// DATE:        February 6th, 2019
    ///        
    /// REVISION:    
    ///
    /// DESIGNER:    Dasha Strigoun
    ///
    /// PROGRAMMER:  Dasha Strigoun
    ///
    /// INTERFACE:   GetId()
    ///
    /// RETURNS:     int
    ///
    /// NOTES:       
    /// ---------------------------------------------- 
    public int GetId() {
        return _Id;
    }

    /// ---------------------------------------------- 
    /// FUNCTION:    Set Health of character
    ///
    /// DATE:        February 6th, 2019
    ///        
    /// REVISION:    
    ///
    /// DESIGNER:    Dasha Strigoun
    ///
    /// PROGRAMMER:  Dasha Strigoun
    ///
    /// INTERFACE:   SetHealth(int health)
    ///                 - int health: new health
    ///
    /// RETURNS:     void
    ///
    /// NOTES:       
    /// ---------------------------------------------- 
    public void SetHealth(int health) {
        _Health = health;
    }

    /// ---------------------------------------------- 
    /// FUNCTION:    Get Health of character
    ///
    /// DATE:        February 6th, 2019
    ///        
    /// REVISION:    
    ///
    /// DESIGNER:    Dasha Strigoun
    ///
    /// PROGRAMMER:  Dasha Strigoun
    ///
    /// INTERFACE:   GetHealth()
    ///
    /// RETURNS:     int
    ///
    /// NOTES:       
    /// ---------------------------------------------- 
    public int GetHealth() {
        return _Health;
    }
}
