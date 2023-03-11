using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnhancingPanel : MonoBehaviour
{
    private Player player;

    public EnhanceType enhanceType;

    [SerializeField] private float increasingMultiplier = 2f;
    [SerializeField] private int costMultiplier = 2;

    [SerializeField] private int cost = 5;
    [SerializeField] private int level = 1;

    [SerializeField] private string nameString;
    [SerializeField] private string descriptionString;

    [SerializeField] private Text costText;
    [SerializeField] private Text nameText;
    [SerializeField] private Text levelText;
    [SerializeField] private Text descriptionText;

    private void Start()
    {
        player = GameManager.Instance.Player.GetComponent<Player>();

        descriptionString = enhanceType switch
        {
            EnhanceType.Attack => player.Damage.ToString(),
            EnhanceType.Health => player.MaxHealth.ToString(),
            EnhanceType.AttackSpeed => player.AttackSpeed.ToString(),
            _ => descriptionString
        };

        UpdateAll();
    }

    public void EnhancePlayerHealth()
    {
        if (player.Money < cost) return;
        
        level++;
        UpdateLevel();
        player.IncreaseHealth(increasingMultiplier);
        player.SpendMoney(cost);

        descriptionString = player.MaxHealth.ToString();
        UpdateDescription();

        IncreaseCost();
    }

    public void EnhancePlayerDamage()
    {
        if (player.Money < cost) return;
        
        level++;
        UpdateLevel();
        player.IncreaseDamage(increasingMultiplier);
        player.SpendMoney(cost);

        descriptionString = player.Damage.ToString();
        UpdateDescription();
        
        IncreaseCost();
    }

    public void EnhancePlayerAttackSpeed()
    {
        if (player.Money < cost) return;
        
        level++;
        UpdateLevel();
        player.IncreaseAttackSpeed(increasingMultiplier);
        player.SpendMoney(cost);

        descriptionString = player.AttackSpeed.ToString();
        UpdateDescription();
        
        IncreaseCost();
    }

    public void IncreaseCost()
    {
        cost *= costMultiplier;
        UpdateCostText();;
    }

    public void UpdateCostText()
    {
        costText.text = cost.ToString();
    }

    public void UpdateName()
    {
        nameText.text = nameString;
    }

    public void UpdateLevel()
    {
        levelText.text = "lvl " + level;
    }

    public void UpdateDescription()
    {
        descriptionText.text = descriptionString;
    }

    public void UpdateAll()
    {
        UpdateCostText();
        UpdateName();
        UpdateLevel();
        UpdateDescription();
    }

    public enum EnhanceType
    {
        Attack,
        Health,
        AttackSpeed
    }
}
