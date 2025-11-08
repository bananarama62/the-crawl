using UnityEngine;

public class Stab : Effect
{
    public float SwingSpeed;
    public float Distance;
    [SerializeField] private GameObject AimDirection;
    private BoxCollider2D Collision;
    private SpriteRenderer Sprite;
    private Vector3 StartPos;
    private Vector3 EndPos;
    private bool IsStabbing = false;


    void enable(bool value)
    {
        Sprite.enabled = value;
        Collision.enabled = value;
    }

    public override int individualEffect()
    {
        StartPos = transform.localPosition;
        Vector3 WorldDir = AimDirection.transform.right.normalized;
        Vector3 LocalDir = transform.parent.InverseTransformDirection(WorldDir);
        EndPos = StartPos + LocalDir * Distance;
        float angle = Mathf.Atan2(LocalDir.y, LocalDir.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0f, 0f, angle);
        enable(true);
        IsStabbing = true;
        return 1;
    }

    void Awake()
    {
        Collision = GetComponent<BoxCollider2D>();
        Sprite = transform.Find("image").GetComponent<SpriteRenderer>();
        enable(false);
    }

    void Update()
    {
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
