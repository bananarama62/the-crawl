using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerAim : MonoBehaviour
{
    PlayerController controller;
    private Vector3 mousePos;
    private Vector2 direction;
    [SerializeField] public GameObject player;
    [SerializeField] public Transform spawnPoint;
    private void Awake()
    {
        controller = GetComponent<PlayerController>();
    }
    private void Update()
    {
        if (controller?.playerClass == null) return;
        handlePlayerAim();
        handleSkill();

    }

    private void handlePlayerAim()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos.z = 0f;
        direction = (mousePos - player.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //player.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    private void handleSkill()
    {
        if (controller.Skill.IsPressed())
        {
            controller.playerClass.castSkill();
        }
    }
}
