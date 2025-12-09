/*   Author: Josh Gillum              .
 *   Date: 11 November 2025           ":"         __ __
 *                                  __|___       \ V /
 *                                .'      '.      | |
 *                                |  O       \____/  |
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 * This file stores the SpawnPylon class, which spawns object at the a specific
 * location. It is used by the Dave boss for its chain lightning spell, but
 * this class can be used for any objects that are to be spawned at a specific
 * location.
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 */
using UnityEngine;
using NUnit.Framework;

public class SpawnPylon : Effect {
   public GameObject PylonPreFab; // Prefab for enemy to spawn
   private GameObject PlayerLocation; // Location of the player (although it can be any object where you want to spawn the object)
   public GameObject SpawnedPylon;
   public override int individualEffect(){
      Debug.Log("Casting Spawn Pylon...");
      // Spawn the pylon a bit higher than the player's location so the bottom is in the correct spot since the origin is the top
      GameObject temp = Instantiate(PylonPreFab, PlayerLocation.transform.position + new Vector3(0,+4,0), Quaternion.identity);
      if(temp){
         SpawnedPylon = temp;
         SpawnedPylon.GetComponent<FallingObject>().setIndicatorOffset(0,-4); // Offset to spawn falling object indicator
         return 0;
      }
      return 2;
   }

   void Awake(){
      base.init();
      PlayerLocation = GameObject.Find("Player");
      //Assert.NotNull(PlayerLocation);
   }

}
