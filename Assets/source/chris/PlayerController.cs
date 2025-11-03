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
    public InputAction Skill;
    public Transform AimDirection;
    float duration = 0.3f;
    float damageTime = 0f;
    float damageDuration = 1f;
    public bool isAttacking = false;
    bool isTakingIt = false;

    float MoveX;
    float MoveY;
    public Archetype PlayerClass;  //chris class 
    public PlayerAim AimCon;
    Vector2 MoveVec;

    [SerializeField] float Speed = 5f;
    [SerializeField] weapon temporary_test_weapon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        AimCon = GetComponent<PlayerAim>();
        PlayerClass = new Archer();
        PlayerClass.Initialize(this);
        rb = GetComponent<Rigidbody2D>();
        initHealthAndSpeed(30);
        Debug.Log("Base: " + getBaseHealth() + " Max: " + getMaxHealth() + " current: " + getHealth() + " Speed: " + getSpeed());
        //Stuff that needs changed ---------------
        Attacks = InputSystem.actions.FindAction("Attacks");
        MoveUp = InputSystem.actions.FindAction("MoveUp");
        MoveDown = InputSystem.actions.FindAction("MoveDown");
        MoveRight = InputSystem.actions.FindAction("MoveRight");
        MoveLeft = InputSystem.actions.FindAction("MoveLeft");
        Skill = InputSystem.actions.FindAction("Skill");

    }

    void Start()
    {
        UIHandler.instance.setHealthValue(getCurrentHealthPercentage());
        UIHandler.instance.setIcon(1,temporary_test_weapon.icon);
        Assert.NotNull(UIHandler.instance);
    }
    // Update is called once per frame
    void Update()
    {
        GetInput();
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
    // class setter function that is called by UI when the button is pressed
    // public void setClass(Archetype chosenClass)
    // {
    //     playerClass = chosenClass;
    // }

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
        temporary_test_weapon.use();
    }

    //---------------------------------
    public void takeDamage(float damage)
    {
        if (isTakingIt)
        {
            return;
        }

        modifyHealth(-1 * (int)(damage));
        UIHandler.instance.setHealthValue(getCurrentHealthPercentage());

        StartCoroutine(TakeItTimer());
    }

    private IEnumerator TakeItTimer()
    {
        isTakingIt = true;
        yield return new WaitForSeconds(damageDuration);
        isTakingIt = false;
    }

    //---------------------------------
    
    // Called by various health modification functions in character.cs
    // when the current health hits 0. Whatever that needs to happen on player player
    // death should occur here.
    public override void die()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }
    

    public void powerUp(Pickup pickup)
    {
        pickup.item.Activate(this.gameObject);
    }

    public void healPlayer(int healAmount)
    {
        modifyHealth(healAmount);
        UIHandler.instance.setHealthValue(getCurrentHealthPercentage());

    }
}
