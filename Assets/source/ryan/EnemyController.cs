using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : Enemy
{
    void FixedUpdate()
    {
        decideMove();
        move();
    }

    protected virtual void Awake()
    {
        initMovement();
    }
}