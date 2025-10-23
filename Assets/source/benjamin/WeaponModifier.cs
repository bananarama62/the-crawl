using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/WeaponModifier")]
public class WeaponModifier : Item
{
    public float damageMultiplier;
    public override void Activate(GameObject target)
    {
        var weapons = target.GetComponentInChildren<weapon>(includeInactive: true);
        if (weapons != null)
        {
            weapons.BoostDamage(damageMultiplier);
            Debug.Log($"Player has gained damage!");
        }
    }
}
