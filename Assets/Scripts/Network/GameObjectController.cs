using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetworkLibrary;

/// ----------------------------------------------
/// Class: 	GameObjectController - A script create and access GameObjects
/// 
/// PROGRAM: SKOM
///
/// FUNCTIONS:	void Start()
///				void Update()
///             public void InstantiateObject(ActorType type, Vector3 location, int actorId)
/// 
/// DATE: 		March 14th, 2019
///
/// REVISIONS: 
///
/// DESIGNER: 	Cameron Roberts
///
/// PROGRAMMER: Cameron Roberts
///
/// NOTES:		
/// ----------------------------------------------
public class GameObjectController : MonoBehaviour
{
    public GameObject Player;
    public GameObject AlliedPlayer;
    public Dictionary<int, GameObject> GameActors { get; private set; } = new Dictionary<int, GameObject>();  


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
    ///             Unused
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
    /// DESIGNER:	Cameron Roberts
    /// 
    /// PROGRAMMER:	Cameron Roberts
    /// 
    /// INTERFACE: 	void Update()
    /// 
    /// RETURNS: 	void
    /// 
    /// NOTES:		MonoBehaviour function. Called every frame.
    ///             Unused.
    /// ----------------------------------------------
    void Update()
    {
    }

    /// ----------------------------------------------
    /// FUNCTION:	InstantiateObject
    /// 
    /// DATE:		March 14th, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Cameron Roberts
    /// 
    /// PROGRAMMER:	Cameron Roberts
    /// 
    /// INTERFACE: 	public void InstantiateObject(ActorType type, Vector3 location, int actorId)
    ///                 ActorType type: The type of actor to be created
    ///                 Vector3 location: The location to instantiate the actor in
    ///                 int actorId: The id to assign to the new actor
    /// 
    /// RETURNS: 	void
    /// 
    /// NOTES:		
    /// ----------------------------------------------
    public void InstantiateObject(ActorType type, Vector3 location, int actorId){
        switch (type)
        {
            case ActorType.Player:
                GameActors.Add(actorId, Instantiate(Player, location, Quaternion.identity));
                GameObject.Find("Main Camera").GetComponent<CameraController>().player = GameActors[actorId];
                break;
            case ActorType.AlliedPlayer:
                GameActors.Add(actorId, Instantiate(AlliedPlayer, location, Quaternion.identity));
                break;
            default:
                break;
        }
        GameActors[actorId].GetComponent<Actor>().ActorId = actorId;
    }
}
