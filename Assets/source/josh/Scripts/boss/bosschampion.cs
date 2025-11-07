using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic;

public class BossChampion : Boss {

   private weapon AxeLeft;
   private weapon AxeRight;
   private DetectRange WithinMeleeRange;
   private DetectRange WithinAbilityRange;
   private BossEncounter WithinSightRange;
   private Transform AimDirection;
   private WeaponProjectile AbilityThrownWeapon;
   private FourDirectionSprite AnimationControl;

   

   void Update() {
      if(WithinSightRange.inSight()){
         playerInSight = true;
         move();
      } else {
         playerInSight = false;
      }
      if(WithinMeleeRange.playerInSight){
         AxeLeft.use();
         AxeRight.use();
      } else {
         if(WithinAbilityRange.playerInSight){
            if(!AbilityThrownWeapon.onCooldown()){
               AbilityThrownWeapon.aim(transform.position,AimDirection.rotation);
               AbilityThrownWeapon.use();
            }
         }
         //Debug.Log("Player in sight: "+playerInSight+" Melee: "+WithinMeleeRange.playerInSight);
      }
      AnimationControl.UpdateDirection(MoveVec);
   }

   void FixedUpdate() {
      if (WithinSightRange.inSight()){
         decideMove();
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
      AxeLeft = transform.Find("axe_left").GetComponent<weapon>();
      AxeRight = transform.Find("axe_right").GetComponent<weapon>();
      WithinMeleeRange = transform.Find("MeleeDetector").GetComponent<DetectRange>();
      WithinAbilityRange = transform.Find("AbilityDetector").GetComponent<DetectRange>();
      AimDirection = transform.Find("AimDirection");

      AbilityThrownWeapon = transform.Find("AbilityThrownWeapon").GetComponent<WeaponProjectile>();
      AnimationControl = GetComponent<FourDirectionSprite>();
      WithinSightRange = GameObject.Find("BossEncounterBounds").GetComponent<BossEncounter>();
      Player = GameObject.Find("Player").GetComponent<Rigidbody2D>();
   }

}
