/*   Author: Josh Gillum              .
 *   Date: 11 November 2025           ":"         __ __
 *                                  __|___       \ V /
 *                                .'      '.      | |
 *                                |  O       \____/  |
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 * This file stores the SpawnMinion effect, which spawns simple enemies at the
 * the current objects location.
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 */
using UnityEngine;

public class SpawnMinion : Effect {
   public GameObject MinionPreFab; // Prefab for enemy to spawn
   public override int individualEffect(){
      Debug.Log("Casting Spawn Minion...");
      Instantiate(MinionPreFab, gameObject.transform.position, Quaternion.identity); // Spawns the object
      return 1;
   }

   void Awake(){
      base.init();
   }

}
