using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Item item;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            item.Activate(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
