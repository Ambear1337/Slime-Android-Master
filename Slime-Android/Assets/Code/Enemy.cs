using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("MOVEMENT SETTINGS")]
    [SerializeField] private float movementSpeed = 2f;

    public float MovementSpeed => movementSpeed;
    
    [Space]
    
    [Header("ATTACK SETTINGS")] 
    [SerializeField] private float attackDamage = 2f;
    [SerializeField] private float attackRange = 0.2f;

    public float AttackDamage => attackDamage;
    public float AttackRange => attackRange;
    
    [Space]
    
    [Header("HEALTH SETTINGS")]
    [SerializeField] private float maxHealth = 20f;
    [SerializeField] private float currentHealth = 20f;

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public void GetDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) GameManager.Instance.EnemySpawner.DestroyEnemy(gameObject);
    }

    private void OnDisable()
    {
        
    }
}
