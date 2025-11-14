using UnityEngine;

public class Orc : EnemyController
{
    [SerializeField] private int damage = 2;
    [SerializeField] private int health = 5;
    [SerializeField] private float attackCooldown = 2f; // Time interval between attacks
    [SerializeField] private float attackDelay = 0.5f; // Delay before dealing damage during attack animation
    private Animator animator;
    private float attackTimer = 0f;
    private float attackDelayTimer = 0f; // Timer for attack delay
    private bool isAttacking = false;

    protected override void Awake()
    {
        initHealthAndSpeed(health, speed: 1);

        initMovement();

        animator = GetComponent<Animator>();
    }

    public override void decideMove()
    {
        if (playerInSight && !isAttacking)
        {
            animator.SetBool("isChasing", true);
            FollowPlayer();
        }
        else
        {
            animator.SetBool("isChasing", false);
            MoveVec = Vector2.zero;
        }
    }

    void Update()
    {
        // Update the attack cooldown timer
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }

        // Update the attack delay timer
        if (isAttacking)
        {
            attackDelayTimer -= Time.deltaTime;

            // Check if the delay timer has expired
            if (attackDelayTimer <= 0)
            {
                DealDamage();
                isAttacking = false; // Reset attacking state
            }
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision); // Call base method for collision detection
    }

    protected override void OnCollisionStay2D(Collision2D collision)
    {
        base.OnCollisionStay2D(collision); // Call base method for collision detection
    }

    protected override void Attack(PlayerController player)
    {
        if (player != null && attackTimer <= 0 && !isAttacking)
        {
            // Trigger the "Attack" animation
            animator.SetTrigger("Player");

            // Start the attack delay timer
            attackDelayTimer = attackDelay;
            isAttacking = true;

            // Reset the attack cooldown timer
            attackTimer = attackCooldown;
        }
    }

    private void DealDamage()
    {
        if (Player != null)
        {
            PlayerController player = Player.GetComponent<PlayerController>();
            if (player != null)
            {
                player.takeDamage(damage);
            }
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        animator.SetBool("damageTaken", true);

        animator.SetInteger("health", getCurrentHealth());

        if (getCurrentHealth() <= 0)
        {
            animator.SetTrigger("Die");
        }

        animator.SetBool("damageTaken", false);
    }
}