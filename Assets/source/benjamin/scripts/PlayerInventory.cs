using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    public List<HotBar> inventoryList;
    public int selectedItem = 0;

    InputAction playerHotbar;
    [Header("Weapon Game Objects")]
    [SerializeField] GameObject sword;
    [SerializeField] GameObject dagger;
    [SerializeField] GameObject bow;
    [SerializeField] GameObject spear;
    [SerializeField] private PlayerController player;

    private Dictionary<HotBarSlot, GameObject> itemSetActive = new Dictionary<HotBarSlot, GameObject>();

    void Awake()
    {
        playerHotbar = InputSystem.actions.FindAction("HotBar");
        if (playerHotbar != null) playerHotbar.Enable();

        itemSetActive.Clear();
        if (sword != null) itemSetActive[HotBarSlot.sword] = sword;
        if (dagger != null) itemSetActive[HotBarSlot.dagger] = dagger;
        if (bow != null) itemSetActive[HotBarSlot.bow] = bow;
        if (spear != null) itemSetActive[HotBarSlot.spear] = spear;

        if (player == null) player = Object.FindAnyObjectByType<PlayerController>();

        if (sword != null) sword.SetActive(false);
        if (dagger != null) dagger.SetActive(false);
        if (bow != null) bow.SetActive(false);
        if (spear != null) spear.SetActive(false);

        if (selectedItem < 0 || selectedItem >= inventoryList.Count) selectedItem = 0;
        ItemSelected();
    }

    void Update()
    {
        if (playerHotbar == null || inventoryList == null || inventoryList.Count == 0) return;

        if (playerHotbar.WasPressedThisFrame())
        {
            selectedItem = (selectedItem + 1) % inventoryList.Count;
            ItemSelected();
        }
    }

    public void Next()
    {
        if (inventoryList == null || inventoryList.Count == 0) return;
        selectedItem = (selectedItem + 1) % inventoryList.Count;
        ItemSelected();
    }

    private void ItemSelected()
    {
        if (inventoryList == null || inventoryList.Count == 0 || selectedItem < 0 || selectedItem >= inventoryList.Count)
            return;

        if (sword != null) sword.SetActive(false);
        if (dagger != null) dagger.SetActive(false);
        if (bow != null) bow.SetActive(false);
        if (spear != null) spear.SetActive(false);

        HotBar selectedHotBar = inventoryList[selectedItem];
        HotBarSlot slot = selectedHotBar.ItemType;
        GameObject selectedObject = itemSetActive[slot];
        selectedObject.SetActive(true);
        weapon w = selectedObject.GetComponent<weapon>();
        w.icon = selectedHotBar.Icon;
        player.EquipWeapon(w);
        Sprite iconToShow = null;
        if (w != null) iconToShow = w.icon;
        if (UIHandler.instance != null)
        {
            UIHandler.instance.setIcon(1, iconToShow);
        }
    }
}