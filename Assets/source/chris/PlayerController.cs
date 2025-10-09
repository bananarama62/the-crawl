using System.Collections;
using System.Xml.Serialization;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
    float damageTime = 0f;
    float damageDuration = 1f;
    public bool isAttacking = false;
    bool isTakingIt = false;
    Slider healthBar;
    string healthBarPath = "HealthBar/HealthBar";

    float MoveX;
    float MoveY;
    float maxHealth = 30f;
    float health = 0f;


    Vector2 MoveVec;

    [SerializeField] float Speed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {

        initHealthAndSpeed(set_base_health: 30, speed: 5);
        Debug.Log("Base: " + getBaseHealth() + " Max: " + getMaxHealth() + " current: " + getHealth() + " Speed: " + getSpeed());
        Swing.SetActive(false);

        rb = GetComponent<Rigidbody2D>();
        //Stuff that needs changed --------------
        Transform t = transform.Find(healthBarPath);
        Assert.NotNull(t);
        healthBar = t.GetComponent<Slider>();
        health = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.minValue = 0;
        healthBar.value = health;
        //Stuff that needs changed ---------------
        Attacks = InputSystem.actions.FindAction("Attacks");
        MoveUp = InputSystem.actions.FindAction("MoveUp");
        MoveDown = InputSystem.actions.FindAction("MoveDown");
        MoveRight = InputSystem.actions.FindAction("MoveRight");
        MoveLeft = InputSystem.actions.FindAction("MoveLeft");
    }

    // Update is called once per frame
    void Update()
    {
        CheckAttack();
        GetInput();
        //---------------------------------
        if (health <= 0)
        {
            playerDeath();
        }
        //---------------------------------
    }

    void FixedUpdate()
    {
        HandleMovement();
        if (Moving())
        {
            Vector2 aimVec = MoveVec;
            if (AimDirection != null && aimVec != Vector2.zero && !isAttacking)
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
        if (Attacks.triggered)
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
    void CheckAttack()
    {
        if (isAttacking)
        {
            timer += Time.deltaTime;
            if (timer >= duration)
            {
                timer = 0f;
                Swing.SetActive(false);
                isAttacking = false;
            }
        }
    }

    //---------------------------------
    public void takeDamage(float damage)
    {
        if (isTakingIt)
        {
            return;
        }

        health -= damage;
        healthBar.value = health;
        StartCoroutine(TakeItTimer());
    }

    private IEnumerator TakeItTimer()
    {
        isTakingIt = true;
        yield return new WaitForSeconds(damageDuration);
        isTakingIt = false;
    }

    //---------------------------------
    
    void playerDeath()
    {
        if(health <= 0 )
        {
            string currentScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentScene);
        }
    }
}
