using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemiesPool = new List<GameObject>();

    public Transform player;

    public GameObject enemyPrefab;

    private int enemiesAlive;
    public int EnemiesAlive => enemiesAlive;

    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, new Vector3(3, 0, 0), Quaternion.identity);
        enemy.GetComponent<EnemyMovement>().player = player;
        
        enemiesPool.Add(enemy);
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    public void SpawnEnemies(int enemiesToSpawn)
    {
        if (enemiesToSpawn > enemiesPool.Count)
        {
            while (enemiesToSpawn > enemiesPool.Count)
            {
                SpawnEnemy();
            }
        }

        enemiesAlive = 0;
        
        foreach (var e in enemiesPool)
        {
            e.SetActive(true);
            enemiesAlive++;
        }
    }

    public void DestroyEnemy(GameObject enemy)
    {
        enemy.SetActive(false);

        if (enemiesPool.Any(e => e.activeSelf))
        {
            return;
        }
        
        GameManager.Instance.RoundManager.EndRound();
    }
}
