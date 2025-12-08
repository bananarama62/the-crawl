using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
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
    string healthBarPath = "Canvas/HealthBar"; // Path to the health bar in the hierarchy

    // Movement-related variables
    protected Vector2 TargetPosition;
    protected Vector2 MoveVec;

    private NavMeshAgent NavAgent;

    // Initializes movement-related components and health bar
    public void initMovement()
    {
        rb = GetComponent<Rigidbody2D>();
        NavAgent = GetComponent<NavMeshAgent>();
        NavAgent.updateRotation = false;
        NavAgent.updateUpAxis = false;

        // Locate and initialize the health bar
        Transform t = transform.Find(healthBarPath);
        Assert.NotNull(t);
        healthBar = t.GetComponent<Slider>();

        healthBar.maxValue = getMaxHealth();
        healthBar.minValue = 0;
        healthBar.value = getCurrentHealth();
    }

    // Moves the enemy based on the calculated movement vector
    public virtual void move()
    {
        rb.MovePosition(rb.position + (MoveVec * getSpeed() * Time.fixedDeltaTime));
    }

    // Calculates movement vector to follow the player if in sight
    protected void FollowPlayer()
    {
        //MoveVec = Vector2.zero;
        if (playerInSight)
        {
            TargetPosition = Player.position;
            //MoveVec = TargetPosition.normalized;
            NavAgent.SetDestination(TargetPosition);
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

    // Detects when the player exits the enemy's trigger collider
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInSight = false;
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
        healthBar.value = modifyHealth((int)(-1 * damage));
    }
}