using Codice.Client.BaseCommands.Merge.Xml;
using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "New HotBar Item", menuName = "HotBar Item")]
public class HotBar : ScriptableObject
{
    public HotBarSlot ItemType;
    public Sprite Icon;

}

public enum HotBarSlot
{
    sword,
    bow,
    dagger,
    spear,
}