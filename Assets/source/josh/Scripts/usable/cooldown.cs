/*   Author: Josh Gillum              .
 *   Date: 11 November 2025           ":"         __ __
 *                                  __|___       \ V /
 *                                .'      '.      | |
 *                                |  O       \____/  |
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 * This file stores the Cooldown class, which handles cooldowns. The length of
 * the cooldown should be set within the editor.
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 */


using UnityEngine;

public class Cooldown : MonoBehaviour{
   // Models a cooldown and includes functions for interacting with and
   // following the restrictions it imposes.

   [SerializeField] private double cooldown_length; // Total length of the cooldown
   private double cooldown_remaining; // Time until cooldown is over
   private bool on_cooldown; // Boolean as to whether the cooldown is still active

   public void init(double set_cooldown_length, bool set_on_cooldown=false){
      // Sets the length of the cooldown and can optionally start it.
      if (setCooldownLength(set_cooldown_length)){
         throw new System.ArithmeticException("Cooldown length must be greater than or equal to 0.");
      }
      if(set_on_cooldown){
         start();
      }
   }
   // If the cooldown is active, updates the cooldown time cooldown time
   // remaining. If time remaining is 0 or less, sets cooldown to inactive.
   void FixedUpdate(){
      if (on_cooldown){
         cooldown_remaining -= Time.fixedDeltaTime;
         if (cooldown_remaining <= 0){
            cooldown_remaining = 0;
            on_cooldown = false;
         }
      }
   }

   // Starts the cooldown. Returns true on error (Cooldown is already active) and
   // false on success
   public bool start(){
      if(on_cooldown){
         return true;
      }
      on_cooldown = true;
      cooldown_remaining = cooldown_length;
      return false;
   }

   // Returns whether the cooldown is active or not
   public bool onCooldown(){
      return on_cooldown;
   }

   // Returns the time left on the cooldown or 0 if not on cooldown.
   public double cooldownRemaining(){
      if (on_cooldown){
         return cooldown_remaining;
      }
      return 0;
   }

   // Returns how long the cooldown is
   public double getCooldownLength(){
      return cooldown_length;
   }

   // Sets the new cooldown length. Must be greater than or equal to 0.
   // Returns true if a negative length is provided and false otherwise.
   public bool setCooldownLength(double new_length){
      if (new_length < 0){
         return true;
      }
      cooldown_length = new_length;
      return false;
   }

}
