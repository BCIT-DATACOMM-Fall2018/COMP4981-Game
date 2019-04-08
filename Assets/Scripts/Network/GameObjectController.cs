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
///             public void InstantiateObject(ActorType type, Vector3 location, int actorId, int team)
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
    public GameObject[] Players;
    public GameObject[] NonPlayers;
    public GameObject[] DummyPlayers;
    public GameObject Tower;
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
    /// REVISIONS:	March 17th, 2019
    ///                 Modify to support teams
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
    public void InstantiateObject(ActorType type, Vector3 location, int actorId, int team){
        if((int)type < 10){
                if(actorId == ConnectionManager.Instance.ClientId){
                    GameActors.Add(actorId, Instantiate(Players[(int) type], location, Quaternion.identity));
                    GameObject.Find("PlayerLockCamera").GetComponent<PlayerLockCamera>().player = GameActors[actorId];
                    GameObject.Find("PlayerLockCamera").GetComponent<CameraOcclusion>().playerPos = GameActors[actorId].transform;
                    GameObject.Find("IsometricCamera").GetComponent<CameraOcclusion>().playerPos = GameActors[actorId].transform;
                    GameActors[actorId].GetComponent<CameraSelector>().playerLockCamera = GameObject.Find("PlayerLockCamera").GetComponent<Camera>();
                    GameActors[actorId].GetComponent<CameraSelector>().isometricCamera = GameObject.Find("IsometricCamera").GetComponent<Camera>();
                    GameObject.Find("PlayerLockCamera").GetComponent<PlayerLockCamera>().SetCameraLock();
                    GameActors[actorId].GetComponent<CameraSelector>().SetIsometricToPlayer();
                    
                    GameActors[actorId].GetComponent<Actor>().deathObject = DummyPlayers[(int) type];
                
                } else {
                    GameActors.Add(actorId, Instantiate(NonPlayers[(int) type], location, Quaternion.identity));
                }

        }
        else
        {
            if(type == ActorType.TowerA){
                GameActors.Add(actorId, Instantiate(Tower, location, Tower.transform.rotation));
            }
        }
        if(team == ConnectionManager.Instance.Team){
            GameActors[actorId].tag = "Ally";
        } else {
            GameActors[actorId].tag = "Enemy";
        }
        GameActors[actorId].GetComponent<Actor>().ActorId = actorId;

    }
}
