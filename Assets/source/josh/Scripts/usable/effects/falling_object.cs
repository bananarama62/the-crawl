/*   Author: Josh Gillum              .
 *   Date: 11 November 2025           ":"         __ __
 *                                  __|___       \ V /
 *                                .'      '.      | |
 *                                |  O       \____/  |
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 * This file stores the FallingObject class, which models an object that falls
 * from above. Can be paired with the Impact class to have the object deal
 * damage to anything within a certain radius.
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 */
using UnityEngine;
using NUnit.Framework;

public class FallingObject : MonoBehaviour {
   // Models an object that falls from above.
   [SerializeField] float spawn_delay; // Number of seconds after indicator spawns before fall starts
   private BoxCollider2D collision;
   private SpriteRenderer sprite;
   private Vector3 finalDestination;
   [SerializeField] GameObject locationIndicatorPreFab; // Indicator as to spawn location
   private GameObject locationIndicator;
   [SerializeField] private float indicator_offset_x; // Can be set in editor if object isn't being spawned by a script
   [SerializeField] private float indicator_offset_y; // ''
   private float elapsed_time;
   [SerializeField] float fall_from_height; // How far above spawn point to start fall from
   [SerializeField] float fall_speed; // Increase to make fall faster. 
   private bool started_falling;
   private bool fall_complete;
   private int fall_length;
   private Vector3 indicator_location;

   [SerializeField] private GameObject impact; // Impact damage that spawns at the end of the fall. See Impact class
   void Start(){
      Debug.Log("Casting Falling object");
      indicator_location = finalDestination + new Vector3(indicator_offset_x,indicator_offset_y,0); // location to spawn indicator
      Debug.Log("Indicator: "+finalDestination+" Spawning: "+indicator_location);
      locationIndicator = Instantiate(locationIndicatorPreFab,indicator_location,Quaternion.identity); // Spawns location indicator
   }

   void Update(){
      if(!started_falling || !fall_complete){
         elapsed_time += Time.deltaTime;
         if(!started_falling){
            if (elapsed_time > spawn_delay){ // Checks if it is time to start the fall
               // Starts fall and removes location indicator
               started_falling = true;
               Destroy(locationIndicator);
               sprite.enabled = true;
               elapsed_time = 0;
               transform.position += new Vector3(0,fall_from_height,0); // Enable object at fall start location
            }
         } if(started_falling){
            if(transform.position != finalDestination){
               // Moves the object down until it is in its final position
               transform.position = Vector3.Lerp(transform.position,finalDestination,elapsed_time * fall_speed);
            } else { // Fall is complete
               fall_complete = true;
               collision.enabled = true; // Object can now be collided with
               Instantiate(impact,indicator_location,Quaternion.identity); // Spawns impact damage
            }
         }
      }
   }

   public void setIndicatorOffset(float x, float y){ // Sets location offset from final desination to spawn location indicator
      indicator_offset_x = x;
      indicator_offset_y = y;
   }

   void Awake(){
      // Disables collision and sprite
      collision = GetComponent<BoxCollider2D>();
      sprite = GetComponent<SpriteRenderer>();
      collision.enabled = false;
      sprite.enabled = false;
      // Saves where to spawn the object
      finalDestination = transform.position;
      elapsed_time = 0;
      started_falling = false;
      fall_complete = false;
   }

}
