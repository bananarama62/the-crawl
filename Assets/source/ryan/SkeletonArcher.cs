using UnityEngine;

public class SkeletonArcher : EnemyController
{
    [SerializeField] private GameObject projectilePrefab; // Prefab for the arrow projectile
    [SerializeField] private float shootInterval = 2f; // Time interval between shots
    [SerializeField] private float followSpeedMultiplier = 0.5f; // Speed multiplier when following the player
    [SerializeField] private AudioClip attackSound; // Reference to the attack sound
    private AudioSource audioSource; // AudioSource component
    private float shootTimer;
    private Animator animator;

    // Initializes movement, animator, and audio source
    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();

        // Initialize AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Decides movement and behavior based on player's position
    public override void decideMove()
    {
        float originalSpeed = getSpeed();

        if (playerInSight && Player != null)
        {
            setSpeed(0);
            shootTimer -= Time.deltaTime;

            animator.SetBool("isChasing", false);

            if (shootTimer <= 0)
            {
                if (!animator.GetBool("isAttacking"))
                {
                    animator.SetBool("isAttacking", true);
                }

                shootTimer = shootInterval;
            }

            // Flip sprite based on player's position
            if (Player.position.x < transform.position.x)
            {
                GetComponent<SpriteRenderer>().flipX = true; // Face left
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false; // Face right
            }
        }
        else if (Player != null)
        {
            setSpeed(originalSpeed * followSpeedMultiplier);
            FollowPlayer();

            animator.SetBool("isChasing", true);
            animator.SetBool("isAttacking", false);

            // Flip sprite based on player's position
            if (Player.position.x < transform.position.x)
            {
                GetComponent<SpriteRenderer>().flipX = true; // Face left
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false; // Face right
            }
        }
        else
        {
            animator.SetBool("isChasing", false);
            animator.SetBool("isAttacking", false);
        }
    }

    // Handles damage taken by the Skeleton Archer and updates animations
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        animator.SetTrigger("takenDamage");

        animator.SetFloat("Health", getCurrentHealth());
    }

    // Handles death logic and triggers death animation
    public override void die()
    {
        base.die();

        animator.SetTrigger("Die");
    }

    // Fires an arrow projectile towards the player
    public void FireArrow()
    {
        if (projectilePrefab != null)
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