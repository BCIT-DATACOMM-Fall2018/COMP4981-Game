using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fountain : MonoBehaviour
{

    float timer;
    JungleCampController walker;

    // Start is called before the first frame update
    void Start()
    {
        timer = 1;
        walker = GetComponent<JungleCampController>();

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        Debug.Log("Timer: " + timer);
        if (timer < -0.2)
        {
            walker.die = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Object enter the trigger");
    }


    void OnTriggerStay(Collider other)
    {
        Debug.Log("Object is within trigger");
        if (timer < 0)
        {
            int result = walker.IncHealth();
            Debug.Log("Person's updated health: " + result);
            timer = 1;
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Object Exited the trigger");

    }


}
