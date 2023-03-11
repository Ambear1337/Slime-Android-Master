using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("HEALTH SETTINGS")] [SerializeField]
    private Image healthBar;
    
    private float damage = 5f;
    private float health = 50f;
    private float maxHealth = 50f;
    private int money = 0;
    private float attackSpeed = 100f;

    public float Damage
    {
        get
        {
            return damage;
        }
        set
        {
            damage = value; 
            playerShoot.ChangeBulletsDamage(damage);
        }
    }
    public float Health => health;
    public float MaxHealth => maxHealth;
    public int Money => money;
    public float AttackSpeed => attackSpeed;
    
    private PlayerShoot playerShoot;

    [SerializeField] private Text coinsText;

    private void Start()
    {
        ChangeHealthBarFillAmount();
        UpdateCoinsText();;
    }

    public void GetDamage(float dmg)
    {
        health -= dmg;
        ChangeHealthBarFillAmount();

        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void IncreaseHealth(float hp)
    {
        maxHealth += hp;
        ChangeHealthBarFillAmount();
    }

    public void ResetHealth()
    {
        health = maxHealth;
        ChangeHealthBarFillAmount();
    }

    public void GetMoney(int mny)
    {
        money += mny;
        UpdateCoinsText();
    }

    public void SpendMoney(int cost)
    {
        money -= cost;
        UpdateCoinsText();
    }

    public void IncreaseDamage(float dmg)
    {
        damage += dmg;
        
        playerShoot.ChangeBulletsDamage(damage);
    }

    public void IncreaseAttackSpeed(float speed)
    {
        attackSpeed += speed;
        
        playerShoot.ChangeShootCooldown();
    }

    private void ChangeHealthBarFillAmount()
    {
        healthBar.fillAmount = health / maxHealth;
    }

    private void UpdateCoinsText()
    {
        coinsText.text = "Coins: " + money;
    }

    private void Awake()
    {
        playerShoot = GetComponent<PlayerShoot>();
    }
}
