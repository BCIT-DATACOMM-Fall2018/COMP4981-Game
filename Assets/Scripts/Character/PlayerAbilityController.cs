
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetworkLibrary;
using NetworkLibrary.MessageElements;

/// ----------------------------------------------
/// Class: 	PlayerAbilityController - A script to provide the logic for player
///                                   initiated abilities.
/// 
/// PROGRAM: SKOM
///
/// FUNCTIONS:	void Start()
///				void Update()
/// 
/// DATE: 		March 14th, 2019
///
/// REVISIONS: 
///
/// DESIGNER: 	Simon Wu, Cameron Roberts
///
/// PROGRAMMER: Simon Wu, Cameron Roberts
///
/// NOTES:		
/// ----------------------------------------------
public class PlayerAbilityController : AbilityController
{


    /// ----------------------------------------------
    /// FUNCTION:	Start
    /// 
    /// DATE:		March 14th, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Cameron Roberts
    /// 
    /// PROGRAMMER:	Cameron Roberts
    /// 
    /// INTERFACE: 	void Start()
    /// 
    /// RETURNS: 	void
    /// 
    /// NOTES:		MonoBehaviour function.
    ///             Called before the first Update().
    ///             Currently unused.
    /// ----------------------------------------------
    void Start()
    {

    }

    /// ----------------------------------------------
    /// FUNCTION:	Update
    /// 
    /// DATE:		March 14th, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Cameron Roberts, Simon Wu
    /// 
    /// PROGRAMMER:	Cameron Roberts, Simon Wu
    /// 
    /// INTERFACE: 	void Update()
    /// 
    /// RETURNS: 	void
    /// 
    /// NOTES:		MonoBehaviour function. Called every frame.
    ///             Checks if a key is pressed and queues
    ///             a reliable Update element based on the 
    ///             ability used.
    /// ----------------------------------------------
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)){
            Debug.Log("Bang!" + transform.position.x);
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            int actorId = gameObject.GetComponent<Character>().ActorId;

            if (GameObject.Find("Terrain").GetComponent<Collider>().Raycast (ray, out hit, Mathf.Infinity)) {
                ConnectionManager.Instance.QueueReliableElement(new AreaAbilityElement(actorId, AbilityType.Placeholder, hit.point.x, hit.point.z));
            }
        }
    }
}