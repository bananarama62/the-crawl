using UnityEngine;

/**
    * Pickup class handles the behavior when the player picks up an item or weapon.
*/
public class Pickup : MonoBehaviour
{
    // Assign one of these in inspector: weapon HotBar or consumable Item
    public HotBar WeaponHotBar;
    public Item ItemPickup;

    // Method called when another collider enters the trigger collider attached to this object
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Only respond to collisions with the player
        if (!collision.CompareTag("Player"))
        {
            return;
        }
        // Get the PlayerInventory component from the player
        var Inventory = collision.GetComponent<PlayerInventory>();
        if (Inventory == null)
        {
            Debug.LogWarning("PlayerInventory not found on player.");
            Destroy(gameObject);
            return;
        }
        // Add the weapon or item to the player's inventory
        if (WeaponHotBar != null)
        {
            Inventory.AddHotbarWeapon(WeaponHotBar);
        }
        // If it's an item pickup
        else if (ItemPickup != null)
        {
            Inventory.AddHotbarItem(ItemPickup);
        }
        // Destroy the pickup object after it has been collected
        Destroy(gameObject);
    }
}