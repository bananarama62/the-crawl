using UnityEngine;
/// <summary>
/// Author: Christopher Soto
/// Date: November 14, 2025
/// This file stores Archetype class used by player
/// </summary>
public abstract class Archetype
{
    protected PlayerController player;
    /// <summary>
    /// initializes player controller sent from player
    /// </summary>
    /// <param name="player"> sent from player</param>
    public virtual void Initialize(PlayerController player)
    {
        this.player = player;
    }
    /// <summary>
    /// abstract classes used by children classes
    /// </summary>
    public abstract void setBaseStats();
    public abstract string getSprite();
    public abstract void setItems();
    public abstract weapon getItems();
    public abstract void castSkill();
}
