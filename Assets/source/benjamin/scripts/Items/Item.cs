using UnityEngine;
/**
    * Item class represents a consumable item that can be used by the player.
    * Item defines the method CreateAction(), subclasses override it to create the exact action, Item.Activate uses the result of the factory method
    * The Factory Method is a pattern where a superclass defines a method for creating an object, but subclasses decide the actual concrete type that gets created.
    * You could devlop this as a builder model instead, which would allow you to configure the action more before creating it.
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
