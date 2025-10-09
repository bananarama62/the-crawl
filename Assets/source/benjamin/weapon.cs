using UnityEngine;
public class weapon : MonoBehaviour
{
    public float damage = 1;
    void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (collision is BoxCollider2D)
        {
            enemy.TakeDamage(damage);
        }
    }
}
