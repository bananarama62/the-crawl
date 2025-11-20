
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
// Manages the player's inventory and hotbar functionality
public class PlayerInventory : MonoBehaviour
{
    // List to hold the player's hotbar items
    public List<HotBar> InventoryList;
    public int SelectedItem = 0;

    // Input action for hotbar selection
    InputAction PlayerHotbar;
    // Possible Weapons
    [Header("Weapon Game Objects")]
    [SerializeField] GameObject sword;
    [SerializeField] GameObject dagger;
    [SerializeField] GameObject bow;
    [SerializeField] GameObject spear;
    [SerializeField] private PlayerController player;
    // Dictionary to map hotbar slots to their corresponding GameObjects
    private Dictionary<HotBarSlot, GameObject> itemSetActive = new Dictionary<HotBarSlot, GameObject>();

    // Enum to differentiate between weapon and item types
    private enum Type { Weapon, Item }
    private class HotbarEntry
    {
        public Type Type;
        public HotBar Weapon;
        public Item Item;
        public Sprite Icon => Weapon != null ? Weapon.Icon : (Item != null ? Item.Icon : null);
    }

    // List to hold filled hotbar slots
    private List<HotbarEntry> FilledSlots = new List<HotbarEntry>();
    private const int HotbarSize = 3;
    
    // Initialize the player's inventory and hotbar
    void Awake()
    {
        if (InventoryList == null)
        {
            InventoryList = new List<HotBar>();
        }
        FilledSlots.Clear();
        for (int i = 0; i < InventoryList.Count; i++)
        {
            var hb = InventoryList[i];
            if (hb != null)
            {
                FilledSlots.Add(new HotbarEntry { Type = Type.Weapon, Weapon = hb });
            }

            if (FilledSlots.Count >= HotbarSize)
            {
                break;
            }
        }

        while (InventoryList.Count < HotbarSize)
        {
            InventoryList.Add(null);
        }
        if (FilledSlots.Count == 0)
        {
            SelectedItem = 0;
        }
        else if (SelectedItem < 0 || SelectedItem >= FilledSlots.Count)
        {
            SelectedItem = 0;
        }
        PlayerHotbar = InputSystem.actions.FindAction("HotBar");

        if (PlayerHotbar != null)
        {
            PlayerHotbar.Enable();
        }

        itemSetActive.Clear();

        if (sword != null)
        {
            itemSetActive[HotBarSlot.sword] = sword;
        }
        if (dagger != null)
        {
            itemSetActive[HotBarSlot.dagger] = dagger;
        }
        if (bow != null)
        {
            itemSetActive[HotBarSlot.bow] = bow;
        }
        if (spear != null)
        {
            itemSetActive[HotBarSlot.spear] = spear;
        }

        if (player == null)
        {
            player = Object.FindAnyObjectByType<PlayerController>();
        }

        if (sword != null)
        {
            sword.SetActive(false);
        }
        if (dagger != null)
        {
            dagger.SetActive(false); 
        }

        if (bow != null)
        {
            bow.SetActive(false);
        }
        if (spear != null)
        {
            spear.SetActive(false);
        }

        UpdateUI();
        ItemSelected();
    }
    // If the player presses the hotbar key, cycle to the next item
    void Update()
    {
        if (PlayerHotbar == null)
        {
            return;
        }
        if (FilledSlots == null || FilledSlots.Count <= 1)
        {
            return;
        }
        if (PlayerHotbar.WasPressedThisFrame())
        {
            SelectedItem = (SelectedItem + 1) % InventoryList.Count;
            ItemSelected();
        }
    }
    // Cycle to the next hotbar item
    public void Next()
    {
        if (FilledSlots == null || FilledSlots.Count <= 1)
        {
            return;
        }
        SelectedItem = (SelectedItem + 1) % FilledSlots.Count;
        ItemSelected();
    }

    // Check if the player has a specific weapon in their hotbar
    public bool HasWeapon(HotBarSlot slot)
    {
        for (int i = 0; i < FilledSlots.Count; i++)
        {
            var h = FilledSlots[i];
            if (h != null && h.Type == Type.Weapon && h.Weapon.ItemType == slot)
            {
                return true;
            }
        }
        return false;
    }

    // Check if the player has a specific item in their hotbar
    public bool HasItem(Item item)
    {
        for (int i = 0; i < FilledSlots.Count; i++)
        {
            var e = FilledSlots[i];
            if (e.Type == Type.Item && e.Item == item)
            {
                return true;
            }
        }
        return false;
    }

