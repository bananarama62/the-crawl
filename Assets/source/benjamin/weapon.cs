using UnityEngine;
public class weapon : MonoBehaviour
{
    public float damage = 1;

    public void BoostDamage(float multiplier)
    {
            damage += multiplier;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (collision is BoxCollider2D)
        {
            enemy.TakeDamage(damage);
        }
    }
}

