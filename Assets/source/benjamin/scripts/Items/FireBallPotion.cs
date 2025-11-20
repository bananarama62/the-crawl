using UnityEngine;

/**
    * FireballPotionItem class represents a fireball potion that can be used to launch a fireball.
*/
[CreateAssetMenu(menuName = "Items/Potions/FireballPotion")]
public class FireballPotionItem : Item
{
    // Prefab of the fireball to be instantiated
    public GameObject FireBall;
    public float DistanceFromPlayer = 1.0f;
    public override IItemAction CreateAction()
    {
        return new FireballPotionAction(FireBall, DistanceFromPlayer);
    }
}
/**
    * FireballPotionAction class defines the action taken when a fireball potion is used.
*/
public class FireballPotionAction : IItemAction
{
    private readonly GameObject _ballPrefab;
    private readonly float _distance;

    public FireballPotionAction(GameObject ballPrefab, float distance)
    {
        _ballPrefab = ballPrefab;
        _distance = distance;
    }

    public void Activate(GameObject target)
    {
        if (target == null)
        {
            return;
        }

        var player = target.GetComponent<PlayerController>();
        if (player == null) 
        { 
            return; 
        }
        var abilityTransform = player.transform.Find("Ability");
        if (abilityTransform != null)
        {
            var fb = abilityTransform.GetComponent<Fireball>();
            if (fb != null)
            {
                fb.use();
                return;
            }
        }
        var aim = player.AimDirection;
        Quaternion rotation = (aim != null) ? aim.rotation : player.transform.rotation;
        Vector3 spawnLocal = new Vector3(_distance, 0f, 0f);
        Vector3 spawnPos = player.transform.position + (rotation * spawnLocal);

        Object.Instantiate(_ballPrefab, spawnPos, rotation);
    }
}