using System.Collections;
using System.Xml.Serialization;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// Author: Christopher Soto
/// Date: November 14, 2025
/// This file stores playercontroller script used by player
/// </summary>
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
    float damageDuration = 1f;
    public bool isAttacking = false;
    bool isTakingIt = false;
    private bool FacingRight = true;
    private Transform Image;

    float MoveX;
    float MoveY;
    public Archetype PlayerClass;  //chris class 
    public PlayerAim AimCon;
    Vector2 MoveVec;

    [SerializeField] float Speed = 5f;
    weapon temporary_test_weapon;
    public Item HotBarItem;

    /// <summary>
    /// sets necessary components and default parameters of the player in case class picker fails
    /// </summary>
    void Awake()
    {
        AimCon = GetComponent<PlayerAim>();
        rb = GetComponent<Rigidbody2D>();
        //we need these because ui breaks without it
        temporary_test_weapon = transform.Find("sword").GetComponent<weapon>();
        initHealthAndSpeed(30);
        //
        Debug.Log("Base: " + getBaseHealth() + " Max: " + getMaxHealth() + " current: " + getHealth() + " Speed: " + getSpeed());
        Attacks = InputSystem.actions.FindAction("Attacks");
        MoveUp = InputSystem.actions.FindAction("MoveUp");
        MoveDown = InputSystem.actions.FindAction("MoveDown");
        MoveRight = InputSystem.actions.FindAction("MoveRight");
        MoveLeft = InputSystem.actions.FindAction("MoveLeft");
        Skill = InputSystem.actions.FindAction("Skill");
        Image = transform.Find("PlayerImage");

    }
    /// <summary>
    /// enables ui at top of screen
    /// </summary>
    void Start()
    {
        UIHandler.instance.setHealthValue(getCurrentHealthPercentage());
        UIHandler.instance.setIcon(1,temporary_test_weapon.icon);
        Assert.NotNull(UIHandler.instance);
    }
    /// <summary>
    /// updates every frame with player input and animation of player
    /// </summary>
    void Update()
    {
        GetInput();
        if(MoveVec.x < 0 && FacingRight){
            FacingRight = false;
            FlipAnimation();
        } else if(MoveVec.x > 0 && !FacingRight){
            FacingRight = true;
            FlipAnimation();
        } 
    }
    /// <summary>
    /// updates every frame with movement and adjusts aim indicator
    /// </summary>
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
    /// <summary>
    /// class setter function that is called by UI when the button is pressed
    /// </summary>
    /// <param name="chosenClass"> sent from ui</param>
    public void setClass(Archetype chosenClass)
     {
        PlayerClass = chosenClass;
        PlayerClass.Initialize(this);
        temporary_test_weapon = PlayerClass.getItems();
        UIHandler.instance.setHealthValue(getCurrentHealthPercentage());
        UIHandler.instance.setIcon(1, temporary_test_weapon.icon);
     }
    /// <summary>
    /// gets input from player and is called by update
    /// </summary>
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
    /// <summary>
    /// moves player called by fixedupdate
    /// </summary>
    void HandleMovement()
    {
        rb.MovePosition(rb.position + (MoveVec * Speed * Time.fixedDeltaTime));
    }
    /// <summary>
    ///  flips sprite based on player input called by update
    /// </summary>
    void FlipAnimation(){
      Vector3 CurrentScale = Image.localScale;
      CurrentScale.x *= -1;
      Image.localScale = CurrentScale;
    }
    /// <summary>
    /// detects if player is in motion, called by fixed update
    /// </summary>
    /// <returns> bool </returns>
    public bool Moving()
    {
        return MoveVec != Vector2.zero;
    }
    /// <summary>
    /// enables current weapon enabled
    /// </summary>
    void AttackFun()
    {
        if (HotBarItem != null)
        {
            var action = HotBarItem.CreateAction();
            if (action != null)
            {
                action.Activate(this.gameObject);
                var inventory = GetComponent<PlayerInventory>();
                inventory.ConsumeHotbarItem(HotBarItem);
            }
            return;
        }
        if (temporary_test_weapon != null)
        {
            temporary_test_weapon.use();
        }
    }

    /// <summary>
    /// player takes damage sent from enemy detecting box collider
    /// </summary>
    /// <param name="damage"> sent from enemy</param>
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
    /// <summary>
    /// function that prevents player from taking infinite damage from constant collision
    /// </summary>
    /// <returns></returns>
    private IEnumerator TakeItTimer()
    {
        isTakingIt = true;
        yield return new WaitForSeconds(damageDuration);
        isTakingIt = false;
    }
    /// <summary>
    /// Called by various health modification functions in character.cs
    /// when the current health hits 0. Whatever that needs to happen on player player
    /// death should occur here.
    /// </summary>
    public override void die()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }
    /// <summary>
    /// equips new weapon
    /// </summary>
    /// <param name="w"></param>
    public void EquipWeapon(weapon w)
    {
        temporary_test_weapon = w;
    }
    public void healPlayer(int healAmount)
    {
        modifyHealth(healAmount);
        UIHandler.instance.setHealthValue(getCurrentHealthPercentage());

    }
}
