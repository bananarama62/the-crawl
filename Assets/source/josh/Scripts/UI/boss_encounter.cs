/*   Author: Josh Gillum              .
 *   Date: 6 November 2025           ":"         __ __
 *                                  __|___       \ V /
 *                                .'      '.      | |
 *                                |  O       \____/  |
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 * This file stores the BossEncounter class, which helps define the bounds of
 * boss encounters, and updates the player's HUD when necessary. 
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 */
using UnityEngine;

public class BossEncounter : MonoBehaviour {
   // Works with a box collider that defines the area of a boss encounter.
   // Makes calls to the UI to update boss-specific information.
   private bool PlayerInSight = false; // Stores whether the player is within the bounds
   [SerializeField] private string BossName; // Name of the boss to be displayed in the UI

   public void SetName(string n){
      // Sets the name of the boss
      BossName = n;
   }

   public bool inSight(){
      // Returns whether the player is within the bounds
      return PlayerInSight;
   }

   void OnTriggerEnter2D(Collider2D collision){
      // Called when the player enters the bounds of the encounter. 
      // Makes boss-specific UI elements visible
      if (collision.CompareTag("Player")){
         PlayerInSight = true;
         UIHandler.instance.EnterBossEncounter(BossName);
      }
   }

   void OnTriggerStay2D(Collider2D collision){
      // Just in case OnTriggerEnter2D was not called for some reason.
      if (collision.CompareTag("Player")){
         if(!PlayerInSight){
            PlayerInSight = true;
         }
      }
   }

   void OnTriggerExit2D(Collider2D collision){
      // Called when the player leaves the bounds. Hides boss-specific
      // UI elements.
      if (collision.CompareTag("Player")){
         PlayerInSight = false;
         UIHandler.instance.ExitBossEncounter();
      }
   }
}
