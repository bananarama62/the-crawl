using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic;

public class BossDave : Boss {

   private ChainLightning ability_chain_lightning;
   private SpawnPylon ability_spawn_pylon;
   [SerializeField] int max_num_pylons;
   private List<GameObject> pylons = new();
   private float time_since_last_pylon_placed;
   [SerializeField] float delay_before_chain_lightning;
   private float chain_lightning_elapsed_time;
   [SerializeField] float delay_before_chain_lightning_end;
   private bool chain_lightning_active;


   void FixedUpdate() {
      if (playerInSight){
         if(pylons.Count < max_num_pylons){
            chain_lightning_active = false;
            int status;
            if((status = ability_spawn_pylon.use()) == 0){
               pylons.Add(ability_spawn_pylon.SpawnedPylon);
               if(pylons.Count == max_num_pylons){
                  time_since_last_pylon_placed = 0;
               }
            } else if(status == 2){
               Debug.Log("Spawn pylon returned NULL.");
            }
         } else {
            if(!chain_lightning_active){
               time_since_last_pylon_placed += Time.deltaTime;
               if(time_since_last_pylon_placed > delay_before_chain_lightning){
                  ability_chain_lightning.setPylons(pylons);
                  ability_chain_lightning.setOrigin(gameObject);
                  ability_chain_lightning.use();
                  chain_lightning_elapsed_time = 0;
                  chain_lightning_active = true;
               }
            } else {
               chain_lightning_elapsed_time += Time.deltaTime;
               if(chain_lightning_elapsed_time > delay_before_chain_lightning_end){
                  foreach (var pylon in pylons){
                     Destroy(pylon);
                  }
                  pylons.Clear();
                  chain_lightning_active = false;
               }
            }
         }
      }
   }

   void Awake()
   {
      initHealthAndSpeed(5,speed:3); // Sets current and max health to 5 and speed to 3
      initMovement();
      Transform t = transform.Find("ChainLightning");
      Assert.NotNull(t);
      ability_chain_lightning = t.GetComponent<ChainLightning>();
      t = transform.Find("SpawnPylon");
      Assert.NotNull(t);
      ability_spawn_pylon = t.GetComponent<SpawnPylon>();
   }

}
