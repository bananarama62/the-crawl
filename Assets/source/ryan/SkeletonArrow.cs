using UnityEngine;

public class SkeletonArrow : MonoBehaviour
{
    public float damage = 2f; // Damage dealt by the arrow

    // Handles collision with the player and applies damage
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object is the player
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.takeDamage(damage);
            }

            // Destroy the arrow after hitting the player
            Destroy(gameObject);
        }
        else if (collision.gameObject.name.ToLower().Contains("walls"))
        {
            // Destroy the arrow if it collides with walls
            Destroy(gameObject);
        }
    }

    // Handles collision with walls and destroys the arrow
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object's name contains "walls"
        if (collision.gameObject.name.ToLower().Contains("walls"))
        {
            // Destroy the arrow game object
            Destroy(gameObject);
        }
    }
}