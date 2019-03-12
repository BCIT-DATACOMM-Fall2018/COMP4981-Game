using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public int health;

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) //If health is 0, die
            Destroy(this.gameObject);
    }
}
