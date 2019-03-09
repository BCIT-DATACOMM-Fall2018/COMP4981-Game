using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetworkLibrary;
using NetworkLibrary.MessageElements;

public class GameStateReporter : MonoBehaviour
{

    private GameObjectController objectController;


    // Start is called before the first frame update
    void Start()
    {
        objectController = GetComponent<GameObjectController>();
    }

    // Update is called once per frame
    void Update()
    {
        List<UpdateElement> gameState = new List<UpdateElement>();
        Debug.Log(objectController.GameActors[1].GetComponent<Character>().Status.HP);
        gameState.Add(new HealthElement(1,objectController.GameActors[1].GetComponent<Character>().Status.HP));
        ConnectionManager.Instance.SendStatePacket(gameState);
    }
}
