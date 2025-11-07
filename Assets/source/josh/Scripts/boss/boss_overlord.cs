/*   Author: Josh Gillum              .
 *   Date: 6 November 2025           ":"         __ __
 *                                  __|___       \ V /
 *                                .'      '.      | |
 *                                |  O       \____/  |
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 * This file stores the BossOverlord class, which controls the AI for the 
 * overlord boss enounter. This boss spawns various minions that attack the 
 * player.
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 */

using UnityEngine;
using NUnit.Framework;

public class BossOverlord : Boss {

   private SpawnMinion AbilitySpawnMinion; // The ability for spawning minions

   void FixedUpdate() {
      // Moves and spawns minions
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
      // Finds the Spawn minion ability.
      Transform t = transform.Find("SpawnMinion");
      Assert.NotNull(t);
      AbilitySpawnMinion = t.GetComponent<SpawnMinion>();
   }

}
