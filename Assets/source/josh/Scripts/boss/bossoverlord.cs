using UnityEngine;
using NUnit.Framework;

public class BossOverlord : Boss {

   private SpawnMinion AbilitySpawnMinion;

   void FixedUpdate() {
      decideMove();
      move();
      if (playerInSight){
         AbilitySpawnMinion.use();
      }
   }
   void Awake()
   {
      initHealthAndSpeed(5,speed:3); // Sets current and max health to 5 and speed to 3
      initMovement();
      Transform t = transform.Find("SpawnMinion");
      Assert.NotNull(t);
      AbilitySpawnMinion = t.GetComponent<SpawnMinion>();
   }

}
