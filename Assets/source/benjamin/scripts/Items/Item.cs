using UnityEngine;
/**
    * Item class represents a consumable item that can be used by the player.
*/
public abstract class Item : ScriptableObject
{
    public Sprite Icon;

    public abstract IItemAction CreateAction();
    public void Activate(GameObject target)
    {
        var action = CreateAction();
        if (action != null)
            action.Activate(target);
    }
}
