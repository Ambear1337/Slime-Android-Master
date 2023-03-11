using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    [FormerlySerializedAs("bulletSpeed")] public float bulletInitialSpeed = 10f;
    public float bulletCurveMagnitude = 1f;
    public float bulletCurveFrequency = 2f;
    public float shootCooldown = 1f;
    public int bulletPoolSize = 10;

    public GameObject[] enemies;

    private List<GameObject> bulletPool = new List<GameObject>();
    private bool canShoot = true;

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        
        shootCooldown = 1 / ((100 + player.AttackSpeed) * 0.01f / 1.7f);
        
        // Populate bullet pool
        for (int i = 0; i < bulletPoolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
        
        ChangeBulletsDamage(player.Damage);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.RoundManager.RoundStarted) return;

        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        Shoot();
    }

    private Transform FindClosestEnemy()
    {
        Transform closestEnemy = null;
        float distance = 100f;

        foreach (var enemy in enemies)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) < distance)
            {
                distance = Vector3.Distance(transform.position, enemy.transform.position);
                closestEnemy = enemy.transform;
            }
        }

        return closestEnemy;
    }

    public void ChangeBulletsDamage(float newDamage)
    {
        foreach (var bul in bulletPool)
        {
            bul.GetComponent<Bullet>().ChangeDamage(newDamage);
        }
    }

    private void Shoot()
    {
        var closestEnemy = FindClosestEnemy();

        if (closestEnemy == null || !canShoot) return;

        var bullet = GetInactiveBullet();

        if (bullet == null) return;

        bullet.transform.position = transform.position + Vector3.up * 0.5f;
        bullet.SetActive(true);
        bullet.GetComponent<Bullet>().target = closestEnemy;

        canShoot = false;
        Invoke("ResetShootCooldown", shootCooldown);
    }

    // Get inactive bullet from pool
    private GameObject GetInactiveBullet()
    {
        foreach (GameObject bullet in bulletPool)
        {
            if (!bullet.activeInHierarchy)
            {
                return bullet;
            }
        }

        return null;
    }

    public void ChangeShootCooldown()
    {
        shootCooldown = 1 / (((100 + player.AttackSpeed) * 0.01f) / 1.7f);
    }

    // Reset shoot cooldown
    private void ResetShootCooldown()
    {
        canShoot = true;
    }
}