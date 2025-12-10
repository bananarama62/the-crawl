using UnityEngine;
//using NUnit.Framework;

public class SpawnPylon : Effect {
   public GameObject PylonPreFab; // Prefab for enemy to spawn
   private GameObject PlayerLocation;
   public GameObject SpawnedPylon;
   public override int individualEffect(){
      Debug.Log("Casting Spawn Pylon...");
      // Spawn the pylon a bit higher than the player's location so the bottom is in the correct spot since the origin is the top
      GameObject temp = Instantiate(PylonPreFab, PlayerLocation.transform.position + new Vector3(0,+4,0), Quaternion.identity);
      if(temp){
         SpawnedPylon = temp;
         SpawnedPylon.GetComponent<FallingObject>().setIndicatorOffset(0,-4);
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
