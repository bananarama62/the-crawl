using UnityEngine;

public class SkeletonArcher : EnemyController
{
    [SerializeField] private GameObject projectilePrefab;   // Prefab for the arrow projectile
    [SerializeField] private float shootInterval = 2f;      // Time interval between shots
    [SerializeField] private float followSpeedMultiplier = 0.5f; // Speed multiplier when following the player
    [SerializeField] private AudioClip attackSound;         // Reference to the attack sound

    [SerializeField] private int health = 3;                // Archer HP
    [SerializeField] private float moveSpeed = 2f;          // Movement speed

    private AudioSource audioSource;
    private float shootTimer;
    private Animator animator;

    protected override void Awake()
    {
        // 1) Initialize health & speed
        initHealthAndSpeed(health, speed: moveSpeed);

        // 2) Set up movement/health bar
        initMovement();

        // 3) Get animator
        animator = GetComponent<Animator>();

        // 4) Get or create AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    public override void decideMove()
    {
        float originalSpeed = getSpeed();

        if (Player == null)
        {
            animator.SetBool("isChasing", false);
            animator.SetBool("isAttacking", false);
            MoveVec = Vector2.zero;
            return;
        }

        if (playerInSight)
        {
            // Stop to shoot
            setSpeed(0f);
            shootTimer -= Time.deltaTime;

            animator.SetBool("isChasing", false);

            if (shootTimer <= 0f)
            {
                animator.SetBool("isAttacking", true);
                shootTimer = shootInterval;
            }
        }
        else
        {
            // Follow player slowly
            setSpeed(originalSpeed * followSpeedMultiplier);
            FollowPlayer(); // sets MoveVec based on Player

            animator.SetBool("isChasing", true);
            animator.SetBool("isAttacking", false);
        }

        // Flip sprite based on player's position
        var sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.flipX = Player.position.x < transform.position.x;
        }
    }

    public override void TakeDamage(float damage)
    {
        int intDamage = Mathf.Max(1, Mathf.RoundToInt(damage));
        int newHealth = modifyHealth(-intDamage);

        if (healthBar != null)
            healthBar.value = newHealth;

        animator.SetInteger("health", newHealth);

        if (newHealth > 0)
        {
            animator.SetTrigger("Hurt");
        }
        else
        {
            animator.SetTrigger("Die");
        }
    }

    public override void die()
    {
        // Stop AI logic
        this.enabled = false;

        // Stop physics movement
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            // or: rb.simulated = false;
        }

        // Disable colliders
        foreach (var c in GetComponents<Collider2D>())
            c.enabled = false;

        // Allow death animation to play out
        Destroy(gameObject, 2f); // whatever your Skeleton Die length is
    }

    public void FireArrow()
    {
        if (projectilePrefab != null && Player != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            Vector2 lastPlayerPosition = Player.position;
            Vector2 direction = (lastPlayerPosition - rb.position).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
            if (projectileRb != null)
            {
                projectileRb.linearVelocity = direction * 5f;
            }

            // Play attack sound
            if (attackSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(attackSound);
            }
        }

        animator.SetBool("isAttacking", false);
    }
}