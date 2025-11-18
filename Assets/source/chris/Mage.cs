using UnityEngine;
/// <summary>
/// Author: Christopher Soto
/// Date: November 14, 2025
/// This file stores Mage class used by player, it inherets from archetype class
/// </summary>
public class Mage : Archetype
{
    public Fireball fire1;
    private weapon weap1;
    /// <summary>
    /// initializes player info from archetype base and then sets fireball ability, stats, and items
    /// </summary>
    /// <param name="player"> passes from player controller</param>
    public override void Initialize(PlayerController player)
    {
        base.Initialize(player);
        fire1 = player.transform.Find("Ability").GetComponent<Fireball>();
        setBaseStats();
        setItems();
    }
    /// <summary>
    /// sets player stats
    /// </summary>
    public override void setBaseStats()
    {
        player.initHealthAndSpeed(set_base_health: 15, speed: 5);
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
        weap1 = player.transform.Find("sword").GetComponent<weapon>();
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
        fire1.use();
    }
}
