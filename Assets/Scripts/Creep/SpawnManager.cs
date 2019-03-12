using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject topCreep;
    public GameObject midCreep;
    public GameObject bottomCreep;

    public float spawnTime = 30f;
    public Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Spawn()
    {
        for (int i = 0; i < 5; i++)
            Instantiate(topCreep, spawnPoint.position, spawnPoint.rotation);

        for (int i = 0; i < 5; i++)
            Instantiate(midCreep, spawnPoint.position, spawnPoint.rotation);

        for (int i = 0; i < 5; i++)
            Instantiate(bottomCreep, spawnPoint.position, spawnPoint.rotation);
    }
}
