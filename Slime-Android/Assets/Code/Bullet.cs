using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private bool shooted;
    private Rigidbody rb;

    public float bulletSpeed;
    public Transform target;

    private float damage;
    public float Damage => damage;

    public PlayerShoot playerShoot;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.velocity = (target.position + Vector3.up * 0.5f - transform.position) * bulletSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            collision.collider.GetComponent<Enemy>().GetDamage(damage);
        }
        
        gameObject.SetActive(false);
    }

    public void ChangeDamage(float newDamage)
    {
        damage = newDamage;
    }
}
