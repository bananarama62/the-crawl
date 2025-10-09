using UnityEngine;
using NUnit.Framework;

public class BossOverlord : Boss {

  private SpawnMinion ability_spawn_minion;

  void FixedUpdate() {
    decideMove();
    move();
    ability_spawn_minion.use();
  }
  void Awake()
  {
    initHealthAndSpeed(5,speed:3); // Sets current and max health to 5 and speed to 3
    initMovement();
    Transform t = transform.Find("SpawnMinion");
    Assert.NotNull(t);
    ability_spawn_minion = t.GetComponent<SpawnMinion>();
  }
  
}
