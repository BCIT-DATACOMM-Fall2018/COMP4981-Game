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
/// DATE:         April 5th, 2019
///
/// REVISIONS:    
///
/// DESIGNER:     Cameron Roberts
///
/// PROGRAMMER:   Cameron Roberts
///
/// NOTES:
/// Tower class.
/// ---------------------------------------------- 
public class Tower : Actor
{
    /// ----------------------------------------------
	/// FUNCTION:	Start()
	///
	/// DATE:		April 5th, 2019
	///
	/// REVISIONS:  
	///
	/// DESIGNER:	Keishi Asai
	///
	/// PROGRAMMER:	Cameron Roberts
	///
	/// INTERFACE: 	void Start()
	///
	/// RETURNS: 	void
	///
	/// NOTES:		Start is called before the first frame update
	/// ----------------------------------------------   
    protected override void Start(){
        base.Start();
        alive = true;
    }

    /// ----------------------------------------------
	/// FUNCTION:	Update()
	///
	/// DATE:		April 5th, 2019
	///
	/// REVISIONS:  
	///
	/// DESIGNER:	Keishi Asai
	///
	/// PROGRAMMER:	Cameron Roberts
	///
	/// INTERFACE: 	void Update()
	///
	/// RETURNS: 	void
	///
	/// NOTES:		Called once per frame update
	/// ----------------------------------------------    
    protected override void  Update()
    {
        Debug.Log(Status.HP + " " + alive);
        if(Status.HP == 0 && alive){
            Die();
        }
        cUI.UpdateCharacterUI(Status);
    }

    /// ----------------------------------------------
	/// FUNCTION:	Die()
	///
	/// DATE:		April 5th, 2019
	///
	/// REVISIONS:  
	///
	/// DESIGNER:	Cameron Roberts
	///
	/// PROGRAMMER:	Cameron Roberts
	///
	/// INTERFACE: 	void Update()
	///
	/// RETURNS: 	void
	///
	/// NOTES:		Moves the tower to a non visible location
	/// ---------------------------------------------- 
    public override void Die(){
        if(alive){
            alive = false;
            transform.position = new Vector3(-10, 0, -10);
        }
    }
}