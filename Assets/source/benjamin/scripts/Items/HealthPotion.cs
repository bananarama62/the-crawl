using UnityEngine;
/**
    * HealthPotionItem class represents a health potion that can be used to heal the player.
*/
[CreateAssetMenu(menuName = "Items/Potions/HealthPotion")]
public class HealthPotionItem : Item
{
    public int HealAmount = 25;

    public override IItemAction CreateAction()
    {
        return new HealthPotionAction(HealAmount);
    }
}
/**
    * HealthPotionAction class defines the action taken when a health potion is used.
*/
public class HealthPotionAction : IItemAction
{
    private readonly int _amount;
    public HealthPotionAction(int amount) => _amount = amount;

    public void Activate(GameObject target)
    {
        if (target == null) return;
        var player = target.GetComponent<PlayerController>();
        if (player != null)
        {
            player.healPlayer(_amount);
        }
    }
}