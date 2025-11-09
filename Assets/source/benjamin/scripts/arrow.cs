    using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float Speed;
    public float Duration;
    public int damage;  
    public string Owner = "Player";

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null) rb = gameObject.AddComponent<Rigidbody2D>();
    }
    public void Fire(Quaternion aim)
    {
        transform.rotation = aim;
        Vector3 dir = transform.right;
        rb.linearVelocity = dir * Speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Owner))
        {
            return;
        }

        Enemy enemy = collision.GetComponent<Enemy>();
        if (collision is BoxCollider2D)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
