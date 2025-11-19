using UnityEngine;
/// <summary>
/// Author: Christopher Soto
/// Date: November 14, 2025
/// This file stores Archer class used by player, it inherets from archetype class
/// </summary>
public class Archer : Archetype
{
    private ArcherTrap trap1;
    private weapon weap1;
    /// <summary>
    /// initializes player info from archetype base and then sets trap ability, stats, and items
    /// </summary>
    /// <param name="player"> passes from player controller</param>
    public override void Initialize(PlayerController player)
    {
        base.Initialize(player);
        trap1 = player.transform.Find("Ability").GetComponent<ArcherTrap>();
        setBaseStats();
        setItems();
    }
    /// <summary>
    /// sets player stats
    /// </summary>
    public override void setBaseStats()
    {
        player.initHealthAndSpeed(set_base_health: 30, speed: 10);
    }
    /// <summary>
    /// returns sprite used by player
    /// </summary>
    /// <returns> sprite location</returns>
    public override string getSprite()
    {
        //sprite info goes here
        return "name";
    }
    /// <summary>
    /// sets items used by player based on class
    /// </summary>
    public override void setItems()
    {
        Transform Wep;

        Wep = player.transform.Find("sword");
        if (Wep != null)Wep.gameObject.SetActive(false);

       Wep= player.transform.Find("dagger");
        if (Wep != null)Wep.gameObject.SetActive(false);

       Wep= player.transform.Find("spear");
        if (Wep != null)Wep.gameObject.SetActive(false);

        Transform bowTransform = player.transform.Find("bow");
        if (bowTransform != null)
        {
            bowTransform.gameObject.SetActive(true);
            weap1 = bowTransform.GetComponent<weapon>();
            if (weap1 != null)
            {
                // Make the player use this weapon immediately
                player.EquipWeapon(weap1);

                // Update UI icon if UIHandler is available
                if (UIHandler.instance != null)
                {
                    UIHandler.instance.setIcon(1, weap1.icon);
                }
            }
        }
    }
    /// <summary>
    /// returns items player currently has
    /// </summary>
    /// <returns>returns current item</returns>
    public override weapon getItems()
    {
        return weap1;
    }
    /// <summary>
    /// casts ability set in initialization
    /// </summary>
    public override void castSkill()
    {
        trap1.use();
    }
}
