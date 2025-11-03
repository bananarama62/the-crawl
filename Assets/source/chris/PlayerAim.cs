using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerAim : MonoBehaviour
{
    PlayerController controller;
    private Vector3 MousePos;
    private Vector2 direction;
    [SerializeField] public GameObject player;
    //[SerializeField] public Transform SpawnPoint;
    [SerializeField] public GameObject indicator;
    private void Awake()
    {
        controller = GetComponent<PlayerController>();
    }
    private void Update()
    {
        if (controller?.PlayerClass == null) return;
        handlePlayerAim();
        handleSkill();

    }

    private void handlePlayerAim()
    {
        // Will likely have to change when we port to another system
        MousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        MousePos.z = 0f;
        direction = (MousePos - player.transform.position).normalized; // Creates a Vector3 pointing from the player's location to the mouse position
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Converts Vector3 to an angle
        indicator.transform.rotation = Quaternion.Euler(0f,0f,angle);


    }
    private void handleSkill()
    {
        if (controller.Skill.IsPressed())
        {
            controller.PlayerClass.castSkill();
        }
    }
}
