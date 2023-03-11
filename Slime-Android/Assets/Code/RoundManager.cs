using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    [SerializeField] private int secondsToNextRound = 5;
    [SerializeField] private Text roundCountText;

    private int currentRound = 0;
    public int CurrentRound => currentRound;

    private bool roundStarted;
    public bool RoundStarted => roundStarted;

    public void StartNewRound()
    {
        currentRound++;
        roundCountText.text = "Round: " + currentRound;
        
        GameManager.Instance.Player.GetComponent<Player>().ResetHealth();

        roundStarted = true;

        GameManager.Instance.EnemySpawner.SpawnEnemies(currentRound < 5 ? 1 : (int)(currentRound / 5));
    }

    public void EndRound()
    {
        roundStarted = false;
        
        Invoke(nameof(StartNewRound), secondsToNextRound);
    }
}
