using System.Collections;
using NUnit.Framework;
using UnityEngine;

/**
    * HotBar class represents an item or weapon that can be equipped in the player's hotbar.
*/
[CreateAssetMenu(fileName = "New HotBar Item", menuName = "HotBar Item")]
public class HotBar : ScriptableObject
{
    public HotBarSlot ItemType;
    public Sprite Icon;

}

// Enum for different hotbar slots
public enum HotBarSlot
{
    sword,
    bow,
    dagger,
    spear,
}
