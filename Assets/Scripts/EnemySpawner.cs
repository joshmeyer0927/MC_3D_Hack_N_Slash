using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Enemy[] enemyPrefabs;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] float respawnRate = 10f;
    [SerializeField] float initialSpawnDelay;
    [SerializeField] int totalNumberToSpawn;
    [SerializeField] int numberToSpawnEachTime = 1;

    float spawnTimer;
    int totalNumberSpawned;

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (ShouldSpawn())
            Spawn();
    }

    void OnEnable()
    {
        spawnTimer = respawnRate - initialSpawnDelay;
    }

    bool ShouldSpawn()
    {
        if (totalNumberToSpawn > 0 && totalNumberSpawned >= totalNumberToSpawn)
            return false;

        return spawnTimer >= respawnRate;
    }

    void Spawn()
    {
        spawnTimer = 0;

        var availableSpawnPoints = spawnPoints.ToList();

        for (int i = 0; i < numberToSpawnEachTime; i++)
        {
            if (totalNumberToSpawn > 0 && totalNumberSpawned >= totalNumberToSpawn)
                break;

            Enemy prefab = ChooseRandomEnemyPrefab();

            if (prefab != null)
            {
                Transform spawnPoint = ChooseRandomSpawnPoint(availableSpawnPoints);

                if (availableSpawnPoints.Contains(spawnPoint))
                    availableSpawnPoints.Remove(spawnPoint);

                //var enemy = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
                var enemy = prefab.Get<Enemy>(spawnPoint.position, spawnPoint.rotation);

                totalNumberSpawned++;
            }
        }
    }

    Transform ChooseRandomSpawnPoint(List<Transform> availableSpawnPoints)
    {
        if (availableSpawnPoints.Count == 0)
            return transform;

        if (availableSpawnPoints.Count == 1)
            return spawnPoints[0];

        int index = UnityEngine.Random.Range(0, availableSpawnPoints.Count);

        return availableSpawnPoints[index];
    }

    Enemy ChooseRandomEnemyPrefab()
    {
        if (enemyPrefabs.Length == 0)
            return null;

        if (enemyPrefabs.Length == 1)
            return enemyPrefabs[0];

        int index = UnityEngine.Random.Range(0, enemyPrefabs.Length);

        return enemyPrefabs[index];
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position, Vector3.one);

        foreach (var spawnPoint in spawnPoints)
            Gizmos.DrawSphere(spawnPoint.position, .5f);
    }
#endif
}
