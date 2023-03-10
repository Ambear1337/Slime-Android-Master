using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] private int secondsToNextRound = 5;

    private int currentRound = 0;

    public void StartNewRound()
    {
        currentRound++;

        GameManager.Instance.EnemySpawner.SpawnEnemies(currentRound < 5 ? 1 : (int)(currentRound / 5));
    }

    public void EndRound()
    {
        StartCoroutine(NextRoundTimer());
    }

    private IEnumerator NextRoundTimer()
    {
        yield return new WaitForSeconds(secondsToNextRound);
        
        StartNewRound();
    }
}
