using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform enemyTopPrefab;
    public Transform enemyBotPrefab;

    public Transform spawnPoint;

    public float timeBetweenWaves = 15f;
    private float countdown = 0;

    private int enemyCount = 3;

    void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave() {
        for (int i = 0; i < enemyCount; i++) {
            SpawnEnemy();
            yield return new WaitForSeconds(2f);
        }
    }

    void SpawnEnemy() {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        Instantiate(enemyTopPrefab, spawnPoint.position, spawnPoint.rotation);
        Instantiate(enemyBotPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
