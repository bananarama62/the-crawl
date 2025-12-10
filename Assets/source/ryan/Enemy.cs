//using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : Character
{
    // Indicates if the player is in sight
    protected bool playerInSight = false;

    // References to the player's Rigidbody2D and the enemy's Rigidbody2D
    protected Rigidbody2D Player;
    protected Rigidbody2D rb;

    // Health bar UI element
    public Slider healthBar;
    // If you assign the Slider in the Inspector, you can remove this path
    string healthBarPath = "Canvas/healthbar"; // Path to the health bar in the hierarchy

    // Movement-related variables
    protected Vector2 TargetPosition;
    protected Vector2 MoveVec;

    private void Start()
    {
        initMovement(); // Call initMovement at the start
    }

    // Initializes movement-related components and health bar
    public void initMovement()
    {
        rb = GetComponent<Rigidbody2D>();
        //Assert.NotNull(rb, "Enemy requires a Rigidbody2D.");

        // If you prefer to assign healthBar in Inspector, you can skip this lookup
        // if (healthBar == null)
        // {
        //     Transform t = transform.Find(healthBarPath);
        //     //Assert.NotNull(t, $"HealthBar not found at path '{healthBarPath}'");
        //     healthBar = t.GetComponent<Slider>();
        // }
        if (healthBar != null)
        {
            healthBar.maxValue = getMaxHealth();
            healthBar.minValue = 0;
            healthBar.value = getCurrentHealth();
        }
    }

    // Moves the enemy based on the calculated movement vector
    public virtual void move()
    {
        if (rb == null) return;

        rb.MovePosition(rb.position + (MoveVec * getSpeed() * Time.fixedDeltaTime));
    }

    // Simple "chase the player" logic without NavMesh
    protected void FollowPlayer()
    {
        if (playerInSight && Player != null)
        {
            TargetPosition = Player.position;
            Vector2 dir = (TargetPosition - rb.position);

            if (dir.sqrMagnitude > 0.0001f)
                MoveVec = dir.normalized;
            else
                MoveVec = Vector2.zero;
        }
        else
        {
            MoveVec = Vector2.zero;
        }
    }

    // Decides the movement behavior of the enemy
    public virtual void decideMove()
    {
        FollowPlayer();
    }

    // Detects when the player enters the enemy's trigger collider
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player = collision.attachedRigidbody;
            playerInSight = true;
        }
    }

    // Detects when the player exits the enemy's trigger collider
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInSight = false;
            Player = null;
        }
    }

    // Handles collision with the player and triggers the attack
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Attack(collision.collider.GetComponent<PlayerController>());
        }
    }

    // Continuously attacks the player while in collision
    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Attack(collision.collider.GetComponent<PlayerController>());
        }
    }

    // Default attack method to be overridden by derived classes
    protected virtual void Attack(PlayerController player)
    {
        // Default implementation (can be empty or generic)
    }

    // Handles damage taken by the enemy and updates the health bar
    public virtual void TakeDamage(float damage)
    {
        int intDamage = Mathf.Max(1, Mathf.RoundToInt(damage));
        int newHealth = modifyHealth(-intDamage);

        if (healthBar != null)
        {
            healthBar.value = newHealth;
        }
    }
}
