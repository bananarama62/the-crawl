using UnityEngine;
public class SwingDagger : Effect
{
    public float SwingSpeed;
    [SerializeField] private GameObject AimDirection;
    [SerializeField] private bool SwingFromLeft;
    [SerializeField] private int StartSwingDegreesFromCenter;
    [SerializeField] private int DegreesOfArc;

    private BoxCollider2D Collision;
    private SpriteRenderer Sprite;
    private Quaternion TargetRotation;
    private Quaternion StartRotation;
    private int SlashNum;
    private bool Swinging = false;

    void Awake()
    {
        Collision = GetComponent<BoxCollider2D>();
        Sprite = transform.Find("image").GetComponent<SpriteRenderer>();
        Enable(false);
    }

    void Enable(bool value)
    {
        Sprite.enabled = value;
        Collision.enabled = value;
        Swinging = value;
    }

    public override int individualEffect()
    {
        SlashNum = 0;
        BeginSwing(SwingFromLeft);
        Enable(true);
        return 1;
    }

    private void BeginSwing(bool FromLeft)
    {
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
        transform.rotation = AimDirection.transform.rotation * StartOffset;
        StartRotation = transform.rotation;
        TargetRotation = transform.rotation * EndOffset;
    }

    void Update()
    {
        if (Swinging)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation, Time.deltaTime * SwingSpeed);

            if (Mathf.Abs(transform.rotation.eulerAngles.z - TargetRotation.eulerAngles.z) <= 2f)
            {
                if (SlashNum == 0)
                {
                    // Start the second slash in the opposite direction
                    SlashNum = 1;
                    SwingFromLeft = !SwingFromLeft;
                    Vector3 scale = transform.localScale;
                    scale.y *= -1;
                    transform.localScale = scale;
                    BeginSwing(SwingFromLeft);
                }
                else
                {
                    // End after the second slash
                    Enable(false);
                }
            }
        }

    }
}