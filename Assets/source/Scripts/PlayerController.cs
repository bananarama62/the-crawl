using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : Character // Parent class is in josh/Scripts/character.cs
{
    Rigidbody2D rb;
    InputAction MoveUp;
    InputAction MoveDown;
    InputAction MoveRight;
    InputAction MoveLeft;
    InputAction Attacks;

    public GameObject Swing;
    public Transform AimDirection;
    float duration = 0.3f;
    float timer = 0f;
    public bool isAttacking = false;

    float MoveX;
    float MoveY;
    float health = 30f;

    Vector2 MoveVec;

    [SerializeField] float Speed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {

      base.Awake(set_base_health: 30, speed: 5);
      Debug.Log("Base: " + getBaseHealth() + " Max: " + getMaxHealth() + " current: " + getHealth() + " Speed: " + getSpeed());

        rb = GetComponent<Rigidbody2D>();
        Attacks = InputSystem.actions.FindAction("Attacks");
        MoveUp = InputSystem.actions.FindAction("MoveUp");
        MoveDown = InputSystem.actions.FindAction("MoveDown");
        MoveRight = InputSystem.actions.FindAction("MoveRight");
        MoveLeft = InputSystem.actions.FindAction("MoveLeft");
    }

    // Update is called once per frame
    void Update()
    {
        checkAttack();
        GetInput();
    }

    void FixedUpdate()
    {
        HandleMovement();
        if (Moving())
        {
            Vector2 aimVec = MoveVec;
            if (AimDirection != null && aimVec != Vector2.zero)
            {
                AimDirection.up = -aimVec;
                AimDirection.localPosition = aimVec;
            }
        }
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
        if (Attacks.IsPressed())
        {

            AttackFun();
        }
        MoveVec = new Vector2(MoveX, MoveY).normalized;
    }

    void HandleMovement()
    {
        rb.MovePosition(rb.position + (MoveVec * Speed * Time.fixedDeltaTime));
    }
    public bool Moving()
    {
        return MoveVec != Vector2.zero;
    }
    void AttackFun()
    {
        if (!isAttacking)
        {
            Swing.SetActive(true);
            isAttacking = true;
        }
    }
    void checkAttack()
    {
        if (isAttacking)
        {
            timer += Time.deltaTime;
            if (timer >= duration)
            {
                Swing.SetActive(false);
                isAttacking = false;
                timer = 0f;
            }
        }
    }
    void playerDeath()
    {
        if(health <= 0 )
        {
            string currentScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentScene);
        }
    }
}
