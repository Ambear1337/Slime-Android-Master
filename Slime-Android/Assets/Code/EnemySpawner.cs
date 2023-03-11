using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemiesPool = new List<GameObject>();
    public List<Transform> enemiesSpawnPoints = new List<Transform>();

    public Transform player;

    public GameObject enemyPrefab;

    private int enemiesAlive;
    public int EnemiesAlive => enemiesAlive;

    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemy.GetComponent<EnemyMovement>().player = player;
        
        enemiesPool.Add(enemy);
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    public void SpawnEnemies(int enemiesToSpawn)
    {
        if (enemiesToSpawn > enemiesPool.Count)
        {
            StartCoroutine(SpawnEnemiesCoroutine(enemiesToSpawn));
        }

        enemiesAlive = 0;
        
        foreach (var e in enemiesPool)
        {
            e.transform.position = enemiesSpawnPoints[enemiesAlive].position;

            Enemy enemyStats = e.GetComponent<Enemy>();
            
            enemyStats.IncreaseHealth(GameManager.Instance.RoundManager.CurrentRound * 2);
            enemyStats.IncreaseDamage(GameManager.Instance.RoundManager.CurrentRound / 2f);
            
            e.SetActive(true);
            enemiesAlive++;
        }
    }

    IEnumerator SpawnEnemiesCoroutine(int enemiesToSpawn)
    {
        while (enemiesToSpawn > enemiesPool.Count)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1f);
        }
    }

    public void DestroyEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        
        GameManager.Instance.Player.GetComponent<Player>().GetMoney(GameManager.Instance.RoundManager.CurrentRound * 5);

        if (enemiesPool.Any(e => e.activeSelf))
        {
            return;
        }
        
        GameManager.Instance.RoundManager.EndRound();
    }
}
