using UnityEngine;

public class SpawnMinion : Effect {
   public GameObject MinionPreFab; // Prefab for enemy to spawn
   public override int individualEffect(){
      Debug.Log("Casting Spawn Minion...");
      Instantiate(MinionPreFab, gameObject.transform.position, Quaternion.identity);
      return 1;
   }

   void Awake(){
      base.init();
   }

}
