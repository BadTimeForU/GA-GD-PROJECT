using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 30f;
    public int maxEnemies = 30;
    public int enemiesPerSpawn = 2;
    public float respawnDelay = 0.5f; // Nu 0.5 seconden zodat je het ziet

    private List<GameObject> activeEnemies = new List<GameObject>();
    private bool hasPerformedInitialRespawn = false;

    void Start()
    {
        SpawnInitialEnemies();
        StartCoroutine(InitialRespawnRoutine());
    }

    IEnumerator InitialRespawnRoutine()
    {
        if (hasPerformedInitialRespawn) yield break;
        hasPerformedInitialRespawn = true;

        // Wacht tot alle enemies gespawnd zijn
        yield return new WaitForEndOfFrame();

        Debug.Log("Start despawn/respawn sequence");

        // Eerst alle enemies uitschakelen
        foreach (var enemy in activeEnemies.ToArray())
        {
            if (enemy != null)
            {
                enemy.SetActive(false);
                Debug.Log("Enemy despawned: " + enemy.name);
            }
        }

        // Wacht even zodat je het ziet
        yield return new WaitForSeconds(respawnDelay);

        // Nu alle enemies weer inschakelen
        foreach (var enemy in activeEnemies.ToArray())
        {
            if (enemy != null)
            {
                enemy.SetActive(true);
                enemy.transform.position = GetRandomSpawnPosition();
                Debug.Log("Enemy respawned: " + enemy.name);
            }
        }

        // Start normale spawn cyclus
        InvokeRepeating("TrySpawnEnemies", spawnInterval, spawnInterval);
    }

    Vector3 GetRandomSpawnPosition()
    {
        return transform.position + new Vector3(
            Random.Range(-2f, 2f),
            0,
            Random.Range(-2f, 2f));
    }

    void SpawnInitialEnemies()
    {
        for (int i = 0; i < enemiesPerSpawn; i++)
        {
            SpawnSingleEnemy();
        }
    }

    void SpawnSingleEnemy()
    {
        GameObject newEnemy = Instantiate(enemyPrefab, GetRandomSpawnPosition(), Quaternion.identity);
        activeEnemies.Add(newEnemy);
        Debug.Log("Initial spawn: " + newEnemy.name);
    }

    void TrySpawnEnemies()
    {
        activeEnemies.RemoveAll(enemy => enemy == null);

        if (activeEnemies.Count >= maxEnemies) return;

        int canSpawn = Mathf.Min(enemiesPerSpawn, maxEnemies - activeEnemies.Count);
        for (int i = 0; i < canSpawn; i++)
        {
            SpawnSingleEnemy();
        }
    }
}