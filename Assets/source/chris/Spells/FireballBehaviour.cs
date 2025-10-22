using UnityEngine;

public class FireballBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    private float ballSpeed = 15f;
    [SerializeField] private LayerMask whatDestroysBall;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        setDestroyTime();
        setVelocity();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((whatDestroysBall.value & (1 << collision.gameObject.layer)) > 0)
        {
            Destroy(gameObject);
        }
    }
    private void setVelocity()
    {
        rb.linearVelocity = transform.right * ballSpeed;
    }

    private void setDestroyTime()
    {
        Destroy(gameObject,3f);
    }
}
