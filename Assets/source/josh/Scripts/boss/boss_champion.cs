/*   Author: Josh Gillum              .
 *   Date: 6 November 2025           ":"         __ __
 *                                  __|___       \ V /
 *                                .'      '.      | |
 *                                |  O       \____/  |
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 * This file stores the BossChampion class, which handles the AI for the 
 * champion boss. This boss features two axes that it can swing within short
 * range. It also has another axe that it can throw towards the player.
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 */

using UnityEngine;
//using NUnit.Framework;
using System.Collections.Generic;

public class BossChampion : Boss {

   private weapon AxeLeft; // Axe held in left hand
   private weapon AxeRight; // Axe held in right hand
   private DetectRange WithinMeleeRange; // The range that the boss will use his melee attacks in
   private DetectRange WithinAbilityRange; // The range that the boss will use his thrown weapon in
   private Transform AimDirection; // The direction that points from the boss towards the player
   private WeaponProjectile AbilityThrownWeapon; // Ability object for the thrown weapon
   private FourDirectionSprite AnimationControl; // Controller for the four direction sprite
   public AudioClip SwingSound;
    void Update() {
      // Moves towards the player if the player is within the encounter area.
      if(WithinSightRange.inSight()){
         playerInSight = true;
         move();
      } else {
         playerInSight = false;
      }
      // Uses melee attacks if player is in close range
      if(WithinMeleeRange.playerInSight){
         AxeLeft.use();
         AxeRight.use();
      } else {
         // Uses ranged attack if player is in medium range
         if(WithinAbilityRange.playerInSight){
            if(!AbilityThrownWeapon.onCooldown()){ // Only aims and attempts to use ability if it is not on cooldown
               AbilityThrownWeapon.aim(transform.position,AimDirection.rotation); // Specifies which direction to throw weapon
               AbilityThrownWeapon.use(); // Actually throws weapon
            }
         }
         //Debug.Log("Player in sight: "+playerInSight+" Melee: "+WithinMeleeRange.playerInSight);
      }
      AnimationControl.UpdateDirection(MoveVec); // Updates animation direction
   }

   void FixedUpdate() {
      if (WithinSightRange.inSight()){
         decideMove(); // Determines which direction to move in

         // Calculates which direction to aim in for attacks at the player
         Vector2 direction = (Player.transform.position - transform.position).normalized;
         float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
         AimDirection.rotation = Quaternion.Euler(0f,0f,angle);
      }
   }

   void Awake()
   {
      initHealthAndSpeed(5,speed:3); // Sets current and max health to 5 and speed to 3
      initMovement();

   }

   void Start(){
      // Finds and sets various elements the boss interacts with.
      AxeLeft = transform.Find("axe_left").GetComponent<weapon>();
      AxeRight = transform.Find("axe_right").GetComponent<weapon>();
      WithinMeleeRange = transform.Find("MeleeDetector").GetComponent<DetectRange>();
      WithinAbilityRange = transform.Find("AbilityDetector").GetComponent<DetectRange>();
      AimDirection = transform.Find("AimDirection");

      AbilityThrownWeapon = transform.Find("AbilityThrownWeapon").GetComponent<WeaponProjectile>();
      AnimationControl = GetComponent<FourDirectionSprite>();
      WithinSightRange = GameObject.Find("BossEncounterBounds").GetComponent<BossEncounter>();
      Player = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        if (SwingSound != null)
        {
            if (AxeLeft != null) AxeLeft.swingSound = SwingSound;
            if (AxeRight != null) AxeRight.swingSound = SwingSound;
        }
    }

}
