using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    InputAction MoveUp;
    InputAction MoveDown;
    InputAction MoveRight;
    InputAction MoveLeft;

    float MoveX;
    float MoveY;

    Vector2 MoveVec;

    [SerializeField] float Speed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        MoveUp = InputSystem.actions.FindAction("MoveUp");
        MoveDown = InputSystem.actions.FindAction("MoveDown");
        MoveRight = InputSystem.actions.FindAction("MoveRight");
        MoveLeft = InputSystem.actions.FindAction("MoveLeft");
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void GetInput()
    {
        MoveY = 0;
        MoveX = 0;
        if (MoveUp.IsPressed())
        {
            MoveY = 1;
        }
        if (MoveDown.IsPressed())
        {
            MoveY = -1;
        }
        if (MoveRight.IsPressed())
        {
            MoveX = 1;
        }
        if (MoveLeft.IsPressed())
        {
            MoveX = -1;
        }
        MoveVec = new Vector2(MoveX, MoveY).normalized;
    }

    void HandleMovement()
    {
        rb.MovePosition(rb.position + (MoveVec * Speed * Time.fixedDeltaTime));
    }

    void Attack()
    {
        
    }
}
