using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Assign one of these in inspector: weapon HotBar or consumable Item
    public HotBar WeaponHotBar;
    public Item ItemPickup;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }
        var Inventory = collision.GetComponent<PlayerInventory>();
        if (Inventory == null)
        {
            Debug.LogWarning("PlayerInventory not found on player.");
            Destroy(gameObject);
            return;
        }

        if (WeaponHotBar != null)
        {
            Inventory.AddHotbarWeapon(WeaponHotBar);
        }
        else if (ItemPickup != null)
        {
            Inventory.AddHotbarItem(ItemPickup);
        }

        Destroy(gameObject);
    }
}