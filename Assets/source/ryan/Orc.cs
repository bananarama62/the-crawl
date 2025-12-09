using UnityEngine;

public class Orc : EnemyController
{
    [SerializeField] private int damage = 2; // Damage dealt by the Orc
    [SerializeField] private int health = 5; // Initial health of the Orc
    [SerializeField] private float attackCooldown = 2f; // Time interval between attacks
    [SerializeField] private float attackDelay = 0.5f; // Delay before dealing damage during attack animation
    [SerializeField] private AudioClip attackSound; // Reference to the attack sound
    private AudioSource audioSource; // AudioSource component
    private Animator animator;
    private float attackTimer = 0f;
    private float attackDelayTimer = 0f; // Timer for attack delay
    private bool isAttacking = false;

    // Initializes health, movement, animator, and audio source
    protected override void Awake()
    {
        initHealthAndSpeed(health, speed: 2);

        initMovement();

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
        if (playerInSight && !isAttacking)
        {
            animator.SetBool("isChasing", true);
            FollowPlayer();

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

    // Handles collision with the player and triggers the attack
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision); // Call base method for collision detection
    }

    // Continuously attacks the player while in collision
    protected override void OnCollisionStay2D(Collision2D collision)
    {
        base.OnCollisionStay2D(collision); // Call base method for collision detection
    }

    // Executes the attack logic, including animations and sound
    protected override void Attack(PlayerController player)
    {
        if (player != null && attackTimer <= 0 && !isAttacking)
        {
            // Play attack sound
            if (attackSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(attackSound);
            }

            // Trigger the "Attack" animation
            animator.SetTrigger("Player");

            // Start the attack delay timer
            attackDelayTimer = attackDelay;
            isAttacking = true;

            // Reset the attack cooldown timer
            attackTimer = attackCooldown;
        }
    }

    // Deals damage to the player
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

    // Handles damage taken by the Orc and updates animations
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