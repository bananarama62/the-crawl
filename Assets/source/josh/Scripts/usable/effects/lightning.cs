/*   Author: Josh Gillum              .
 *   Date: 11 November 2025           ":"         __ __
 *                                  __|___       \ V /
 *                                .'      '.      | |
 *                                |  O       \____/  |
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 * This file stores the Lightning class, which is spawned by the chain
 * lightning spell. They have a finite lifespan
 *~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~^~~
 */
using UnityEngine;
using NUnit.Framework;
using UnityEditor.Animations;
using System.Collections;
public class Lightning : MonoBehaviour {
   // The six animations for the lightning
   [SerializeField] private AnimatorController animation_loop_1;
   [SerializeField] private AnimatorController animation_loop_2;
   [SerializeField] private AnimatorController animation_loop_3;
   [SerializeField] private AnimatorController animation_loop_4;
   [SerializeField] private AnimatorController animation_loop_5;
   [SerializeField] private AnimatorController animation_loop_6;
   [SerializeField] private float timeToLive; // seconds to live for
   [SerializeField] private int damage;

   private AnimatorBase AnimationControl = new AnimatorType(); // Used for dynamic binding

   void Start(){
      Destroy(gameObject,timeToLive); // Sets timer for when to destroy object
   }

   public void setAnimation(int x){
      // Determines which animation in the chain to use
      AnimatorController ChangeTo = null;
      int num = AnimationControl.getAnimation(x);
      if(num == 1){
         ChangeTo = animation_loop_1;
      } else if(num == 2){
         ChangeTo = animation_loop_2;
      } else if(num == 3){
         ChangeTo = animation_loop_3;
      } else if(num == 4){
         ChangeTo = animation_loop_4;
      } else if(num == 5){
         ChangeTo = animation_loop_5;
      } else if(num == 6){
         ChangeTo = animation_loop_6;
      }

      if(ChangeTo != null){ // If the number was within the bounds of what can set the animation to.
         gameObject.GetComponent<Animator>().runtimeAnimatorController = ChangeTo;
      }
   }

   void OnTriggerEnter2D(Collider2D collision)
   {
      // Only deals damage to the player.
      PlayerController player = collision.GetComponent<PlayerController>();
      if (collision.CompareTag("Player"))
      {
         player.takeDamage(damage);
      }
   }
}
