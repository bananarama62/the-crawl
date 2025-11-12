/*   Author: Josh Gillum              .
 *   Date: 11 November 2025           ":"         __ __
 *                                  __|___       \ V /
 *                                .'      '.      | |
 *                                |  O       \____/  |
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 * This file stores the Effect class, which is any sort of effect or event that
 * can occur within the game. This class is designed to be used by all sorts of
 * different sources. It can be used as an ability, as part of a weapon, etc.
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 */

using UnityEngine;
using NUnit.Framework;

public abstract class Effect : MonoBehaviour {
   // This class models an effect, which is just anything that happens in the
   // game. Ex: sword swing, explosion, projectile.
   
   // A game object with the Cooldown class script attached should be the child
   // of the object with an individual effect script.
   protected Cooldown cooldown;

   public abstract int individualEffect(); // Gets overriden to provide the actual effect

   public int use(){
      // This function should be called to use the effect. It ensures
      // that the cooldown is not active.
      if(cooldown.start()){ // Ability is still on cooldown
         return 1;
      }
      return individualEffect();
   }

   
   public bool onCooldown(){
      // Returns whether the cooldown for this effect is active or not
      return cooldown.onCooldown();
   }

   public void init(){
      // Finds the attached cooldown
      Transform t = transform.Find("Cooldown");
      Assert.NotNull(t);
      cooldown = t.GetComponent<Cooldown>();
   }

   void Start() {
      init();
   }
}
