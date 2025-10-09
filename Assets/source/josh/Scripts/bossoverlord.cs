using UnityEngine;

public class BossOverlord : Boss {
  private SpawnMinion a;

  void FixedUpdate() {
    decideMove();
    move();
  }
  void Awake()
  {
    base.init(5,speed:3); // Sets current and max health to 5 and speed to 3
  }
  
}
