using UnityEngine;
/// <summary>
/// Author: Christopher Soto
/// Date: November 14, 2025
/// This file stores trap behavior used by ArcherTrap on the player when using the archer class
/// </summary>
public class TrapBehavior : MonoBehaviour
{
    private Rigidbody2D rb;
    private float damage = 5;
    [SerializeField] private AudioClip clip;
    /// <summary>
    /// collects necessary components used by trap and sets trap destroy time so trap can disappear after a set time
    /// </summary>
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        setDestroyTime();
    }
    /// <summary>
    /// when trap collides with enemy box collider, destroys trap, play sound and deals damage to enemy
    /// </summary>
    /// <param name="collision"> collects collision parameters from target</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (collision is BoxCollider2D && collision.CompareTag("Enemy"))
        {
            AudioSource.PlayClipAtPoint(clip, transform.position);
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// destroys trap after a set time
    /// </summary>
    private void setDestroyTime()
    {
        Destroy(gameObject,9f);
    }
    /// <summary>
    /// test function for test file
    /// </summary>
    /// <param name="enemy"> passed by test file</param>
    public void TestDamage(Enemy enemy)
    {
        enemy.TakeDamage(damage);
    }
    /// <summary>
    /// sets fireball damage, used by test file
    /// </summary>
    /// <param name="damage1"> passed by test file</param>
    public void setDamage(float damage1)
    {
        damage = damage1;
    }
}
