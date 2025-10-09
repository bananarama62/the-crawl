using UnityEngine;

public class BossOverlord : Boss {
  private SpawnMinion a;

  void FixedUpdate() {
    decideMove();
    move();
  }
  void Awake()
  {
    a = new SpawnMinion();
    initHealthAndSpeed(5,speed:3); // Sets current and max health to 5 and speed to 3
    initMovement();
    a.use();
  }
  
}
