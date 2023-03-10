using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Transform player;
    public Transform Player => player;

    private RoundManager roundManager;
    public RoundManager RoundManager => roundManager;

    private EnemySpawner enemySpawner;
    public EnemySpawner EnemySpawner => enemySpawner;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        player = FindObjectOfType<Player>().transform;
        roundManager = FindObjectOfType<RoundManager>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    public void StartGame()
    {
        roundManager.StartNewRound();
    }
}
