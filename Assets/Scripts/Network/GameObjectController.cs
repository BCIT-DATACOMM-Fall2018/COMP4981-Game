using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetworkLibrary;

public class GameObjectController : MonoBehaviour
{
    public GameObject Player;
    public GameObject AlliedPlayer;
    public Dictionary<int, GameObject> GameActors { get; private set; } = new Dictionary<int, GameObject>();  


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

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
        GameActors[actorId].GetComponent<Character>().ActorId = actorId;
    }
}
