using UnityEngine;
/**
    * Bow class handles the bow behavior and arrow firing.
*/
public class Bow : Effect
{
    // Arrow prefab to be instantiated when firing
    [SerializeField] private GameObject ArrowPrefab;
    // Starting position for arrow spawn
    [SerializeField] private Transform StartPos;
    // Reference to the aim direction object
    [SerializeField] private GameObject AimDirection;   
    // Speed at which the arrow will travel
    [SerializeField] private float ArrowSpeed;
    // Identifier for the entity that fired the arrow
    [SerializeField] private string Caster = "Player";
    // Reference to the sprite renderer for visual representation
    private SpriteRenderer Sprite;
    // Track the bow's rotation to match the aim direction
    private void Update()
    {
        transform.rotation = AimDirection.transform.rotation;
    }

    // Method to execute the individual effect of firing an arrow
    public override int individualEffect()
    {
        // Determine spawn position and rotation for the arrow
        Vector3 SpawnPos;
        SpawnPos = StartPos.position;
        Quaternion Rotation;
        Rotation = AimDirection.transform.rotation;

        // Instantiate and configure the arrow
        GameObject arrow = Instantiate(ArrowPrefab, SpawnPos, Rotation);
        Arrow  aw= arrow.GetComponent<Arrow>();
        aw.Owner = Caster;
        aw.Speed = ArrowSpeed;
        aw.Fire(Rotation);
        return 1;
    }
}