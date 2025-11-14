using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : Character
{
    protected bool playerInSight = false;
    protected Rigidbody2D Player;
    protected Rigidbody2D rb;

    public Slider healthBar;
    string healthBarPath = "Canvas/HealthBar";
    protected Vector2 TargetPosition;
    protected Vector2 MoveVec;

    public void initMovement()
    {
        rb = GetComponent<Rigidbody2D>();

        // Path to slider
        Transform t = transform.Find(healthBarPath);
        Assert.NotNull(t);
        healthBar = t.GetComponent<Slider>();

        healthBar.maxValue = getMaxHealth();
        healthBar.minValue = 0;
        healthBar.value = getCurrentHealth();
    }

    public virtual void move()
    {
        rb.MovePosition(rb.position + (MoveVec * getSpeed() * Time.fixedDeltaTime));
    }

    protected void FollowPlayer()
    {
        MoveVec = Vector2.zero;
        if (playerInSight)
        {
            TargetPosition = Player.position - rb.position;
            MoveVec = TargetPosition.normalized;
        }
    }

    public virtual void decideMove()
    {
        FollowPlayer();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player = collision.attachedRigidbody;
            playerInSight = true;
        }
    }

    // Call the attack function in derived classes
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Attack(collision.collider.GetComponent<PlayerController>());
        }
    }

    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Attack(collision.collider.GetComponent<PlayerController>());
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInSight = false;
        }
    }

    protected virtual void Attack(PlayerController player)
    {
        // Default implementation (can be empty or generic)
    }

    public virtual void TakeDamage(float damage)
    {
        healthBar.value = modifyHealth((int)(-1 * damage));
    }
}