using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Prefab & Spawn locaties")]
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    [Header("Spawn instellingen")]
    public float spawnInterval = 30f;

    void Start()
    {
        // Controleer direct of alles goed ingesteld is
        if (enemyPrefab == null || spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("❌ EnemyPrefab of SpawnPoints niet ingesteld in de Inspector!");
            return;
        }

        // Spawn meteen een wave
        SpawnWave();

        // Herhaal elke X seconden
        InvokeRepeating(nameof(SpawnWave), spawnInterval, spawnInterval);
    }

    void SpawnWave()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            // Spawn 2 enemies per spawnPoint
            for (int i = 0; i < 2; i++)
            {
                Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            }
        }
    }
}
