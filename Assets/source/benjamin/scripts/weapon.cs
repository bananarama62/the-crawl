using UnityEngine;
public class weapon : MonoBehaviour
{
    public float damage = 1;
    private Cooldown cooldown;
    [SerializeField] private Effect effect;
    public Sprite icon;
    public string Caster;

    public AudioClip swingSound;
    public int use(){
        PlaySfx();
        return effect.use();
    }
    public void PlaySfx(){
        AudioSource.PlayClipAtPoint(swingSound, transform.position);
    }
    public void BoostDamage(float multiplier)
    {
            damage += multiplier;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
       if(Caster != "Player" && collision.CompareTag("Player")){
          collision.GetComponent<PlayerController>().takeDamage(damage);
       } else if(Caster != "Enemy" && collision.CompareTag("Enemy")){
           Enemy enemy = collision.GetComponent<Enemy>();
           if (collision is BoxCollider2D)
           {
               enemy.TakeDamage(damage);
           }
       }
    }
}

