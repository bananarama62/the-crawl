using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// Author: Christopher Soto
/// Date: November 14, 2025
/// This file stores playeraim script used by player
/// </summary>
public class PlayerAim : MonoBehaviour
{
    PlayerController controller;
    private Vector3 MousePos;
    private Vector2 direction;
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject indicator;
    /// <summary>
    /// sets necessary components
    /// </summary>
    private void Awake()
    {
        controller = GetComponent<PlayerController>();
    }
    /// <summary>
    /// constantly updates every frame checking player aim input and if player used his skill
    /// </summary>
    private void Update()
    {
        if (controller?.PlayerClass == null) return;
        handlePlayerAim();
        handleSkill();

    }
    /// <summary>
    /// sets aim direction from mouse input and sets visual indicator
    /// </summary>
    private void handlePlayerAim()
    {
        // Will likely have to change when we port to another system
        MousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        MousePos.z = 0f;
        direction = (MousePos - player.transform.position).normalized; // Creates a Vector3 pointing from the player's location to the mouse position
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Converts Vector3 to an angle
        indicator.transform.rotation = Quaternion.Euler(0f,0f,angle);


    }
    /// <summary>
    /// activates skill if button was pressed
    /// </summary>
    private void handleSkill()
    {
        if (controller.Skill.IsPressed())
        {
            controller.PlayerClass.castSkill();
        }
    }
}
