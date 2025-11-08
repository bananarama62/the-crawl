using UnityEngine;

public abstract class Item : ScriptableObject
{
    public abstract void Activate(GameObject target);
}
