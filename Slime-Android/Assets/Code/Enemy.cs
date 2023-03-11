using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("MOVEMENT SETTINGS")]
    [SerializeField] private float movementSpeed = 2f;

    public float MovementSpeed => movementSpeed;
    
    [Space]
    
    [Header("ATTACK SETTINGS")] 
    [SerializeField] private float attackDamage = 2f;
    [SerializeField] private float attackRange = 0.2f;
    [SerializeField] private float attackCooldown = 1f;

    public float AttackDamage => attackDamage;
    public float AttackRange => attackRange;
    public float AttackCooldown => attackCooldown;

    [Space]
    
    [Header("HEALTH SETTINGS")] [SerializeField]
    private Image healthBar;
    [SerializeField] private float maxHealth = 20f;
    [SerializeField] private float currentHealth = 20f;

    private void OnEnable()
    {
        currentHealth = maxHealth;
        ChangeHealthBarFillAmount();
    }

    public void GetDamage(float damage)
    {
        currentHealth -= damage;
        ChangeHealthBarFillAmount();
        if (currentHealth <= 0) GameManager.Instance.EnemySpawner.DestroyEnemy(gameObject);
    }

    public void IncreaseDamage(float damage)
    {
        attackDamage += damage;
    }

    public void IncreaseHealth(float health)
    {
        maxHealth += health;
        ChangeHealthBarFillAmount();
    }
    
    private void ChangeHealthBarFillAmount()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    public void IncreaseAttackSpeed(float speed)
    {
        //attackSpeed += speed;
    }
}
