/*   Author: Josh Gillum              .
 *   Date: 11 November 2025           ":"         __ __
 *                                  __|___       \ V /
 *                                .'      '.      | |
 *                                |  O       \____/  |
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 * This file stores the ChainLightning class, which models a spell. This class
 * is designed to be used with the SpawnPylon spell. It creates lightning bolts
 * that string between an arbitrary number of objects (pylons).
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 */
using UnityEngine;
using System.Collections.Generic;

public class ChainLightning : Effect {
   // Models a chain lightning spell that strings lightning between pylons
   private List<GameObject> pylons;
   [SerializeField] private GameObject LightningBoltPreFab;
   private GameObject origin;

   public override int individualEffect(){
      // SetPylons must be called before this spell is used.
      // This is what actually causes the spell to be cast.
      
      Debug.Log("Casting Chain Lightning...");
      GameObject current_bolt;
      GameObject next_pylon;
      GameObject current_pylon;
      for(int i = -1; i < pylons.Count; ++i){
         // Determines which object to start from and which object to end at, for this iteration
         if(i < 0){
            current_pylon = origin;
            next_pylon = pylons[0];
         } else {
            current_pylon = pylons[i];
            if(i < pylons.Count - 1){
               next_pylon = pylons[i+1];
            } else {
               next_pylon = pylons[0];
            }
         }
         // Creates a number of bolts needed to span the distance without stretching them much.
         // Spawns as many bolts as possible without being too large, then scales all of them to fill in whaterver
         // distance is left.
         current_bolt = Instantiate(LightningBoltPreFab, current_pylon.transform.position, Quaternion.Euler(Vector3.zero)); // Creates the first bolt

         // Calculates which angle to rotate the bolts to so they face the correct direction.
         Vector3 direction = next_pylon.transform.position - current_pylon.transform.position;
         direction.z = 0;
         current_bolt.transform.localScale = new Vector3(3f,3f,1f);
         current_bolt.GetComponent<Lightning>().setAnimation(0); // Sets animation to first in loop
         Vector2 size = current_bolt.GetComponent<BoxCollider2D>().size;

         int count = (int)(direction.magnitude / size.x); // Determines how many bolts can be fit in the gap
         float ratio = direction.magnitude / (size.x*count); // Gap divided by distance the bolts will span (this is amount to scale them all by later)
         size = size * ratio;
         float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Converts Vector3 to an angle
         current_bolt.transform.localScale = new Vector3(ratio,ratio,1f); // Scales bolt by ratio to fill ingap.
         direction = Vector3.Normalize(direction);
         Vector3 previous_position = current_bolt.transform.position;
         Vector3 rotated_length = Vector3.Scale(direction,new Vector3(size.x,size.y,0f)); // Multiplies direction by size to find amount to add to each bolt to get starting point of next bolt.
         // Repeats above process for remaining bolts in strand.
         for(int j = 1; j < count; ++j){
            Debug.Log(direction);
            Vector3 position = rotated_length + previous_position;
            current_bolt = Instantiate(LightningBoltPreFab, position, Quaternion.Euler(0f,0f,angle));
            current_bolt.transform.localScale = new Vector3(3f,3f,1f);
            current_bolt.transform.localScale = new Vector3(ration,ration,1f);
            current_bolt.GetComponent<Lightning>().setAnimation((j+1)%6);
            previous_position = current_bolt.transform.position;
         }
      }
      return 1;
   }

   public void setPylons(List<GameObject> pylons_list){
      // Sets the list of objects that the lightning will string between
      pylons = pylons_list;
   }

   public void setOrigin(GameObject o){
      // Sets which object will be the source of the spell, causing lightning
      // to spawn between its current location and the first pylon.
      origin = o;
   }

   void Awake(){
      base.init();
   }
}
