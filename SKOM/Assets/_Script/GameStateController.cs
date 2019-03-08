using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine;
using NetworkLibrary;
using NetworkLibrary.MessageElements;
public class GameStateController : MonoBehaviour
{
    private GameObjectController objectController;
    private ClientStateMessageBridge stateBridge;
    private ConcurrentQueue<UpdateElement> elementQueue;

    // Start is called before the first frame update
    void Start()
    {
        elementQueue = ConnectionManager.Instance.MessageQueue;
        objectController = GetComponent<GameObjectController>();
        stateBridge = new ClientStateMessageBridge(objectController);
        objectController.InstantiateObject(GameObjectType.Player, new Vector3(), 1);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateElement updateElement;
        while(elementQueue.TryDequeue(out updateElement)){
            updateElement.UpdateState(stateBridge);
        }
    }
}
