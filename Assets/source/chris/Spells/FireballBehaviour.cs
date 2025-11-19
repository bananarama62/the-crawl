using UnityEngine;
/// <summary>
/// Author: Christopher Soto
/// Date: November 14, 2025
/// This file stores Fireball behavior used by fireball on the player when using the mage class
/// </summary>
public class FireballBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    private float BallSpeed = 15f;
    private float damage = 5;
    [SerializeField] private LayerMask WhatDestroysBall;
    /// <summary>
    /// collects necessary components used by fireball and sets fireball speed and destroy time so fireball can disappear if it misses
    /// </summary>
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        setDestroyTime();
        setVelocity();
    }
    /// <summary>
    /// when fireball collides with enemy box collider, destroys fireball and deals damage to enemy
    /// </summary>
    /// <param name="collision"> collects collision parameters from target</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if ((WhatDestroysBall.value & (1 << collision.gameObject.layer)) > 0)
        {
            Destroy(gameObject);
        }
        if (collision is BoxCollider2D)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// sets fireball velocity when spawned
    /// </summary>
    private void setVelocity()
    {
        rb.linearVelocity = transform.right * BallSpeed;
    }
    /// <summary>
    /// destroys fireball if it misses after a set time
    /// </summary>
    private void setDestroyTime()
    {
        Destroy(gameObject,3f);
    }
    /// <summary>
    /// test function for test file
    /// </summary>
    /// <param name="enemy"> passed by test file</param>
    public void TestDamage(Enemy enemy)
    {
        enemy.TakeDamage(damage);
    }
    /// <summary>
    /// sets fireball damage, used by test file
    /// </summary>
    /// <param name="damage1"> passed by test file</param>
    public void setDamage(float damage1)
    {
        damage = damage1;
    }
}
