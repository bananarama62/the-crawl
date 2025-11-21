/*   Author: Josh Gillum              .
 *   Date: 6 November 2025           ":"         __ __
 *                                  __|___       \ V /
 *                                .'      '.      | |
 *                                |  O       \____/  |
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 * This file stores the DetectRange class, which is used to see if the player
 * is within a certain range. It functions off of a collider set as a trigger
 * and sets the playerInSight variable based on entering or exiting the
 * trigger.
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 */

using UnityEngine;

public class DetectRange : MonoBehaviour {

   public bool playerInSight = false;

   void OnTriggerEnter2D(Collider2D collision)
   {
      // Player is in sight and within collider
      if (collision.CompareTag("Player"))
      {
         playerInSight = true;
      }
   }

   void OnTriggerExit2D(Collider2D collision)
   {
      // Player is out of sight and outside of collider
      if (collision.CompareTag("Player"))
      {
         playerInSight = false;
      }
   }

}
