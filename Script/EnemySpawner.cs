using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;  
    public float spawnInterval = 5f;  
    public float spawnRadius = 5f; 
    public bool spawning = false; 

    private float timeSinceLastSpawn;

    void Start()
    {
        timeSinceLastSpawn = 0f;
    }

    void Update()
    {
        if (spawning)
        {
            timeSinceLastSpawn += Time.deltaTime;

            if (timeSinceLastSpawn >= spawnInterval)
            {
                SpawnEnemy();
                timeSinceLastSpawn = 0f;
            }
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPosition = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
