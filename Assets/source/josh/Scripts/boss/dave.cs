using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic;

public class BossDave : Boss {

   private ChainLightning AbilityChainLightning;
   private SpawnPylon AbilitySpawnPylon;
   [SerializeField] int MaxNumPylons;
   private List<GameObject> Pylons = new();
   private float TimeSinceFinalPylonPlaced;
   [SerializeField] float DelayBeforeChainLightning;
   private float ChainLightningElapsedTime;
   [SerializeField] float TimeUntilCleanupPylons;
   private bool ChainLightningIsActive;


   void FixedUpdate() {
      if (playerInSight){
         if(Pylons.Count < MaxNumPylons){
            ChainLightningIsActive = false;
            int Status;
            if((Status = AbilitySpawnPylon.use()) == 0){
               Pylons.Add(AbilitySpawnPylon.SpawnedPylon);
               if(Pylons.Count == MaxNumPylons){
                  TimeSinceFinalPylonPlaced = 0;
               }
            } else if(Status == 2){
               Debug.Log("Spawn pylon returned NULL.");
            }
         } else {
            if(!ChainLightningIsActive){
               TimeSinceFinalPylonPlaced += Time.deltaTime;
               if(TimeSinceFinalPylonPlaced > DelayBeforeChainLightning){
                  AbilityChainLightning.setPylons(Pylons);
                  AbilityChainLightning.setOrigin(gameObject);
                  AbilityChainLightning.use();
                  ChainLightningElapsedTime = 0;
                  ChainLightningIsActive = true;
               }
            } else {
               ChainLightningElapsedTime += Time.deltaTime;
               if(ChainLightningElapsedTime > TimeUntilCleanupPylons){
                  foreach (var Pylon in Pylons){
                     Destroy(Pylon);
                  }
                  Pylons.Clear();
                  ChainLightningIsActive = false;
               }
            }
         }
      }
   }

   void Awake()
   {
      initHealthAndSpeed(5,speed:3); // Sets current and max health to 5 and speed to 3
      initMovement();
      // Finds the ChainLightning Script
      Transform t = transform.Find("ChainLightning");
      Assert.NotNull(t);
      AbilityChainLightning = t.GetComponent<ChainLightning>();
      // Finds the SpawnPylon script
      t = transform.Find("SpawnPylon");
      Assert.NotNull(t);
      AbilitySpawnPylon = t.GetComponent<SpawnPylon>();
   }

}
