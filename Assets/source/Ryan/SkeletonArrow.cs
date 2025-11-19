using UnityEngine;

public class SkeletonArrow : MonoBehaviour
{
    public float damage = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.takeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}