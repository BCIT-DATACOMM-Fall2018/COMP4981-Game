using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectController : MonoBehaviour
{
    public GameObject player;
    public Dictionary<int, GameObject> GameActors { get; private set; } = new Dictionary<int, GameObject>();  


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void InstantiateObject(GameObjectType type, Vector3 location, int actorID){
        switch (type)
        {
            case GameObjectType.Player:
                GameActors.Add(actorID, Instantiate(player, location, Quaternion.identity));
                GameObject.Find("Main Camera").GetComponent<CameraController>().player = GameActors[actorID];
                break;
            default:
                break;
        }
    }
}
