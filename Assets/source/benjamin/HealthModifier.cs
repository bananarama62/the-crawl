using UnityEngine;
[CreateAssetMenu(menuName = "PowerUps/HealthModifier")]
public class HealthModifier : Item
{
    public int healthValue;
    public override void Activate(GameObject target)
    {
        var healthComponent = target.GetComponent<PlayerController>();
        if (healthComponent != null)
        {
            healthComponent.healPlayer(healthValue);
            Debug.Log($"Player has been healed!");
        }

    }
}
