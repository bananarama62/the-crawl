using UnityEngine;

/**
    * Arrow class handles the behavior of the arrow projectile.
*/
public class Arrow : MonoBehaviour
{
    // Speed at which the arrow travels
    public float Speed;
    public float Duration;
    // Damage dealt by the arrow upon impact
    public int damage;  
    public string Owner = "Player";

    // Reference to the Rigidbody2D component for physics calculations
    Rigidbody2D rb;

    // Initialize the Rigidbody2D component     
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null) rb = gameObject.AddComponent<Rigidbody2D>();
    }
    // Method to fire the arrow in a specified direction
    public void Fire(Quaternion aim)
    {
        transform.rotation = aim;
        Vector3 dir = transform.right;
        rb.linearVelocity = dir * Speed;
    }
    // Handle collision detection and damage application
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Ignore collisions with the owner of the arrow
        if (collision.gameObject.CompareTag(Owner))
        {
            return;
        }   
        
        if (collision.gameObject.name.Contains("Walls"))
        {
            Destroy(gameObject);
            return;
        }

        // Apply damage to player or enemy based on the owner of the arrow
        Enemy enemy = collision.GetComponent<Enemy>();
        if (collision is BoxCollider2D)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
