using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;

    private Enemy stats;

    private void Awake()
    {
        stats = GetComponent<Enemy>();

        player = GameManager.Instance.Player;
    }

    private void Update()
    {
        if (transform.position.x > player.position.x) transform.Translate(Vector3.left * (stats.MovementSpeed * Time.deltaTime));
    }
}
