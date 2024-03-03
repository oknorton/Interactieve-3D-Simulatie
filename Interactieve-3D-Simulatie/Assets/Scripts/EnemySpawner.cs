using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : HealthSystem
{
    public GameObject enemyPrefab;
    public int maxEnemies, waveSpawnInterval, currentEnemies, spawnRange, activationRange;
    public int enemiesPerWave;
    private bool isActivated;
   

    private void Update()
    {
        if (!isActivated)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, activationRange);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    isActivated = true;
                    Debug.Log("Player entered spawner range, spawner activating");
                    StartCoroutine(SpawnEnemyWaves());

                    break;
                }
            }
        }
    }

    IEnumerator SpawnEnemyWaves()
    {
        if (currentEnemies < maxEnemies)
        {
            for (int i = 0; i < enemiesPerWave; i++)
            {
                if (currentEnemies >= maxEnemies)
                    break;

                SpawnEnemy();
            }
        }
        while (true)
        {
            yield return new WaitForSeconds(waveSpawnInterval);

            if (currentEnemies < maxEnemies)
            {
                for (int i = 0; i < enemiesPerWave; i++)
                {
                    if (currentEnemies >= maxEnemies)
                        break;

                    SpawnEnemy();
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, activationRange);
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRange;
        spawnPosition.y = 0; 

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        currentEnemies++;
    }
}