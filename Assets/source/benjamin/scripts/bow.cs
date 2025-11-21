using UnityEngine;
/**
    * Bow class handles the bow behavior and arrow firing.
*/
/**
 * Utilized asset is the bow sound effect from Minecraft. The sound has been altered by pitching it higher, this falls under transformative. Although there is a legal implication for reusing the sound,
 * it is being used here for educational purposes only, and no profit will be generated. 
 * However, even if we consider this as a commerical use, it still falls under fair use as it is a transformative use.
 * 
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
    [SerializeField] private Sprite yourDefaultSprite;
    [SerializeField] private Sprite badSprite;
    private ArrowSprite bowSprite;
    // Track the bow's rotation to match the aim direction
    private void Update()
    {
        transform.rotation = AimDirection.transform.rotation;
    }

    private void Awake()
    {
        bowSprite = new ActualArrowSprite(gameObject);
        bowSprite.SetSprite(yourDefaultSprite, badSprite);

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