/*   Author: Josh Gillum              .
 *   Date: 11 November 2025           ":"         __ __
 *                                  __|___       \ V /
 *                                .'      '.      | |
 *                                |  O       \____/  |
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 * This file stores the Swing class, which is used by weapons that are swung.
 * Swing causes the object to rotate in an arc.
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 */
using UnityEngine;

public class Swing : Effect {
   // Models an object swinging in an arc.
   public float SwingSpeed;
   private Quaternion TargetRotation;
   [SerializeField] private GameObject AimDirection; // Direction to attack in
   [SerializeField] private bool SwingFromLeft; // If true, swing from left to right, else right to left
   [SerializeField] private int StartSwingDegreesFromCenter; // How many degrees from center to start swing
   [SerializeField] private int DegreesOfArc; // How many degrees to rotate object as part of swing
   private BoxCollider2D Collision;
   private SpriteRenderer Sprite;


   void enable(bool value){
      // Enables or disables the sprite and collision
      Sprite.enabled = value;
      Collision.enabled = value;
   }

   public override int individualEffect(){
      // Actual effect of object
      Quaternion StartingRotationOffset;
      Quaternion EndingRotation;
      // Calculates starting rotation and ending rotation
      if(SwingFromLeft){
         StartingRotationOffset = Quaternion.Euler(0,0,StartSwingDegreesFromCenter);
         EndingRotation = Quaternion.Euler(0,0,-DegreesOfArc);
      } else {
         StartingRotationOffset = Quaternion.Euler(0,0,-StartSwingDegreesFromCenter);
         EndingRotation = Quaternion.Euler(0,0,DegreesOfArc);
      }
      transform.rotation = AimDirection.transform.rotation * StartingRotationOffset; // Moves object to start of swing
      TargetRotation = transform.rotation * EndingRotation; // Calculates end rotation
      enable(true); // Enables collision and sprite
      return 1;
   }

   void Awake(){
      // Gets the collider and Sprite renderer objects and disables them
      Collision = GetComponent<BoxCollider2D>();
      Sprite = transform.Find("image").GetComponent<SpriteRenderer>();
      enable(false);
   }

   void Update(){
      if(Collision.enabled){ // Moves object whenever collision is enabled (object is active)
         transform.rotation = Quaternion.Slerp(transform.rotation,TargetRotation,Time.deltaTime * SwingSpeed);
         // Checks if near the end of rotation and ends early to prevent stalling.
         if(Mathf.Abs(transform.rotation.eulerAngles.z - TargetRotation.eulerAngles.z) <= 2f){
            enable(false);
         }
      }
   }
}
