/*   Author: Josh Gillum              .
 *   Date: 6 November 2025           ":"         __ __
 *                                  __|___       \ V /
 *                                .'      '.      | |
 *                                |  O       \____/  |
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 * This file stores the BossDave class, which defines the AI for the dave boss
 * encounter. This fight is intended to require planning on the part of the
 * player. The boss has an ability to spawn a number of pylons that fall from
 * the ceiling, doing impact damage in a short radius. After a number of Pylons
 * are spawned, the boss will shoot lightning, which chains between the pylons.
 * The player will need to stand in strategic locations when the pylons are 
 * thrown, so that they aren't trapped by them. 
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 */

using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic;

public class BossDave : Boss {
   // Stores the dave boss, which has the ability to throw pylons and chain lightning

   // Abilities that the boss can use
   private ChainLightning AbilityChainLightning;
   private SpawnPylon AbilitySpawnPylon;
   //
   // The number of pylons that will be spawned before chain lighting is cast
   [SerializeField] int MaxNumPylons; 
   private List<GameObject> Pylons = new(); // Stores spawned pylons
   private float TimeSinceFinalPylonPlaced; // Stores the time elapsed since the final pylon is placed before chain lightning
   [SerializeField] float DelayBeforeChainLightning; // Delay to wait after final pylon before casting chain lightning
   private float ChainLightningElapsedTime; // Elapsed time since chain lightning was cast.
   [SerializeField] float TimeUntilCleanupPylons; // How long after chain lightning was cast to wait before destroying pylons.
   private bool ChainLightningIsActive; // Whether chain lightning is currently active
   private FourDirectionSprite AnimationControl; // Controls sprite animations

   void FixedUpdate() {
      if (playerInSight){
         decideMove();
         // Casts pylons if the max amount aren't out
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
            // Casts chain lightning if not active
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
               // Destroys pylons if enough time has passed. Will change to use Destroy(pylon,TimeUntilCleanupPylons);
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
      } else {
         MoveVec = Vector2.zero;
      }
   }

   void Update(){
      // Moves the boss and updates the animation
      move();
      AnimationControl.UpdateDirection(MoveVec);
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
      AnimationControl = GetComponent<FourDirectionSprite>();
   }

}
