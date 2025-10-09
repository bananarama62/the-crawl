using UnityEngine;
public class weapon : MonoBehaviour
{
    public float damage = 1;
    void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyController enemy = collision.GetComponent<EnemyController>();
        if (collision is BoxCollider2D)
        {
            enemy.TakeDamage(damage);
        }
    }
}
