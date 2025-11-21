using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : Enemy
{
    // Called every fixed frame-rate frame, handles movement and decision-making
    void FixedUpdate()
    {
        decideMove();
        move();
    }

    // Initializes movement and health bar on object creation
    protected virtual void Awake()
    {
        initMovement();
    }
}