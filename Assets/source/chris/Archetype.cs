using UnityEngine;

public abstract class Archetype
{
    protected PlayerController player;
    public virtual void Initialize(PlayerController player)
    {
        this.player = player;
    }
    public abstract void setBaseStats();
    public abstract string getSprite();
    public abstract void setItems();
    public abstract weapon getItems();
    public abstract void castSkill();
}
