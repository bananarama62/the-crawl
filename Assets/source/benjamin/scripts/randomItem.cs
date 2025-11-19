using System.Collections.Generic;
using UnityEngine;

public class randomItem : MonoBehaviour
{

    [Tooltip("Consumable Items")]
    public Item[] possibleItems;

    [Tooltip("Weapons")]
    public HotBar[] possibleWeapons;
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
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }
        var inventory = collision.GetComponent<PlayerInventory>();

        var candidates = new List<Candidate>();

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

        Destroy(gameObject);
    }
}
