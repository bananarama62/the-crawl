using UnityEngine;
/**
    * Stab class handles the stabbing effect of a weapon.
*/
public class Stab : Effect
{
    // Speed at which the stab animation occurs
    public float SwingSpeed;

    // Distance the weapon will stab forward
    public float Distance;

    // Reference to the aim direction object
    [SerializeField] private GameObject AimDirection;

    // References to the collider and sprite renderer components
    private BoxCollider2D Collision;
    private SpriteRenderer Sprite;

    // Starting and ending positions for the stab
    private Vector3 StartPos;
    private Vector3 EndPos;
    private bool IsStabbing = false;

    // Enable or disable the stab effect
    void enable(bool value)
    {
        Sprite.enabled = value;
        Collision.enabled = value;
    }
    // Execute the individual effect of stabbing
    public override int individualEffect()
    {
        // Calculate start and end positions based on aim direction
        StartPos = transform.localPosition;
        Vector3 WorldDir = AimDirection.transform.right.normalized;
        Vector3 LocalDir = transform.parent.InverseTransformDirection(WorldDir);
        EndPos = StartPos + LocalDir * Distance;
        float angle = Mathf.Atan2(LocalDir.y, LocalDir.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0f, 0f, angle);
        // Start the stabbing motion
        enable(true);
        IsStabbing = true;
        return 1;
    }

    // Initialize references  
    void Awake()
    {
        Collision = GetComponent<BoxCollider2D>();
        Sprite = transform.Find("image").GetComponent<SpriteRenderer>();
        enable(false);
    }

    void Update()
    {
        // Handle the stabbing motion
        if (IsStabbing)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, EndPos, SwingSpeed * Time.deltaTime);
            float traveled = Vector3.Distance(transform.localPosition, StartPos);
            if (traveled >= Distance - 0.01f || Vector3.Distance(transform.localPosition, EndPos) <= 0.001f)
            {
                IsStabbing = false;
                enable(false);
                transform.localPosition = StartPos;
            }
        }
    }
}
