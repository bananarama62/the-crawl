/*   Author: Josh Gillum              .
 *   Date: 6 November 2025           ":"         __ __
 *                                  __|___       \ V /
 *                                .'      '.      | |
 *                                |  O       \____/  |
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 * This file stores the Boss class, which is a base class for all boss 
 * entities. Inherits all Enemy functions and all Character functions as well.
 * Currently only overrides the TakeDamage function to use the special boss
 * health bar instead of the default enemy health bar.
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 */

using UnityEngine;

public abstract class Boss : Enemy {
   protected BossEncounter WithinSightRange; // Boss encounter trigger
   
   public override void TakeDamage(float damage){
      // Overrides to use boss health bar instead of default enemy health bar.
      modifyHealth((int)(-1*damage));
      UIHandler.instance.SetBossHealth(getCurrentHealthPercentage());
   }
}

