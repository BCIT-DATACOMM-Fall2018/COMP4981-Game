
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetworkLibrary;
using NetworkLibrary.MessageElements;

public class PlayerAbilityController : AbilityController
{


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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