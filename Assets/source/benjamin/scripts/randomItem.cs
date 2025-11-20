using System.Collections.Generic;
using UnityEngine;

/**
    * randomItem class gives the player a random item or weapon from predefined lists upon pickup.
*/
public class randomItem : MonoBehaviour
{
    // Lists of possible items and weapons to give to the player
    [Tooltip("Consumable Items")]
    public Item[] possibleItems;

    [Tooltip("Weapons")]
    public HotBar[] possibleWeapons;
    // Struct to represent a candidate item or weapon
    private struct Candidate
    {
        public Item item;
        public HotBar weapon;

        public bool IsItem
        {
            get { return item != null; }
        }

        public bool IsWeapon
        {
            get { return weapon != null; }
        }
    }
    // Method called when another collider enters the trigger collider attached to this object
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }
        // Get the PlayerInventory component from the player
        var inventory = collision.GetComponent<PlayerInventory>();

        // Build a list of candidate items and weapons that the player does not already have
        var candidates = new List<Candidate>();

        // Check possible items and weapons
        if (possibleItems != null)
        {
            for (int i = 0; i < possibleItems.Length; i++)
            {
                Item it = possibleItems[i];
                if (it == null)
                {
                    continue;
                }
                if (!inventory.HasItem(it))
                {
                    Candidate c = new Candidate();
                    c.item = it;
                    candidates.Add(c);
                }
            }
        }
        if (possibleWeapons != null)
        {
            for (int i = 0; i < possibleWeapons.Length; i++)
            {
                HotBar hw = possibleWeapons[i];
                if (hw == null)
                {
                    continue;
                }
                if (!inventory.HasWeapon(hw.ItemType))
                {
                    Candidate c = new Candidate();
                    c.weapon = hw;
                    candidates.Add(c);
                }
            }
        }
        
        // If no candidates are available, do nothing
        if (candidates.Count == 0)
        {
            return;
        }   
        // Randomly select one candidate and add it to the player's inventory
        var chosen = candidates[Random.Range(0, candidates.Count)];
        if (chosen.IsItem)
        {
            inventory.AddHotbarItem(chosen.item);
            Debug.Log($"randomItem: gave item {chosen.item.name}");
        }
        else if (chosen.IsWeapon)
        {
            inventory.AddHotbarWeapon(chosen.weapon);
            Debug.Log($"randomItem: gave weapon {chosen.weapon.name}");
        }
        
        // Destroy the pickup object after giving the item or weapon

        Destroy(gameObject);
    }
}
