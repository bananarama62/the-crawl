using UnityEngine;
public class weapon : MonoBehaviour
{
    public float damage = 1;
    private Cooldown cooldown;
    [SerializeField] private Effect effect;
    
    public int use(){
        return effect.use();
    }

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