    // Add a weapon to the player's hotbar
    public void AddHotbarWeapon(HotBar hotbarEntry)
    {
        if (hotbarEntry == null)
        {
            return;
        }
        // Check if the weapon is already in the hotbar
        for (int i = 0; i < FilledSlots.Count; i++)
        {
            var h = FilledSlots[i];
            if (h != null && h.Type == Type.Weapon && h.Weapon.ItemType == hotbarEntry.ItemType)
            {
                return;
            }
        }
        // Add the weapon to the hotbar if there's space, otherwise replace the selected item
        if (FilledSlots.Count < HotbarSize)
        {
            FilledSlots.Add(new HotbarEntry { Type = Type.Weapon, Weapon = hotbarEntry });
            if (FilledSlots.Count == 1)
            {
                SelectedItem = 0;
                ItemSelected();
            }
        }
        else
        {
            FilledSlots[SelectedItem] = new HotbarEntry { Type = Type.Weapon, Weapon = hotbarEntry };
            ItemSelected();
        }
        UpdateUI();
    }
    // Add an item to the player's hotbar
    public void AddHotbarItem(Item hotbarEntry)
    {
        if (hotbarEntry == null)
        {
            return;
        }
        // Check if the item is already in the hotbar
        for (int i = 0; i < FilledSlots.Count; i++)
        {
            var e = FilledSlots[i];
            if (e.Type == Type.Item && e.Item == hotbarEntry)
            {
                return;
            }
        }
        // Add the item to the hotbar if there's space, otherwise replace the selected item
        if (FilledSlots.Count < HotbarSize)
        {
            FilledSlots.Add(new HotbarEntry { Type = Type.Item, Item = hotbarEntry });
            if (FilledSlots.Count == 1)
            { 
                SelectedItem = 0; ItemSelected(); 
            }
        }
        else
        {
            FilledSlots[SelectedItem] = new HotbarEntry { Type = Type.Item, Item = hotbarEntry };
            ItemSelected();
        }
        UpdateUI();
    }
    // Consume (remove) an item from the player's hotbar
    public void ConsumeHotbarItem(Item item)
    {
        if (item == null || FilledSlots == null || FilledSlots.Count == 0)
        {
            return;
        }
        if (SelectedItem >= 0 && SelectedItem < FilledSlots.Count)
        {
            var current = FilledSlots[SelectedItem];
            if (current != null && current.Type == Type.Item && current.Item == item)
            {
                FilledSlots.RemoveAt(SelectedItem);
                if (player != null && player.HotBarItem == item)
                {
                    player.HotBarItem = null;
                }

                if (FilledSlots.Count == 0)
                {
                    SelectedItem = 0;
                }
                else if (SelectedItem >= FilledSlots.Count)
                {
                    SelectedItem = FilledSlots.Count - 1;
                }
                ItemSelected();
                UpdateUI();
                return;
            }
        }
    }
    // Handle the selection of an item in the hotbar
    private void ItemSelected()
    {
        // Handle item selection in the hotbar
        if(FilledSlots == null || FilledSlots.Count == 0)
        {
            return;
        }
        if(SelectedItem < 0 || SelectedItem >= FilledSlots.Count)
        {
            SelectedItem = 0;
        }

        if (sword != null)
        {
            sword.SetActive(false);
        }
        if (dagger != null)
        {
            dagger.SetActive(false);
        }
        if (bow != null)
        {
            bow.SetActive(false);
        }
        if (spear != null)
        {
            spear.SetActive(false);
        }
        UpdateUI();
        // Handle item selection in the hotbar
        var hotbarEntry = FilledSlots[SelectedItem];
        if (hotbarEntry.Type == Type.Weapon)
        {
            var WeaponEntry = hotbarEntry.Weapon;
            if (WeaponEntry == null)
            {
                return;
            }
            GameObject GO = itemSetActive[WeaponEntry.ItemType];
            GO.SetActive(true);
            weapon W = GO.GetComponent<weapon>();
            if (W != null)
            {
               W.icon = WeaponEntry.Icon;
                player.EquipWeapon(W);
            }
        }
        else
        {
            var ItemEntry = hotbarEntry.Item;
            if (ItemEntry == null)
            {
                return;
            }
            player.EquipWeapon(null);
            player.HotBarItem = ItemEntry;
        }
        // Update the UI icon for the selected item
        if (UIHandler.instance != null)
        {
            UIHandler.instance.setIcon(SelectedItem + 1, hotbarEntry.Icon);
        }
    }
    // Update the hotbar UI to reflect the current items
    private void UpdateUI()
    {
        // Update the hotbar UI to reflect the current items
        if (UIHandler.instance == null)
        {
            return;
        }
        for (int i = 0; i < HotbarSize; i++)
        {
            Sprite icon = null;
            if (i < FilledSlots.Count && FilledSlots[i] != null)
            {
                icon = FilledSlots[i].Icon;
            }

            UIHandler.instance.setIcon(i + 1, icon);
        }
    }
}