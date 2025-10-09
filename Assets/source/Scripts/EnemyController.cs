using UnityEngine;

public class EnemyController : MonoBehaviour
{
    bool ChasePlayer = false;
    Rigidbody2D Player;
    Rigidbody2D rb;
    float health = 3;
    float maxHealth = 3;
    float Speed = 1;
    Vector2 TargetPosition;
    Vector2 MoveVec;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        MoveVec = Vector2.zero;
        if (ChasePlayer)
        {
            TargetPosition = Player.position - rb.position;
            MoveVec = TargetPosition.normalized;
            rb.MovePosition(rb.position + (MoveVec * Speed * Time.fixedDeltaTime));
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player = collision.attachedRigidbody;
            ChasePlayer = true;
        }
    }

   void OnTriggerExit2D(Collider2D collision)
   {
        if (collision.CompareTag("Player"))
        {
            ChasePlayer = false;
        }
   }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
