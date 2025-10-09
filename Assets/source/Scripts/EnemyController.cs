using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : Enemy
{

  void FixedUpdate() {
    decideMove();
    move();
  }
  void Awake()
  {
    initHealthAndSpeed(3,speed:1); // Sets current and max health to 3 and speed to 1
    initMovement();
  }
}
