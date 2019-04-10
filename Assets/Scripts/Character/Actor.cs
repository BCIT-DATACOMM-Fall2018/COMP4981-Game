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
///               public virtual void Die()
/// 
/// DATE:         February 7th, 2019
///
/// REVISIONS:    
///
/// DESIGNER:     Keishi Asai
///
/// PROGRAMMER:   Keishi Asai, Cameron Roberts
///
/// NOTES:
/// Character class.
/// ---------------------------------------------- 
public class Actor : MonoBehaviour
{
    protected ActorUI cUI;
    public Status Status;
    public int ActorId {get;set;}

    protected bool alive;
    public bool banished;

    [HideInInspector]
    public GameObject deathObject;

    /// ----------------------------------------------
	/// FUNCTION:	Start()
	///
	/// DATE:		February 7th, 2019
	///
	/// REVISIONS:  
	///
	/// DESIGNER:	Keishi Asai
	///
	/// PROGRAMMER:	Keishi Asai
	///
	/// INTERFACE: 	void Start()
	///
	/// RETURNS: 	void
	///
	/// NOTES:		Start is called before the first frame update
	/// ----------------------------------------------    
    protected virtual void Start()
    {
        Status.HP = 1000;
        cUI = GetComponent<ActorUI>();
    }

    /// ----------------------------------------------
	/// FUNCTION:	Update()
	///
	/// DATE:		February 7th, 2019
	///
	/// REVISIONS:  
	///
	/// DESIGNER:	Keishi Asai
	///
	/// PROGRAMMER:	Keishi Asai
	///
	/// INTERFACE: 	void Update()
	///
	/// RETURNS: 	void
	///
	/// NOTES:		Called once per frame update
	/// ----------------------------------------------      
    protected virtual void Update()
    {
        if(transform.position.x != -10){
            alive = true;
        }
        cUI.UpdateCharacterUI(Status);
    }

    /// ----------------------------------------------
	/// FUNCTION:	Die()
	///
	/// DATE:		February 7th, 2019
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
	/// NOTES:		Spawns the actors death object and makes it play the death animation.
    ///             Death object is destroyed after 3 seconds.
	/// ----------------------------------------------    
    public virtual void Die(){
        if(alive){
            GameObject clone = Instantiate(deathObject, transform.position, transform.rotation);
            //TODO make the created object animate
            clone.GetComponent<Animator>().SetInteger("die", new System.Random().Next(1,3));
            Destroy(clone, 3);
            alive = false;
        }
    }
}