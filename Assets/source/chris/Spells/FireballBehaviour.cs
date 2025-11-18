using UnityEngine;

public class FireballBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    private float BallSpeed = 15f;
    private float damage = 5;
    [SerializeField] private LayerMask WhatDestroysBall;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        setDestroyTime();
        setVelocity();
    }
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
    private void setVelocity()
    {
        rb.linearVelocity = transform.right * BallSpeed;
    }

    private void setDestroyTime()
    {
        Destroy(gameObject,3f);
    }

    public void TestDamage(Enemy enemy)
    {
        enemy.TakeDamage(damage);
    }

    public void setDamage(float damage1)
    {
        damage = damage1;
    }
}
