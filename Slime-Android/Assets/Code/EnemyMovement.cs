using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    
    private bool canAttack = true;
    
    private Enemy stats;

    private Rigidbody rb;

    private void OnEnable()
    {
        canAttack = true;
    }

    private void Awake()
    {
        stats = GetComponent<Enemy>();
        rb = GetComponent<Rigidbody>();

        player = GameManager.Instance.Player;
    }

    private void FixedUpdate()
    {
        if (transform.position.x > player.position.x + stats.AttackRange)
        {
            rb.velocity = Vector3.left * (stats.MovementSpeed * Time.deltaTime);
        }
        else
        {
            rb.velocity = Vector3.zero;
            Attack();
        }
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    private void Attack()
    {
        if (player == null || !canAttack) return;
        
        player.GetComponent<Player>().GetDamage(stats.AttackDamage);
        
        canAttack = false;
        StartCoroutine(AttackCooldown());
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(stats.AttackCooldown);

        canAttack = true;
    }
}
