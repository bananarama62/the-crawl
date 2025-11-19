using UnityEngine;

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
