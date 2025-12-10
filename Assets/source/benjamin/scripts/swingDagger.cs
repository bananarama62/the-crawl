using UnityEngine;
/**
    * SwingDagger class handles the swinging dagger effect in the game.
*/
public class SwingDagger : Effect
{
    // Speed at which the dagger swings
    public float SwingSpeed;
    // Reference to the aim direction object
    [SerializeField] private GameObject AimDirection;
    // Indicates whether the swing starts from the left side
    [SerializeField] private bool SwingFromLeft;
    // Degrees to start the swing from center
    [SerializeField] private int StartSwingDegreesFromCenter;
    // Total degrees of arc for the swing
    [SerializeField] private int DegreesOfArc;

    // References to the collider and sprite renderer components
    private BoxCollider2D Collision;
    private SpriteRenderer Sprite;
    // Target rotation for the swing
    private Quaternion TargetRotation;
    private Quaternion StartRotation;
    // Number of slashes completed in the swing
    private int SlashNum;
    private bool Swinging = false;

    // Initialize references
    void Awake()
    {
        Collision = GetComponent<BoxCollider2D>();
        Sprite = transform.Find("image").GetComponent<SpriteRenderer>();
        Enable(false);
    }
    // Enable or disable the swing effect
    void Enable(bool value)
    {
        Sprite.enabled = value;
        Collision.enabled = value;
        Swinging = value;
    }
    // Execute the individual effect of swinging the dagger
    public override int individualEffect()
    {
        SlashNum = 0;
        BeginSwing(SwingFromLeft);
        Enable(true);
        return 1;
    }
    // Begin the swing from the specified side
    private void BeginSwing(bool FromLeft)
    {
        // Calculate start and end rotations based on swing direction
        Quaternion StartOffset;
        Quaternion EndOffset;
        if (FromLeft)
        {
            StartOffset = Quaternion.Euler(0, 0, StartSwingDegreesFromCenter);
            EndOffset = Quaternion.Euler(0, 0, -DegreesOfArc);
        }
        else
        {
            StartOffset = Quaternion.Euler(0, 0, -StartSwingDegreesFromCenter);
            EndOffset = Quaternion.Euler(0, 0, DegreesOfArc);
        }
        // Set the initial rotation and target rotation for the swing
        transform.rotation = AimDirection.transform.rotation * StartOffset;
        StartRotation = transform.rotation;
        TargetRotation = transform.rotation * EndOffset;
    }

    void Update()
    {
        // Handle the swinging motion
        if (Swinging)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation, Time.deltaTime * SwingSpeed);
            // Check if the swing has reached the target rotation
            if (Mathf.Abs(transform.rotation.eulerAngles.z - TargetRotation.eulerAngles.z) <= 2f)
            {
                // If it's the first slash, prepare for the second slash
                if (SlashNum == 0)
                {
                    SlashNum = 1;
                    SwingFromLeft = !SwingFromLeft;
                    Vector3 scale = transform.localScale;
                    scale.y *= -1;
                    transform.localScale = scale;
                    BeginSwing(SwingFromLeft);
                }
                // If both slashes are complete, disable the swing effect
                else
                {
                    Enable(false);
                }
            }
        }

    }
}