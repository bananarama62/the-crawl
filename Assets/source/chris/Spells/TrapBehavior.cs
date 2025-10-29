using UnityEngine;

public class TrapBehavior : MonoBehaviour
{
    private Rigidbody2D rb;
    private float BallSpeed = 15f;
    private float damage = 5;
    [SerializeField] private LayerMask WhatDestroysTrap;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        setDestroyTime();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (collision is BoxCollider2D && collision.CompareTag("Enemy"))
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
    private void setDestroyTime()
    {
        Destroy(gameObject,9f);
    }
}
