/*   Author: Josh Gillum              .
 *   Date: 11 November 2025           ":"         __ __
 *                                  __|___       \ V /
 *                                .'      '.      | |
 *                                |  O       \____/  |
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 * This file stores the Usable class, which is designed to be a parent class of
 * any object that can be used (weapon, ability, consumable)...
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 */

using UnityEngine;

public abstract class Usable {
   // Models an object or item that can be used.
   private string name;
   public abstract int use();

   public void setName(string new_name){
      // Sets the name of the object
      name = new_name;
   }

   public string getName(){
      // Returns the name of the object.
      return name;
   }
}
